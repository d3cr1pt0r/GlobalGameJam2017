using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	[SerializeField] private GameObject LevelManagerPrefab;
	[HideInInspector] public LevelManager LevelManager;

	private void Awake ()
	{
		Log.LogDebug ("GameManager Awake()");

		GameObject levelManagerObject = Instantiate (LevelManagerPrefab, Vector3.zero, Quaternion.identity);
		levelManagerObject.transform.parent = transform;
		LevelManager = levelManagerObject.GetComponent<LevelManager> ();
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
		GameObject levelObject = Instantiate (level.LevelPrefab, level.LevelPosition, Quaternion.identity);

		levelObject.transform.SetParent (transform, false);
	}

}
