using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPatternSpawnerController : MonoBehaviour
{
	private const string Tag = "ItemPatternSpawnerController";
	private Level Level;

	private void Awake ()
	{
		Level = LevelManager.Instance.GetCurrentLevel ();
		CreatePool ();
		SpawnPattern ();
	}

	private void CreatePool ()
	{
		// create this huge pool of shit
		for (int i = 0; i < Level.Patterns.Count; i++) {
			Pattern p = Level.Patterns [i];

			for (int j = 0; j < p.ItemNodes.Count; j++) {
				Pattern.ItemNode itemNode = p.ItemNodes [j];

				if (itemNode.ItemPrefab == null) {
					Log.LogDebug (Tag, "CreatePool, skipping empty prefab, probably random type");
					continue;
				}

				PoolManager.Instance.AddToPool (itemNode.ItemPrefab, 10);
			}
		}
	}

	private bool CheckForNewPattern ()
	{
		return false;
	}

	private GameObject GetRandomPrefab (Enums.ItemType itemType)
	{
		List<GameObject> prefabs = null;

		if (itemType == Enums.ItemType.GOAL) {
			prefabs = ItemPrefabUtil.GetAllGoalPrefabs ();
		}
		if (itemType == Enums.ItemType.DEBREE) {
			prefabs = ItemPrefabUtil.GetAllGoalPrefabs ();
		}

		if (prefabs == null) {
			Log.LogDebug (Tag, "GetRandomPrefab failed");
			return null;
		}

		int random = Random.Range (0, prefabs.Count);
		return prefabs [random];
	}

	private Pattern GetPatternFromProbability ()
	{
		int random = Random.Range (0, Level.Patterns.Count);
		return Level.Patterns [random];
	}

	private void SpawnPattern ()
	{
		Pattern pattern = GetPatternFromProbability ();
		GameObject itemPrefab;

		for (int i = 0; i < pattern.ItemNodes.Count; i++) {
			Pattern.ItemNode itemNode = pattern.ItemNodes [i];

			if (itemNode.ItemPrefab == null) {
				itemPrefab = GetRandomPrefab (itemNode.ItemType);
			} else {
				itemPrefab = itemNode.ItemPrefab;
			}

			if (itemPrefab == null) {
				Log.LogDebug (Tag, "SpawnPattern failed to get itemPrefab");
				return;
			}

			GameObject go = PoolManager.Instance.GetFromPool (itemPrefab);
			go.transform.position = itemNode.Position + pattern.RootPosition;
		}
	}

}
