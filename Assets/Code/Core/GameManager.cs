using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	[SerializeField] private GameObject LevelManagerPrefab;
	[SerializeField] private GameObject ScoreManagerPrefab;

	[HideInInspector] public LevelManager LevelManager;
	[HideInInspector] public GameStateManager GameStateManager;

	private void Awake ()
	{
		Log.LogDebug ("GameManager Awake()");

		GameObject levelManagerObject = Instantiate (LevelManagerPrefab, Vector3.zero, Quaternion.identity);
		GameObject scoreManagerObject = Instantiate (ScoreManagerPrefab, Vector3.zero, Quaternion.identity);

		levelManagerObject.transform.parent = transform;
		scoreManagerObject.transform.parent = transform;

		LevelManager = levelManagerObject.GetComponent<LevelManager> ();
		GameStateManager = scoreManagerObject.GetComponent<GameStateManager> ();
	}

	private void StartGame ()
	{
		
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

}
