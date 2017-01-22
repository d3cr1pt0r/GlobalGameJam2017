using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ItemPrefabUtil
{
	private const string Tag = "ItemPrefabUtil";
	public static string ItemPrefabsPath = "Assets/Resources/Prefabs/Items";
	public static string ItemPrefabsResourcesPath = "Prefabs/Items/";

	public static List<GameObject> GetAllGoalPrefabs ()
	{
		List<GameObject> prefabs = new List<GameObject> ();
		string[] items = Directory.GetFiles (ItemPrefabUtil.ItemPrefabsPath);

		for (int i = 0; i < items.Length; i++) {
			string path = items [i];
			string name = Path.GetFileNameWithoutExtension (path);
			if (name.StartsWith ("goal_") && !name.Contains ("goal_proxy") && path.EndsWith (".prefab")) {
				//GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> (path) as GameObject;
				GameObject prefab = Resources.Load<GameObject> (ItemPrefabsResourcesPath + name) as GameObject;

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
				//GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> (path) as GameObject;
				GameObject prefab = Resources.Load<GameObject> (ItemPrefabsResourcesPath + name) as GameObject;

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
		string path = ItemPrefabsResourcesPath + "debree_proxy";
		GameObject prefab = Resources.Load<GameObject> (path);

		if (prefab == null) {
			Log.LogDebug (Tag, "GetDebreeProxy failed to load prefab at path {0}", path);
		}

		return prefab;
	}

	public static GameObject GetGoalProxy ()
	{
		string path = ItemPrefabsResourcesPath + "goal_proxy";
		GameObject prefab = Resources.Load<GameObject> (path);

		if (prefab == null) {
			Log.LogDebug (Tag, "GetDebreeProxy failed to load prefab at path {0}", path);
		}

		return prefab;
	}

}
