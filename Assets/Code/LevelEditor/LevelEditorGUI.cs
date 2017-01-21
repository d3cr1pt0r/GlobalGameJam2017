using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(LevelEditor))]
public class LevelEditorGUI : Editor
{

	private const string Tag = "LevelEditorGUI";

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		LevelEditor LevelEditor = (LevelEditor)target;

		if (GUILayout.Button ("Load Level")) {
			if (LevelEditor.LevelPrefab == null) {
				Log.LogDebug (Tag, "Load Level failed (no level loaded)");
				return;
			}

			GameObject levelPrefab = Instantiate (LevelEditor.LevelPrefab, Vector3.zero, Quaternion.identity);
			LevelEditor.LoadedLevel = levelPrefab;
		}

		if (GUILayout.Button ("Save Level")) {
			PrefabUtility.ReplacePrefab (LevelEditor.LoadedLevel, LevelEditor.LevelPrefab, ReplacePrefabOptions.ReplaceNameBased);
		}
	}

}
