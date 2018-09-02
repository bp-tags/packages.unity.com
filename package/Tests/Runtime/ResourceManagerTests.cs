using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.ResourceManagement;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ResourceManagerTests : IPrebuildSetup
{
    private string k_RootTestAssetsFolder = "Assets/TestAssetsToBeDeleted/";
    private string k_ResourcesFolder = "Assets/TestAssetsToBeDeleted/TestResources/Resources/";

    private Dictionary<string, bool> k_GeneratedFilesToCleanup;
    bool m_resourceManagerInitialized = false;

    IEnumerator WaitForRMInit()
    {
        while (!m_resourceManagerInitialized)
            yield return null;

        ResourceManager.ResourceLocators.Clear();
        ResourceManager.ResourceProviders.Clear();
        ResourceManager.ResourceProviders.Add(new LegacyResourcesProvider());
        ResourceManager.ResourceLocators.Add(new LegacyResourcesLocator());
        ResourceManager.ResourceLocators.Add(new ResourceLocationLocator());
        ResourceManager.InstanceProvider = new InstanceProvider();

    }

    public void Setup()
    {
        if (!Directory.Exists(k_ResourcesFolder))
            Directory.CreateDirectory(k_ResourcesFolder);

        PrefabUtility.CreateEmptyPrefab(k_ResourcesFolder + "Cube.prefab");
        PrefabUtility.CreateEmptyPrefab(k_ResourcesFolder + "Cube1.prefab");
        PrefabUtility.CreateEmptyPrefab(k_ResourcesFolder + "Cube2.prefab");
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        k_GeneratedFilesToCleanup = new Dictionary<string, bool>();

        //These are files generated by the ResourceManager that may need to be cleaned up in the end.  We'll check and make sure they don't already
        //exist before deciding to delete them.
        k_GeneratedFilesToCleanup.Add("Assets/StreamingAssets/ResourceManagerRuntimeData_catalog.json.meta", false);
        k_GeneratedFilesToCleanup.Add("Assets/StreamingAssets/ResourceManagerRuntimeData_settings.json.meta", false);
        k_GeneratedFilesToCleanup.Add("Assets/StreamingAssets/catalog_1.hash", false);
        k_GeneratedFilesToCleanup.Add("Assets/StreamingAssets/catalog_1.json", false);
        k_GeneratedFilesToCleanup.Add("Assets/StreamingAssets/VirtualAssetBundleData.json.meta", false);
        k_GeneratedFilesToCleanup.Add("Assets/StreamingAssets.meta", false);
        k_GeneratedFilesToCleanup.Add("Assets/StreamingAssets/catalog_1.hash.meta", false);
        k_GeneratedFilesToCleanup.Add("Assets/StreamingAssets/catalog_1.json.meta", false);

        string[] keys = k_GeneratedFilesToCleanup.Keys.ToArray();
        foreach (string path in keys)
        {
            if (File.Exists(path))
                k_GeneratedFilesToCleanup[path] = true;
        }

        ResourceManager.InitializationComplete += OnResourceManagerInitilized;
    }

    private void OnResourceManagerInitilized()
    {
        m_resourceManagerInitialized = true;
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        Directory.Delete(k_RootTestAssetsFolder, true);

        foreach (string path in k_GeneratedFilesToCleanup.Keys)
        {
            if (!k_GeneratedFilesToCleanup[path])
            {
                if(File.Exists(path))
                    File.Delete(path);
            }
        }
    }

[UnityTest]
    public IEnumerator CanLoadAssetsFrom_ResourcesFolder_WithCallback()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube.prefab";
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go);
        MonoBehaviour.Destroy(go);

        GameObject cube = null;
        var oper = ResourceManager.LoadAsync<GameObject, string>("Cube");
        oper.Completed +=
            (op) =>
            {
                cube = op.Result as GameObject;
            };

        yield return null;
        Assert.IsNotNull(cube);
        DestroyAsset(cubePath);
    }

    [UnityTest]
    public IEnumerator CanLoadFrom_ResourceFolder_WithAsyncOperation()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube.prefab";
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go);
        MonoBehaviour.Destroy(go);

        IAsyncOperation op = ResourceManager.LoadAsync<GameObject, string>("Cube");
        yield return op;

        GameObject cube = op.Result as GameObject;
        op.Release();
        Assert.IsNotNull(cube);

        DestroyAsset(cubePath);
        MonoBehaviour.Destroy(go);
    }

    [UnityTest]
    public IEnumerator CanLoadAllAssets_FromResourcesFolder()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube.prefab";
        string cube1Path = k_ResourcesFolder + "Cube1.prefab";
        string cube2Path = k_ResourcesFolder + "Cube2.prefab";

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go);
        PrefabUtility.CreatePrefab(cube1Path, go);
        PrefabUtility.CreatePrefab(cube2Path, go);
        MonoBehaviour.Destroy(go);

        List<GameObject> gameObjects = new List<GameObject>();
        IAsyncOperation op = ResourceManager.LoadAllAsync<GameObject, string>(new List<string>() { "Cube", "Cube1", "Cube2" }, (operation) =>
            {
                gameObjects.Add(operation.Result);
            });

        yield return op;

        Assert.AreEqual(3, gameObjects.Count);
        DestroyAsset(cubePath);
        DestroyAsset(cube1Path);
        DestroyAsset(cube2Path);
    }

    [UnityTest]
    public IEnumerator GetResourceLocation()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube.prefab";
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go);
        MonoBehaviour.Destroy(go);

        IResourceLocation location = ResourceManager.GetResourceLocation("Cube");

        IAsyncOperation op = ResourceManager.LoadAsync<GameObject, IResourceLocation>(location);
        yield return op;

        GameObject cube = op.Result as GameObject;
        Assert.IsNotNull(cube);
        Assert.AreEqual("Cube", cube.name);
        DestroyAsset(cubePath);
    }

    [UnityTest]
    public IEnumerator GetResourceProvider()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube.prefab";
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go);
        MonoBehaviour.Destroy(go);


        IResourceLocation location = ResourceManager.GetResourceLocation("Cube");
        IResourceProvider provider = ResourceManager.GetResourceProvider<GameObject>(location);

        Assert.AreEqual(typeof(LegacyResourcesProvider).FullName, provider.ProviderId);
        DestroyAsset(cubePath);
    }

    [UnityTest]
    public IEnumerator InstansiateObject_Async()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube1.prefab";
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go);
        MonoBehaviour.Destroy(go);

        IAsyncOperation op = ResourceManager.InstantiateAsync<GameObject, string>("Cube1");
        yield return op;

        GameObject obj = op.Result as GameObject;
        Assert.IsNotNull(obj);
        Assert.IsNotNull(GameObject.Find("Cube1(Clone)"));

        MonoBehaviour.Destroy(GameObject.Find("Cube1(Clone)"));
        DestroyAsset(cubePath);
    }

    [UnityTest]
    public IEnumerator InstansiateAllObjects_Async()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube.prefab";
        string cube1Path = k_ResourcesFolder + "Cube1.prefab";
        string cube2Path = k_ResourcesFolder + "Cube2.prefab";

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go);
        PrefabUtility.CreatePrefab(cube1Path, go);
        PrefabUtility.CreatePrefab(cube2Path, go);
        MonoBehaviour.Destroy(go);

        List<GameObject> objects = new List<GameObject>();
        IAsyncOperation op =
            ResourceManager.InstantiateAllAsync<GameObject, string>(new List<string>() { "Cube", "Cube1", "Cube2" },
                (o) =>
            {
                objects.Add(o.Result);
            });
        yield return op;

        Assert.AreEqual(3, objects.Count);
        Assert.IsNotNull(GameObject.Find("Cube(Clone)"));
        Assert.IsNotNull(GameObject.Find("Cube1(Clone)"));
        Assert.IsNotNull(GameObject.Find("Cube2(Clone)"));

        MonoBehaviour.Destroy(GameObject.Find("Cube(Clone)"));
        MonoBehaviour.Destroy(GameObject.Find("Cube1(Clone)"));
        MonoBehaviour.Destroy(GameObject.Find("Cube2(Clone)"));

        DestroyAsset(cubePath);
        DestroyAsset(cube1Path);
        DestroyAsset(cube2Path);
    }

    [UnityTest]
    public IEnumerator LoadAllDependencies_FromResourceLocation()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube.prefab";
        string cube1Path = k_ResourcesFolder + "Cube1.prefab";
        string cube2Path = k_ResourcesFolder + "Cube2.prefab";

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go);
        PrefabUtility.CreatePrefab(cube1Path, go);
        PrefabUtility.CreatePrefab(cube2Path, go);
        MonoBehaviour.Destroy(go);

        IResourceLocation dep1 = ResourceManager.GetResourceLocation("Cube1");
        IResourceLocation dep2 = ResourceManager.GetResourceLocation("Cube2");
        IResourceLocation[] deps = new IResourceLocation[] { dep1, dep2 };
        IResourceLocation location = new ResourceLocationBase<string>("Cube", "Cube", typeof(LegacyResourcesProvider).FullName, deps);

        List<GameObject> loadedDependencies = new List<GameObject>();
        IAsyncOperation asyncOperation = ResourceManager.PreloadDependenciesAsync(location, (op) =>
            {
                loadedDependencies.Add(op.Result as GameObject);
            });
        yield return asyncOperation;
        asyncOperation.Release();

        Assert.AreEqual(2, loadedDependencies.Count);
        DestroyAsset(cubePath);
        DestroyAsset(cube1Path);
        DestroyAsset(cube2Path);
    }

    [UnityTest]
    public IEnumerator ReleaseInstance()
    {
        yield return WaitForRMInit();

        string cube1Path = k_ResourcesFolder + "Cube1.prefab";
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cube1Path, go);
        MonoBehaviour.Destroy(go);

        IAsyncOperation op = ResourceManager.InstantiateAsync<GameObject, string>("Cube1");
        yield return op;

        Assert.IsNotNull(GameObject.Find("Cube1(Clone)"));

        ResourceManager.ReleaseInstance<GameObject>(op.Result as GameObject);
        op.Release();
        yield return null;
        Assert.IsNull(GameObject.Find("Cube1(Clone)"));
        DestroyAsset(cube1Path);
    }

    [UnityTest]
    public IEnumerator LoadAllObjects_Async()
    {
        yield return WaitForRMInit();

        string cubePath = k_ResourcesFolder + "Cube.prefab";
        string cube1Path = k_ResourcesFolder + "Cube1.prefab";

        GameObject go1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PrefabUtility.CreatePrefab(cubePath, go1);
        PrefabUtility.CreatePrefab(cube1Path, go1);
        MonoBehaviour.Destroy(go1);

        IResourceLocation loc1 = ResourceManager.GetResourceLocation("Cube");
        IResourceLocation loc2 = ResourceManager.GetResourceLocation("Cube1");
        List<IResourceLocation> locs = new List<IResourceLocation>() { loc1, loc2 };

        List<GameObject> loadedObjects = new List<GameObject>();
        IAsyncOperation loadOp = ResourceManager.LoadAllAsync<GameObject, IResourceLocation>(locs, (op) =>
            {
                GameObject go = op.Result as GameObject;
                loadedObjects.Add(go);
            });
        yield return loadOp;
        loadOp.Release();

        Assert.AreEqual(2, loadedObjects.Count);
        DestroyAsset(cubePath);
        DestroyAsset(cube1Path);
    }

    void DestroyAsset(string path)
    {
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
