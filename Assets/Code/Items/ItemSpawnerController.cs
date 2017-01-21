using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerController : MonoBehaviour
{
	public static string Tag = "SpawnerController";

	public float SpawnRateFrom;
	public float SpawnRateTo;
	public List<Spawnable> Spawnables;

	public bool Enabled { get; private set; }

	private float Timer;
	private float NextSpawnRate;
	private int PoolSize;

	public void SetEnabled (bool enabled)
	{
		Enabled = enabled;
	}

	private void Awake ()
	{
		Log.LogDebug (Tag, "Awake");

		NextSpawnRate = Random.Range (SpawnRateFrom, SpawnRateTo);
		Enabled = true;
		CreatePool ();
	}

	private void Update ()
	{
		if (Enabled) {
			Timer += Time.deltaTime;

			if (Timer >= NextSpawnRate) {
				Spawn ();
				NextSpawnRate = Random.Range (SpawnRateFrom, SpawnRateTo);
				Timer = 0.0f;
			}
		}
	}

	private void CreatePool ()
	{
		for (int i = 0; i < Spawnables.Count; i++) {
			PoolManager.Instance.AddToPool (Spawnables [i].SpawnablePrefab, 10);
		}
	}

	private Spawnable GetSpawnableFromProbability ()
	{
		List<Spawnable> qualifiedSpawnable = new List<Spawnable> ();

		for (int i = 0; i < Spawnables.Count; i++) {
			float random = Random.Range (0.0f, 1.0f);

			if (random <= Spawnables [i].SpawnProbability) {
				qualifiedSpawnable.Add (Spawnables [i]);
			}
		}

		if (qualifiedSpawnable.Count > 0) {
			return qualifiedSpawnable [Random.Range (0, qualifiedSpawnable.Count)];
		}

		return null;
	}

	private void Spawn ()
	{
		Spawnable spawnable = GetSpawnableFromProbability ();

		if (spawnable != null) {
			GameObject go = PoolManager.Instance.GetFromPool (spawnable.SpawnablePrefab);
			go.transform.position = transform.position;
			go.transform.localScale = Vector3.one * spawnable.Scale;
		}
	}

}
