using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Universe.asset", menuName = "LevelManager/Universe", order = 1)]
public class Universe : ScriptableObject
{
	public List<Level> Levels;
}