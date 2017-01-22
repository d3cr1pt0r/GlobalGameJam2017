using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPatternSpawnerController : MonoBehaviour
{
	private const string Tag = "ItemPatternSpawnerController";
	private Level Level;

	private float bottomY;
	private Transform topMostPatternTransform;
	private bool Enabled;
	private int SpawnedPatterns;

	private void Awake ()
	{
		Level = LevelManager.Instance.GetCurrentLevel ();

		CreatePool ();
		SpawnPattern ();
		CalculateBottomYPosition ();

		Enabled = true;
		SpawnedPatterns = 0;
	}

	private void Update ()
	{
		if (Enabled) {
			if (ShouldSpawnNextPattern ()) {
				SpawnPattern ();
			}
		}
	}

	public void SetEnabled (bool enabled)
	{
		Enabled = enabled;
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

	private void CalculateBottomYPosition ()
	{
		bottomY = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).y;
	}

	private bool ShouldSpawnNextPattern ()
	{
		if (topMostPatternTransform == null) {
			return false;
		}

		float diff = Mathf.Abs (bottomY - topMostPatternTransform.position.y);

		return diff < 5.0f;
	}

	private GameObject GetRandomPrefab (Enums.ItemType itemType)
	{
		List<GameObject> prefabs = null;

		if (itemType == Enums.ItemType.GOAL) {
			prefabs = ItemPrefabUtil.GetAllGoalPrefabs ();
		}
		if (itemType == Enums.ItemType.DEBREE) {
			prefabs = ItemPrefabUtil.GetAllDebreePrefabs ();
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
		if (Level.Patterns.Count == 0) {
			Log.LogDebug (Tag, "GetPatternFromProbability no patterns to load");
			return null;
		}

		int random = Random.Range (0, Level.Patterns.Count);
		return Level.Patterns [random];
	}

	private void SpawnPattern ()
	{
		Pattern pattern = GetPatternFromProbability ();

		if (pattern == null) {
			Log.LogDebug (Tag, "SpawnPattern pattern is null");
			return;
		}

		GameObject itemPrefab;
		float highestYPosition = -1;

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
			go.transform.rotation = Quaternion.Euler (itemNode.Rotation);
			go.transform.localScale = itemNode.Scale;

			Log.LogDebug (Tag, "SpawnPattern name {0}", pattern.name);

			if (itemNode.Position.y > highestYPosition) {
				highestYPosition = itemNode.Position.y;
				topMostPatternTransform = go.transform;
			}
		}

		SpawnedPatterns++;
		CheckForSpawningDone ();
	}

	private void CheckForSpawningDone ()
	{
		if (SpawnedPatterns > Level.PatternsToGenerate) {
			GameStateManager.Instance.LevelComplete ();
		}
	}

}
