using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{

	public Universe Universe;
	public GameObject CurrentLoadedLevel;
	public int CurrentLevel;

	private static LevelManager instance;

	private LevelManager ()
	{
		Log.LogDebug ("LevelManager Awake()");
	}

	public static LevelManager Instance {
		get {
			if (instance == null) {
				instance = new LevelManager ();
			}	
			return instance;
		}
	}

	public Level GetCurrentLevel ()
	{
		return Universe.Levels [CurrentLevel];
	}

	public bool IncreaseLevel ()
	{
		CurrentLevel += 1;

		if (CurrentLevel > Universe.Levels.Count - 1) {
			Log.LogWarning (string.Format ("Level {0} does't exist", CurrentLevel));
			return false;
		}

		return true;
	}

	public bool IsLastLevel ()
	{
		return CurrentLevel == Universe.Levels.Count - 1;
	}

}
