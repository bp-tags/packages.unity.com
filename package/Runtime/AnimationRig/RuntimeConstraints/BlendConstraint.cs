﻿namespace UnityEngine.Animations.Rigging
{
    [System.Serializable]
    public struct BlendConstraintData : IAnimationJobData, IBlendConstraintData, IRigReferenceSync
    {
        [SerializeField] JobTransform m_ConstrainedObject;
        [SerializeField] JobTransform m_SourceA;
        [SerializeField] JobTransform m_SourceB;
        [SerializeField] bool m_BlendPosition;
        [SerializeField] bool m_BlendRotation;

        [SerializeField, Range(0f, 1f)] float m_PositionWeight;
        [SerializeField, Range(0f, 1f)] float m_RotationWeight;

        public JobTransform constrainedObject { get => m_ConstrainedObject; set => m_ConstrainedObject = value; }
        public JobTransform sourceObjectA { get => m_SourceA; set => m_SourceA = value; }
        public JobTransform sourceObjectB { get => m_SourceB; set => m_SourceB = value; }
        public bool blendPosition { get => m_BlendPosition; set => m_BlendPosition = value; }
        public bool blendRotation { get => m_BlendRotation; set => m_BlendRotation = value; }
        public float positionWeight { get => m_PositionWeight; set => m_PositionWeight = Mathf.Clamp01(value); }
        public float rotationWeight { get => m_RotationWeight; set => m_RotationWeight = Mathf.Clamp01(value); }

        Transform IBlendConstraintData.constrainedObject => m_ConstrainedObject.transform;
        Transform IBlendConstraintData.sourceA => m_SourceA.transform;
        Transform IBlendConstraintData.sourceB => m_SourceB.transform;

        bool IAnimationJobData.IsValid() => !(m_ConstrainedObject.transform == null || m_SourceA.transform == null || m_SourceB.transform == null);

        void IAnimationJobData.SetDefaultValues()
        {
            m_ConstrainedObject = JobTransform.defaultNoSync;
            m_SourceA = JobTransform.defaultNoSync;
            m_SourceB = JobTransform.defaultNoSync;
            m_BlendPosition = true;
            m_BlendRotation = true;
            m_PositionWeight = 0.5f;
            m_RotationWeight = 0.5f;
        }

        JobTransform[] IRigReferenceSync.allReferences => new JobTransform[] { m_ConstrainedObject, m_SourceA, m_SourceB };
    }

    [AddComponentMenu("Animation Rigging/Blend Constraint")]
    public class BlendConstraint : RuntimeRigConstraint<
        BlendConstraintJob,
        BlendConstraintData,
        BlendConstraintJobBinder<BlendConstraintData>
        >
    {
    #if UNITY_EDITOR
    #pragma warning disable 0414
        [SerializeField, HideInInspector] bool m_SourceObjectsGUIToggle;
        [SerializeField, HideInInspector] bool m_SettingsGUIToggle;
    #endif
    }
}