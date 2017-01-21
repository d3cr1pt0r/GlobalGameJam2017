using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	[SerializeField] private Universe Universe;

	private void Awake ()
	{
		LevelManager.Instance.SetUniverse (Universe);
		StartGame ();
	}

	private void StartGame ()
	{
		LoadLevel ();
	}

	private void PauseGame ()
	{
	
	}

	private void StopGame ()
	{
		
	}

	private void LoadNextLevel ()
	{
		
	}

	private void LoadLevel ()
	{
		Level level = LevelManager.Instance.GetCurrentLevel ();

		if (level != null) {
			GameObject levelObject = Instantiate (level.LevelPrefab, level.LevelPosition, Quaternion.identity);
			levelObject.transform.SetParent (transform, false);
		}
	}

}
