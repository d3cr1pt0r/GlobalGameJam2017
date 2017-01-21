using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
	private const string Tag = "PoolManager";

	private static PoolManager instance;
	private Dictionary<string, List<GameObject>> PoolInactive;
	private Dictionary<string, List<GameObject>> PoolActive;
	private GameObject rootPoolObject;
	private GameObject activePoolObject;
	private GameObject inactivePoolObjects;

	private PoolManager ()
	{
		Log.LogDebug (Tag, "Awake");

		PoolInactive = new Dictionary<string, List<GameObject>> ();
		PoolActive = new Dictionary<string, List<GameObject>> ();

		rootPoolObject = new GameObject ("PoolManager");
		inactivePoolObjects = new GameObject ("Inactive");
		activePoolObject = new GameObject ("Active");

		inactivePoolObjects.transform.SetParent (rootPoolObject.transform);
		activePoolObject.transform.SetParent (rootPoolObject.transform);
	}

	public static PoolManager Instance {
		get {
			if (instance == null) {
				instance = new PoolManager ();
			}
			return instance;
		}
	}

	public void AddToPool (GameObject go, int n)
	{
		string name = GetName (go);

		for (int i = 0; i < n; i++) {
			GameObject g = GameObject.Instantiate (go, Vector3.zero, Quaternion.identity);
			g.SetActive (false);
			g.transform.SetParent (inactivePoolObjects.transform);

			if (!PoolInactive.ContainsKey (name)) {
				PoolInactive.Add (name, new List<GameObject> ());
			}
			if (!PoolActive.ContainsKey (name)) {
				PoolActive.Add (name, new List<GameObject> ());
			}

			PoolInactive [name].Add (g);
		}
	}

	public GameObject GetFromPool (GameObject go)
	{
		string name = GetName (go);

		if (PoolInactive.ContainsKey (name)) {
			if (PoolInactive [name].Count > 0) {
				GameObject g = PoolInactive [name] [0];
				g.SetActive (true);
				g.transform.SetParent (activePoolObject.transform);
				PoolInactive [name].Remove (g);
				PoolActive [name].Add (g);
				Log.LogDebug (Tag, "GetFromPool {0}", g.name);
				return g;
			}
			Log.LogDebug (Tag, "Pool {0} ran out of objects", name);
		}
		Log.LogDebug (Tag, "Pool does not contain a key {0}", name);

		return null;
	}

	public void ReturnToPool (GameObject go)
	{
		string name = GetName (go);

		if (PoolActive.ContainsKey (name)) {
			for (int i = 0; i < PoolActive [name].Count; i++) {
				GameObject g = PoolActive [name] [i];

				if (g == go) {
					g.SetActive (false);
					g.transform.SetParent (inactivePoolObjects.transform);
					PoolActive [name].Remove (g);
					PoolInactive [name].Add (g);
					Log.LogDebug (Tag, "ReturnToPool {0}", g.name);
					break;
				}
			}
		}
	}

	private string GetName (GameObject go)
	{
		return go.name.Replace ("(Clone)", "");
	}

}
