using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Spawnable.asset", menuName = "LevelManager/Spawneble", order = 1)]
public class Spawnable : ScriptableObject
{

	public GameObject SpawnablePrefab;

	public float SpawnProbability;
	public float FallSpeed;
	public float Scale;

}
