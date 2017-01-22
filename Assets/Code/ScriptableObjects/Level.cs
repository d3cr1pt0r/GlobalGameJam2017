using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Level.asset", menuName = "LevelManager/Level", order = 1)]
public class Level : ScriptableObject
{
    public Transform Player1SpawnPoint;
    public Transform Player2SpawnPoint;
	public GameObject LevelPrefab;
	public Vector3 LevelPosition;
	public int Lives = 10;

	public List<Pattern> Patterns;

}
