using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
	private const string Tag = "LevelManager";

	public GameObject CurrentLoadedLevel;
	public int CurrentLevel;

	private Universe Universe;
	private static LevelManager instance;

	private LevelManager ()
	{
		Log.LogDebug (Tag, "Awake");
	}

	public static LevelManager Instance {
		get {
			if (instance == null) {
				instance = new LevelManager ();
			}	
			return instance;
		}
	}

	public void SetUniverse (Universe universe)
	{
		Universe = universe;
	}

	public Level GetCurrentLevel ()
	{
		Level level = Universe.Levels [CurrentLevel];

		if (level.LevelPrefab == null) {
			Log.LogError (Tag, "Level prefab is not set to an instance of an object: {0}", CurrentLevel);
			return null;
		}

		return level;
	}

	public bool IncreaseLevel ()
	{
		CurrentLevel += 1;

		if (CurrentLevel > Universe.Levels.Count - 1) {
			Log.LogWarning (Tag, "Level {0} does't exist", CurrentLevel);
			return false;
		}

		return true;
	}

	public bool IsLastLevel ()
	{
		return CurrentLevel == Universe.Levels.Count - 1;
	}

}
