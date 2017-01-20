using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

	public Universe Universe;
	public int CurrentLevel;
	public GameObject CurrentLoadedLevel;

	private void Awake ()
	{
		Log.LogDebug ("LevelManager Awake()");
	}

	public void LoadLevel (int index)
	{
		if (index < 0 || index > Universe.Levels.Count) {
			Log.LogWarning (string.Format ("Level {0} does't exist", CurrentLevel));
			return;
		}

		CurrentLevel = index;
		Level level = Universe.Levels [CurrentLevel];
		CurrentLoadedLevel = Instantiate (level.LevelPrefab, level.LevelPosition, Quaternion.identity);
	}

	public void LoadNextLevel ()
	{
		CurrentLevel += 1;

		if (CurrentLevel > Universe.Levels.Count - 1) {
			Log.LogWarning (string.Format ("Level {0} does't exist", CurrentLevel));
			return;
		}

		LoadLevel (CurrentLevel);
	}

	public bool IsLastLevel ()
	{
		return CurrentLevel == Universe.Levels.Count - 1;
	}

	public int GetCurrentLevel ()
	{
		return CurrentLevel;
	}

}
