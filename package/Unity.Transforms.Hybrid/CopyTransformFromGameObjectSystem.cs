﻿using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Unity.Transforms
{
    [UnityEngine.ExecuteAlways]
    [UpdateInGroup(typeof(TransformSystemGroup))]
    [UpdateBefore(typeof(EndFrameTRSToLocalToWorldSystem))]
    public class CopyTransformFromGameObjectSystem : JobComponentSystem
    {
        struct TransformStash
        {
            public float3 position;
            public quaternion rotation;
        }

        [BurstCompile]
        struct StashTransforms : IJobParallelForTransform
        {
            public NativeArray<TransformStash> transformStashes;

            public void Execute(int index, TransformAccess transform)
            {
                transformStashes[index] = new TransformStash
                {
                    rotation       = transform.rotation,
                    position       = transform.position,
                };
            }
        }

        [BurstCompile]
        struct CopyTransforms : IJobProcessComponentDataWithEntity<LocalToWorld>
        {
            [DeallocateOnJobCompletion] public NativeArray<TransformStash> transformStashes;

            public void Execute(Entity entity, int index, ref LocalToWorld localToWorld)
            {
                var transformStash = transformStashes[index];

                localToWorld = new LocalToWorld
                {
                    Value = float4x4.TRS(
                        transformStash.position,
                        transformStash.rotation,
                        new float3(1.0f, 1.0f, 1.0f))
                };
            }
        }

        ComponentGroup m_TransformGroup;

        protected override void OnCreateManager()
        {
            m_TransformGroup = GetComponentGroup(
                ComponentType.ReadOnly(typeof(CopyTransformFromGameObject)),
                typeof(UnityEngine.Transform),
                ComponentType.ReadWrite<LocalToWorld>());
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var transforms = m_TransformGroup.GetTransformAccessArray();
            var transformStashes = new NativeArray<TransformStash>(transforms.length, Allocator.TempJob);
            var stashTransformsJob = new StashTransforms
            {
                transformStashes = transformStashes
            };

            var stashTransformsJobHandle = stashTransformsJob.Schedule(transforms, inputDeps);

            var copyTransformsJob = new CopyTransforms
            {
                transformStashes = transformStashes,
            };

            return copyTransformsJob.ScheduleGroup(m_TransformGroup, stashTransformsJobHandle);
        }
    }
}
