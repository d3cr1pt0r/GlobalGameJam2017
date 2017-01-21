using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;


public class ItemPrefabUtil
{
	private const string Tag = "ItemPrefabUtil";
	public static string ItemPrefabsPath = "Assets/Resources/Prefabs/Items";

	public static List<GameObject> GetAllGoalPrefabs ()
	{
		List<GameObject> prefabs = new List<GameObject> ();
		string[] items = Directory.GetFiles (ItemPrefabUtil.ItemPrefabsPath);

		for (int i = 0; i < items.Length; i++) {
			string path = items [i];
			string name = Path.GetFileNameWithoutExtension (path);
			if (name.StartsWith ("goal_") && !name.Contains ("goal_proxy") && path.EndsWith (".prefab")) {
				GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> (path) as GameObject;

				if (prefab != null) {
					prefabs.Add (prefab);
				} else {
					Log.LogDebug (Tag, "GetAllGoalPrefabs failed to LoadAssetAtPath {0}", path);
				}
			}
		}

		return prefabs;
	}

	public static List<GameObject> GetAllDebreePrefabs ()
	{
		List<GameObject> prefabs = new List<GameObject> ();
		string[] items = Directory.GetFiles (ItemPrefabUtil.ItemPrefabsPath);

		for (int i = 0; i < items.Length; i++) {
			string path = items [i];
			string name = Path.GetFileNameWithoutExtension (path);
			if (name.StartsWith ("debree_") && !name.Contains ("debree_proxy") && path.EndsWith (".prefab")) {
				GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> (path) as GameObject;

				if (prefab != null) {
					prefabs.Add (prefab);
				} else {
					Log.LogDebug (Tag, "GetAllGoalPrefabs failed to LoadAssetAtPath {0}", path);
				}
			}
		}

		return prefabs;
	}

	public static GameObject GetDebreeProxy ()
	{
		string path = ItemPrefabsPath + "/debree_proxy.prefab";

		if (!File.Exists (path)) {
			Log.LogDebug (Tag, "GetDebreeProxy file does not exist {0}", path);
			return null;
		}

		return AssetDatabase.LoadAssetAtPath<GameObject> (path) as GameObject;
	}

	public static GameObject GetGoalProxy ()
	{
		string path = ItemPrefabsPath + "/goal_proxy.prefab";

		if (!File.Exists (path)) {
			Log.LogDebug (Tag, "GetDebreeProxy file does not exist {0}", path);
			return null;
		}

		return AssetDatabase.LoadAssetAtPath<GameObject> (path) as GameObject;
	}

}
