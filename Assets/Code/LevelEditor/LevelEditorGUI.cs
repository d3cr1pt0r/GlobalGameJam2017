using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor (typeof(LevelEditor))]
public class LevelEditorGUI : Editor
{

	private const string Tag = "LevelEditorGUI";
	private const string ItemPrefabsPath = "Assets/Resources/Prefabs/Items";

	private List<GameObject> AddedItems;
	private string patternName = "pattern_1";

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		LevelEditor LevelEditor = (LevelEditor)target;

		GUILayout.BeginHorizontal ();
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
		GUILayout.EndHorizontal ();

		GUILayout.Label ("Level Items:");

		string[] items = Directory.GetFiles (ItemPrefabsPath);
		AddedItems = new List<GameObject> ();

		for (int i = 0; i < items.Length; i++) {
			string path = items [i];
			string name = Path.GetFileNameWithoutExtension (path);

			if (!path.EndsWith (".prefab")) {
				continue;
			}

			if (GUILayout.Button (name)) {
				GameObject itemObject = AssetDatabase.LoadAssetAtPath<GameObject> (path);
				GameObject itemPrefab = Instantiate (itemObject, Vector3.zero, Quaternion.identity);

				GameObject container = GameObject.Find ("PatternContainer");
				if (container == null) {
					container = new GameObject ("PatternContainer");
				}

				itemPrefab.transform.SetParent (container.transform);

			}
		}

		GUILayout.Label ("Pattern name");
		patternName = GUILayout.TextField (patternName, 25);

		if (GUILayout.Button ("Save Pattern")) {
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
					itemNode.ItemType = Enums.ItemType.GOAL;
					itemNode.Position = patternItem.transform.position;
				} else {
					string path = null;
					string[] files = Directory.GetFiles (ItemPrefabsPath);
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

					Debug.Log (path);
					Debug.Log (originalItemPrefab);

					itemNode.ItemPrefab = originalItemPrefab;
					itemNode.ItemType = patternItem.GetComponent<ItemController> ().GetItemType ();
					itemNode.Position = patternItem.transform.position;
				}

				pattern.ItemNodes.Add (itemNode);
			}

			AssetDatabase.CreateAsset (pattern, "Assets/Resources/Universe/Patterns/" + patternName + ".asset");
			AssetDatabase.SaveAssets ();

			EditorUtility.FocusProjectWindow ();

			Selection.activeObject = pattern;
		}
	}

}
