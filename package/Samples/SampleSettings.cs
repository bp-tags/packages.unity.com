using UnityEngine;
using UnityEngine.XR.Management;

namespace Samples
{
    /// <summary>
    /// Simple sample settings showing how to create custom configuration data for your package.
    /// </summary>
    [XRConfigurationData("Sample Settings", SampleConstants.kSettingsKey)]
    [System.Serializable]
    public class SampleSettings : ScriptableObject
    {
        /// Static instance that will hold the runtime asset instance we created in our build process.
        /// <see cref="SampleBuildProcessor"/>
        ///
        #if !UNITY_EDITOR
        public static SampleSettings s_RuntimeInstance = null;
        #endif

         public enum Requirement
         {
             Required,
             Optional,
             None
         }

         [SerializeField, Tooltip("Changes item requirement.")]
         Requirement m_RequiresItem;

         public Requirement RequiresItem
         {
             get { return m_RequiresItem; }
             set { m_RequiresItem = value; }
         }

         [SerializeField, Tooltip("Some toggle for runtime.")]
         bool m_RuntimeToggle = true;

         public bool RuntimeToggle
         {
            get { return m_RuntimeToggle; }
            set { m_RuntimeToggle = value; }
         }

         public void Awake()
         {
            #if !UNITY_EDITOR
            s_RuntimeInstance = this;
            #endif
         }
    }
}
