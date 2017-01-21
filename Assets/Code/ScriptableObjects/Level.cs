using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Level.asset", menuName = "LevelManager/Level", order = 1)]
public class Level : ScriptableObject
{
	
	public GameObject LevelPrefab;
	public Vector3 LevelPosition;

	public int Goal;

}
