using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor (typeof(LevelEditor))]
public class LevelEditorGUI : Editor
{

	private const string Tag = "LevelEditorGUI";

	private string patternName = "pattern_1";

	private LevelEditor LevelEditor;

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		LevelEditor = (LevelEditor)target;

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Load Level")) {
			LoadLevel ();
		}

		if (GUILayout.Button ("Save Level")) {
			PrefabUtility.ReplacePrefab (LevelEditor.LoadedLevel, LevelEditor.LevelPrefab, ReplacePrefabOptions.ReplaceNameBased);
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Load Pattern")) {
			LoadPattern ();
		}

		if (GUILayout.Button ("Save Pattern")) {
			PrefabUtility.ReplacePrefab (LevelEditor.LoadedLevel, LevelEditor.LevelPrefab, ReplacePrefabOptions.ReplaceNameBased);
		}
		GUILayout.EndHorizontal ();

		GUILayout.Label ("Level Items:");

		string[] items = Directory.GetFiles (ItemPrefabUtil.ItemPrefabsPath);

		for (int i = 0; i < items.Length; i++) {
			string path = items [i];
			string name = Path.GetFileNameWithoutExtension (path);

			if (!path.EndsWith (".prefab")) {
				continue;
			}

			if (GUILayout.Button (name)) {
				LoadItemPrefab (path);
			}
		}

		GUILayout.Label ("Pattern name");
		patternName = GUILayout.TextField (patternName, 25);

		if (GUILayout.Button ("Save Pattern")) {
			SavePattern ();
		}
	}

	private void LoadLevel ()
	{
		if (LevelEditor.LevelPrefab == null) {
			Log.LogDebug (Tag, "Load Level failed (no level loaded)");
			return;
		}

		GameObject levelPrefab = Instantiate (LevelEditor.LevelPrefab, Vector3.zero, Quaternion.identity);
		LevelEditor.LoadedLevel = levelPrefab;
	}

	private void LoadPattern ()
	{
		if (LevelEditor.Pattern == null) {
			Log.LogDebug (Tag, "Load Pattern failed (no pattern loaded)");
			return;
		}

		if (LevelEditor.LoadedLevel == null) {
			LoadLevel ();
		}

		GameObject container = GameObject.Find ("PatternContainer");
		if (container != null) {
			Log.LogDebug (Tag, "PatternContainer found! Delete currently loaded pattern data and try again!");
			return;
		} else {
			container = new GameObject ("PatternContainer");
		}

		for (int i = 0; i < LevelEditor.Pattern.ItemNodes.Count; i++) {
			Pattern.ItemNode itemNode = LevelEditor.Pattern.ItemNodes [i];
			GameObject itemPrefab = null;

			if (itemNode.ItemPrefab == null) {
				if (itemNode.ItemType == Enums.ItemType.GOAL) {
					itemPrefab = ItemPrefabUtil.GetGoalProxy ();
				}
				if (itemNode.ItemType == Enums.ItemType.DEBREE) {
					itemPrefab = ItemPrefabUtil.GetDebreeProxy ();
				}
			} else {
				itemPrefab = itemNode.ItemPrefab;
			}

			itemPrefab = Instantiate (itemPrefab);

			itemPrefab.transform.position = itemNode.Position;
			itemPrefab.transform.rotation = Quaternion.Euler (itemNode.Rotation);
			itemPrefab.transform.localScale = itemNode.Scale;

			itemPrefab.transform.SetParent (container.transform);
		}
	}

	private void SavePattern ()
	{
		GameObject container = GameObject.Find ("PatternContainer");
		if (container == null) {
			Log.LogDebug (Tag, "No PatternContainer GameObject found");
			return;
		}

		Pattern pattern = ScriptableObject.CreateInstance<Pattern> ();
		pattern.ItemNodes = new List<Pattern.ItemNode> ();
		pattern.RootPosition = container.transform.position;

		for (int i = 0; i < container.transform.childCount; i++) {
			GameObject patternItem = container.transform.GetChild (i).gameObject;
			Pattern.ItemNode itemNode = new Pattern.ItemNode ();

			if (patternItem.name.Contains ("goal_proxy")) {
				itemNode.ItemPrefab = null;
				itemNode.ItemType = Enums.ItemType.GOAL;
				itemNode.Position = patternItem.transform.position;
			} else if (patternItem.name.Contains ("debree_proxy")) {
				itemNode.ItemPrefab = null;
				itemNode.ItemType = Enums.ItemType.DEBREE;
				itemNode.Position = patternItem.transform.position;
			} else {
				string path = null;
				string[] files = Directory.GetFiles (ItemPrefabUtil.ItemPrefabsPath);
				for (int j = 0; j < files.Length; j++) {
					string name = Path.GetFileNameWithoutExtension (files [j]);
					if (patternItem.name.Contains (name)) {
						path = files [j];
						break;
					}
				}

				if (path == null) {
					Log.LogDebug (Tag, "Could not find prefab for spawned object {0}", patternItem.name);
					return;
				}

				GameObject originalItemPrefab = AssetDatabase.LoadAssetAtPath (path, typeof(GameObject)) as GameObject;

				itemNode.ItemPrefab = originalItemPrefab;
				itemNode.ItemType = patternItem.GetComponent<ItemController> ().GetItemType ();
				itemNode.Position = patternItem.transform.position;
				itemNode.Rotation = patternItem.transform.rotation.eulerAngles;
				itemNode.Scale = patternItem.transform.localScale;
			}

			pattern.ItemNodes.Add (itemNode);
		}

		AssetDatabase.CreateAsset (pattern, "Assets/Resources/Universe/Patterns/" + patternName + ".asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = pattern;
	}

	private void LoadItemPrefab (string path)
	{
		GameObject itemObject = AssetDatabase.LoadAssetAtPath<GameObject> (path);
		GameObject itemPrefab = Instantiate (itemObject, Vector3.zero, Quaternion.identity);

		GameObject container = GameObject.Find ("PatternContainer");
		if (container == null) {
			container = new GameObject ("PatternContainer");
		}

		itemPrefab.transform.SetParent (container.transform);
	}

}
