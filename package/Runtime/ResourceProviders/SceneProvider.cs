using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.Diagnostics;
using System;

namespace UnityEngine.ResourceManagement
{
    public class SceneProvider : ISceneProvider
    {
        class InternalOp : AsyncOperationBase<Scene>
        {
            LoadSceneMode m_loadMode;
            Scene m_scene;
            System.Action<IAsyncOperation<IList<object>>> action;

            public InternalOp()
            {
                action = (op) =>
                {
                    if (op == null || op.Status == AsyncOperationStatus.Succeeded)
                    {
                        var reqOp = SceneManager.LoadSceneAsync((Context as IResourceLocation).InternalId, m_loadMode);
                        m_scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
                        if (reqOp == null || reqOp.isDone)
                            DelayedActionManager.AddAction((System.Action<AsyncOperation>)OnSceneLoaded, 0, reqOp);
                        else
                            reqOp.completed += OnSceneLoaded;
                    }
                    else
                    {
                        m_error = op.OperationException;
                        SetResult(default(Scene));
                        OnSceneLoaded(null);
                    }
                };
            }


            public IAsyncOperation<Scene> Start(IResourceLocation location, IAsyncOperation<IList<object>> loadDependencyOperation, LoadSceneMode loadMode)
            {
                Validate();
                Context = location;
                m_loadMode = loadMode;
                if (loadDependencyOperation == null)
                    action(null);
                else
                    loadDependencyOperation.Completed += action;
                return this;
            }

            void OnSceneLoaded(AsyncOperation operation)
            {
                Validate();
                ResourceManagerEventCollector.PostEvent(ResourceManagerEventCollector.EventType.LoadSceneAsyncCompletion, Context, 1);
                ResourceManagerEventCollector.PostEvent(ResourceManagerEventCollector.EventType.CacheEntryLoadPercent, Context, 100);
                SetResult(m_scene);
                InvokeCompletionEvent();
            }

            public override bool IsDone
            {
                get
                {
                    Validate();
                    return base.IsDone && Result.isLoaded;
                }
            }
        }

        public IAsyncOperation<Scene> ProvideSceneAsync(IResourceLocation location, IAsyncOperation<IList<object>> loadDependencyOperation, LoadSceneMode loadMode)
        {
            if (location == null)
                throw new ArgumentNullException("location");
            return AsyncOperationCache.Instance.Acquire<InternalOp>().Start(location, loadDependencyOperation, loadMode);
        }

        class InternalReleaseOp : AsyncOperationBase<Scene>
        {
            public IAsyncOperation<Scene> Start(IResourceLocation location, Scene scene)
            {
                Validate();
                Context = location;
                ResourceManagerEventCollector.PostEvent(ResourceManagerEventCollector.EventType.ReleaseSceneAsyncRequest, Context, 0);
                var unloadOp = SceneManager.UnloadSceneAsync(scene);
                if (unloadOp != null)
                    unloadOp.completed += OnSceneUnloaded;
                return this;
            }

            void OnSceneUnloaded(AsyncOperation operation)
            {
                Validate();
                ResourceManagerEventCollector.PostEvent(ResourceManagerEventCollector.EventType.ReleaseSceneAsyncCompletion, Context, 0);
                ResourceManagerEventCollector.PostEvent(ResourceManagerEventCollector.EventType.CacheEntryLoadPercent, Context, 0);
            }

            public override bool IsDone
            {
                get
                {
                    Validate();
                    return base.IsDone && !Result.isLoaded;
                }
            }
        }

        public IAsyncOperation<Scene> ReleaseSceneAsync(IResourceLocation location, Scene scene)
        {
            return AsyncOperationCache.Instance.Acquire<InternalReleaseOp>().Start(location, scene);
        }
    }
}
