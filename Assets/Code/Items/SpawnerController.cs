using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
	public float SpawnRate;
	public List<Spawnable> Spawnables;

	public bool Enabled { get; private set; }

	private float Timer;
	private int PoolSize;

	public void SetEnabled (bool enabled)
	{
		Enabled = enabled;
	}

	private void Awake ()
	{
		CreatePool ();
	}

	private void Update ()
	{
		if (Enabled) {
			Timer += Time.deltaTime;

			if (Timer >= SpawnRate) {
				Spawn ();
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

	private void Spawn ()
	{
		List<GameObject> qualifiedGO = new List<GameObject> ();

		for (int i = 0; i < Spawnables.Count; i++) {
			float random = Random.Range (0.0f, 1.0f);

			if (random <= Spawnables [i].SpawnProbability) {
				qualifiedGO.Add (Spawnables [i].SpawnablePrefab);
			}
		}

		if (qualifiedGO.Count > 0) {
			GameObject qualified = qualifiedGO [Random.Range (0, qualifiedGO.Count)];
			GameObject go = PoolManager.Instance.GetFromPool (qualified);
			go.transform.position = transform.position;
		}
	}

}
