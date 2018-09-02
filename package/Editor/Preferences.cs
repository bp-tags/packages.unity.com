using UnityEngine;
using UnityEditor;
using System.Collections;

namespace ProGrids.Editor
{
	static class Preferences
	{
		static Color s_GridColorX;
		static Color s_GridColorY;
		static Color s_GridColorZ;
		static float s_AlphaBump;
		static bool s_ScaleSnapEnabled;
		static SnapMethod s_SnapMethod;
		static float s_BracketIncreaseValue;
		static SnapUnit s_GridUnits;
		static bool s_SyncUnitySnap;

		static KeyCode s_IncreaseGridSize = KeyCode.Equals;
		static KeyCode s_DecreaseGridSize = KeyCode.Minus;
		static KeyCode s_NudgePerspectiveBackward = KeyCode.LeftBracket;
		static KeyCode s_NudgePerspectiveForward = KeyCode.RightBracket;
		static KeyCode s_NudgePerspectiveReset = KeyCode.Alpha0;
		static KeyCode s_CyclePerspective = KeyCode.Backslash;

		static bool s_PrefsLoaded = false;

		[PreferenceItem("ProGrids")]
		public static void PreferencesGUI()
		{
			if (!s_PrefsLoaded)
				s_PrefsLoaded = LoadPreferences();

			EditorGUI.BeginChangeCheck();

			GUILayout.Label("Snap Behavior", EditorStyles.boldLabel);
			s_AlphaBump = EditorGUILayout.Slider(new GUIContent("Tenth Line Alpha", "Every 10th line will have it's alpha value bumped by this amount."), s_AlphaBump, 0f, 1f);
			s_GridUnits = (SnapUnit)EditorGUILayout.EnumPopup("Grid Units", s_GridUnits);
			s_ScaleSnapEnabled = EditorGUILayout.Toggle("Snap On Scale", s_ScaleSnapEnabled);
			s_SnapMethod = (SnapMethod) EditorGUILayout.EnumPopup("Snap Method", s_SnapMethod);
			s_SyncUnitySnap = EditorGUILayout.Toggle("Sync w/ Unity Snap", s_SyncUnitySnap);

			GUILayout.Label("Grid Colors", EditorStyles.boldLabel);
			s_GridColorX = EditorGUILayout.ColorField("X Axis", s_GridColorX);
			s_GridColorY = EditorGUILayout.ColorField("Y Axis", s_GridColorY);
			s_GridColorZ = EditorGUILayout.ColorField("Z Axis", s_GridColorZ);

			GUILayout.Label("Shortcuts", EditorStyles.boldLabel);
			s_IncreaseGridSize = (KeyCode)EditorGUILayout.EnumPopup("Increase Grid Size", s_IncreaseGridSize);
			s_DecreaseGridSize = (KeyCode)EditorGUILayout.EnumPopup("Decrease Grid Size", s_DecreaseGridSize);
			s_NudgePerspectiveBackward = (KeyCode)EditorGUILayout.EnumPopup("Nudge Perspective Backward", s_NudgePerspectiveBackward);
			s_NudgePerspectiveForward = (KeyCode)EditorGUILayout.EnumPopup("Nudge Perspective Forward", s_NudgePerspectiveForward);
			s_NudgePerspectiveReset = (KeyCode)EditorGUILayout.EnumPopup("Nudge Perspective Reset", s_NudgePerspectiveReset);
			s_CyclePerspective = (KeyCode)EditorGUILayout.EnumPopup("Cycle Perspective", s_CyclePerspective);

			if (GUILayout.Button("Reset"))
			{
				if (UnityEditor.EditorUtility.DisplayDialog("Delete ProGrids editor preferences?", "Are you sure you want to delete these? This action cannot be undone.", "Yes", "No"))
					ResetPrefs();
			}

			if(EditorGUI.EndChangeCheck())
				SetPreferences();
		}

		public static bool LoadPreferences()
		{
			s_ScaleSnapEnabled = EditorPrefs.GetBool(PreferenceKeys.SnapScale, Defaults.SnapOnScale);
			s_GridColorX = (EditorPrefs.HasKey(PreferenceKeys.GridColorX)) ? EditorUtility.ColorWithString(EditorPrefs.GetString(PreferenceKeys.GridColorX)) : Defaults.GridColorX;
			s_GridColorY = (EditorPrefs.HasKey(PreferenceKeys.GridColorY)) ? EditorUtility.ColorWithString(EditorPrefs.GetString(PreferenceKeys.GridColorY)) : Defaults.GridColorY;
			s_GridColorZ = (EditorPrefs.HasKey(PreferenceKeys.GridColorZ)) ? EditorUtility.ColorWithString(EditorPrefs.GetString(PreferenceKeys.GridColorZ)) : Defaults.GridColorZ;
			s_AlphaBump = EditorPrefs.GetFloat(PreferenceKeys.AlphaBump, Defaults.AlphaBump);
			s_BracketIncreaseValue = EditorPrefs.HasKey(PreferenceKeys.BracketIncreaseValue) ? EditorPrefs.GetFloat(PreferenceKeys.BracketIncreaseValue) : .25f;
			s_GridUnits = (SnapUnit) EditorPrefs.GetInt(PreferenceKeys.GridUnit, 0);
			s_SyncUnitySnap = EditorPrefs.GetBool(PreferenceKeys.SyncUnitySnap, true);
			s_SnapMethod = (SnapMethod) EditorPrefs.GetInt(PreferenceKeys.SnapMethod, (int) Defaults.SnapMethod);

			s_IncreaseGridSize = EditorPrefs.HasKey(PreferenceKeys.IncreaseGridSize)
				? (KeyCode)EditorPrefs.GetInt(PreferenceKeys.IncreaseGridSize)
				: KeyCode.Equals;
			s_DecreaseGridSize = EditorPrefs.HasKey(PreferenceKeys.DecreaseGridSize)
				? (KeyCode)EditorPrefs.GetInt(PreferenceKeys.DecreaseGridSize)
				: KeyCode.Minus;
			s_NudgePerspectiveBackward = EditorPrefs.HasKey(PreferenceKeys.NudgePerspectiveBackward)
				? (KeyCode)EditorPrefs.GetInt(PreferenceKeys.NudgePerspectiveBackward)
				: KeyCode.LeftBracket;
			s_NudgePerspectiveForward = EditorPrefs.HasKey(PreferenceKeys.NudgePerspectiveForward)
				? (KeyCode)EditorPrefs.GetInt(PreferenceKeys.NudgePerspectiveForward)
				: KeyCode.RightBracket;
			s_NudgePerspectiveReset = EditorPrefs.HasKey(PreferenceKeys.NudgePerspectiveReset)
				? (KeyCode)EditorPrefs.GetInt(PreferenceKeys.NudgePerspectiveReset)
				: KeyCode.Alpha0;
			s_CyclePerspective = EditorPrefs.HasKey(PreferenceKeys.CyclePerspective)
				? (KeyCode)EditorPrefs.GetInt(PreferenceKeys.CyclePerspective)
				: KeyCode.Backslash;

			return true;
		}

