using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
	private const string Tag = "GameManager";

	[SerializeField] private Universe Universe;
	[SerializeField] private Transform LevelContainer;
	[SerializeField] private bool test = false;

	[SerializeField] private CharacterController CharacterControllerP1;
	[SerializeField] private CharacterController CharacterControllerP2;

	public float Parallax;
	public GameObject CameraHolder;

	protected Game ()
	{
	}

	private void Awake ()
	{
		Log.LogDebug (Tag, "Awake");

		GameStateManager.Instance.OnGameOver += GameOver;

		LevelManager.Instance.SetUniverse (Universe);
		UIController.Instance.SetMainMenuEnabled (true);
		StartGame ();
	}

	private void StartGame ()
	{
		LoadCurrentLevel ();
	}

	private void PauseGame ()
	{
	
	}

	private void StopGame ()
	{
		
	}

	private void GameOver ()
	{
		UIController.Instance.SetScoreGameOver (GameStateManager.Instance.Score);
		UIController.Instance.ShowGameOverDialog ();
		LevelManager.Instance.ResetCurrentLevel ();
		LevelManager.Instance.CurrentLoadedLevel.GetComponent<ItemPatternSpawnerController> ().SetEnabled (false);

		CharacterControllerP1.SetControlsActive (false);
		CharacterControllerP2.SetControlsActive (false);
	}

	public void Quit ()
	{
		StopGame ();
		Application.Quit ();
	}

	public void LoadNextLevel ()
	{
		LevelManager.Instance.IncreaseLevel ();
		GameStateManager.Instance.UpdateUI ();

		PoolManager.Instance.DestroyPool ();
		UnloadCurrentLevel ();

		if (!test) {
			LoadCurrentLevel ();
		}
	}

	private void LoadCurrentLevel ()
	{
		Level level = LevelManager.Instance.GetCurrentLevel ();

		if (level != null) {
			CharacterControllerP1.transform.position = level.Player1SpawnPoint.position;
			CharacterControllerP2.transform.position = level.Player2SpawnPoint.position;

			CharacterControllerP1.Reset ();
			CharacterControllerP2.Reset ();

			CharacterControllerP1.SetControlsActive (true);
			CharacterControllerP2.SetControlsActive (true);

			GameStateManager.Instance.SetLives (level.Lives);

			GameObject levelObject = Instantiate (level.LevelPrefab, level.LevelPosition, Quaternion.identity);
			levelObject.transform.SetParent (LevelContainer, false);
			LevelManager.Instance.CurrentLoadedLevel = levelObject;
		}
	}

	private void UnloadCurrentLevel ()
	{
		if (LevelManager.Instance.CurrentLoadedLevel != null) {
			GameObject.DestroyImmediate (LevelManager.Instance.CurrentLoadedLevel);
			LevelManager.Instance.CurrentLoadedLevel = null;
		}
	}

}
