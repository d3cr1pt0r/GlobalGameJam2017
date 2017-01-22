using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
	private const string Tag = "GameManager";

	[SerializeField] private Universe Universe;
	public float Parallax;
	public GameObject CameraHolder;
	public UIController UIController;

	protected Game ()
	{
	}

	private void Awake ()
	{
		Log.LogDebug (Tag, "Awake");

		GameStateManager.Instance.OnGameOver += GameOver;

		LevelManager.Instance.SetUniverse (Universe);
		UIController.SetMainMenuEnabled (true);
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
		UIController.Instance.ShowGameOverDialog ();
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
		LoadCurrentLevel ();
	}

	private void LoadCurrentLevel ()
	{
		Level level = LevelManager.Instance.GetCurrentLevel ();

		GameStateManager.Instance.SetLives (level.Lives);

		if (level != null) {
			GameObject levelObject = Instantiate (level.LevelPrefab, level.LevelPosition, Quaternion.identity);
			levelObject.transform.SetParent (transform, false);
			LevelManager.Instance.CurrentLoadedLevel = levelObject;
		}
	}

	private void UnloadCurrentLevel ()
	{
		if (LevelManager.Instance.CurrentLoadedLevel != null) {
			GameObject.Destroy (LevelManager.Instance.CurrentLoadedLevel);
			LevelManager.Instance.CurrentLoadedLevel = null;
		}
	}

}