		public static void SetPreferences()
		{
			EditorPrefs.SetBool(PreferenceKeys.SnapScale, s_ScaleSnapEnabled);
			EditorPrefs.SetString(PreferenceKeys.GridColorX, s_GridColorX.ToString("f3"));
			EditorPrefs.SetString(PreferenceKeys.GridColorY, s_GridColorY.ToString("f3"));
			EditorPrefs.SetString(PreferenceKeys.GridColorZ, s_GridColorZ.ToString("f3"));
			EditorPrefs.SetFloat(PreferenceKeys.AlphaBump, s_AlphaBump);
			EditorPrefs.SetFloat(PreferenceKeys.BracketIncreaseValue, s_BracketIncreaseValue);
			EditorPrefs.SetInt(PreferenceKeys.GridUnit, (int)s_GridUnits);
			EditorPrefs.SetBool(PreferenceKeys.SyncUnitySnap, s_SyncUnitySnap);
			EditorPrefs.SetInt(PreferenceKeys.IncreaseGridSize, (int)s_IncreaseGridSize);
			EditorPrefs.SetInt(PreferenceKeys.DecreaseGridSize, (int)s_DecreaseGridSize);
			EditorPrefs.SetInt(PreferenceKeys.NudgePerspectiveBackward, (int)s_NudgePerspectiveBackward);
			EditorPrefs.SetInt(PreferenceKeys.NudgePerspectiveForward, (int)s_NudgePerspectiveForward);
			EditorPrefs.SetInt(PreferenceKeys.NudgePerspectiveReset, (int)s_NudgePerspectiveReset);
			EditorPrefs.SetInt(PreferenceKeys.CyclePerspective, (int)s_CyclePerspective);
			EditorPrefs.SetInt(PreferenceKeys.SnapMethod, (int) s_SnapMethod);

			if (ProGridsEditor.instance != null)
				ProGridsEditor.instance.LoadPreferences();
		}

		public static void ResetPrefs()
		{
			EditorPrefs.DeleteKey(PreferenceKeys.SnapValue);
			EditorPrefs.DeleteKey(PreferenceKeys.SnapMultiplier);
			EditorPrefs.DeleteKey(PreferenceKeys.SnapEnabled);
			EditorPrefs.DeleteKey(PreferenceKeys.LastOrthoToggledRotation);
			EditorPrefs.DeleteKey(PreferenceKeys.BracketIncreaseValue);
			EditorPrefs.DeleteKey(PreferenceKeys.GridUnit);
			EditorPrefs.DeleteKey(PreferenceKeys.LockGrid);
			EditorPrefs.DeleteKey(PreferenceKeys.LockedGridPivot);
			EditorPrefs.DeleteKey(PreferenceKeys.GridAxis);
			EditorPrefs.DeleteKey(PreferenceKeys.PerspGrid);
			EditorPrefs.DeleteKey(PreferenceKeys.SnapScale);
			EditorPrefs.DeleteKey(PreferenceKeys.PredictiveGrid);
			EditorPrefs.DeleteKey(PreferenceKeys.SnapAsGroup);
			EditorPrefs.DeleteKey(PreferenceKeys.MajorLineIncrement);
			EditorPrefs.DeleteKey(PreferenceKeys.SyncUnitySnap);
			EditorPrefs.DeleteKey(PreferenceKeys.SnapMethod);
			EditorPrefs.DeleteKey(PreferenceKeys.GridColorX);
			EditorPrefs.DeleteKey(PreferenceKeys.GridColorY);
			EditorPrefs.DeleteKey(PreferenceKeys.GridColorZ);
			EditorPrefs.DeleteKey(PreferenceKeys.AlphaBump);
			EditorPrefs.DeleteKey(PreferenceKeys.ShowGrid);
			EditorPrefs.DeleteKey(PreferenceKeys.IncreaseGridSize);
			EditorPrefs.DeleteKey(PreferenceKeys.DecreaseGridSize);
			EditorPrefs.DeleteKey(PreferenceKeys.NudgePerspectiveBackward);
			EditorPrefs.DeleteKey(PreferenceKeys.NudgePerspectiveForward);
			EditorPrefs.DeleteKey(PreferenceKeys.NudgePerspectiveReset);
			EditorPrefs.DeleteKey(PreferenceKeys.CyclePerspective);

			LoadPreferences();
		}
	}
}
