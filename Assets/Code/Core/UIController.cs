using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
	private const string Tag = "UIController";

	[SerializeField] private GameObject MainPanel;
	[SerializeField] private GameObject MainMenuPanel;
	[SerializeField] private GameObject GameOverPanel;
	[SerializeField] private GameObject GameContinuePanel;
	[SerializeField] private GameObject ScorePanel;

	[SerializeField] private Text TextScore;
	[SerializeField] private Text TextLives;
	[SerializeField] private Text TextScoreGameOver;

	protected UIController ()
	{
		
	}

	public void OnButtonPlayClick ()
	{
		AudioController.Instance.PlayMusic (MusicType.GameOver, false);
		AudioController.Instance.PlayMusic (MusicType.Full);

		SetMainMenuEnabled (false);
		SetGameOverDialogEnabled (false);
		SetContinuePanelEnabled (false);
		SetScorePanelEnabled (true);

		Game.Instance.LoadNextLevel ();
	}

	public void OnButtonQuitClick ()
	{
		Game.Instance.Quit ();
	}

	public void SetScore (int score)
	{
		TextScore.text = "Score: " + score.ToString ();
	}

	public void SetLives (int lives)
	{
		TextLives.text = "Lives: " + lives.ToString ();
	}

	public void SetScoreGameOver (int score)
	{
		TextScoreGameOver.text = "Score: " + score;
	}

	public void ShowGameOverDialog ()
	{
		SetMainMenuEnabled (false);
		SetScorePanelEnabled (false);
		SetGameOverDialogEnabled (true);
	}

	public void SetScorePanelEnabled (bool enabled)
	{
		ScorePanel.SetActive (enabled);
	}

	public void SetContinuePanelEnabled (bool enabled)
	{
		GameContinuePanel.SetActive (enabled);
	}

	public void SetGameOverDialogEnabled (bool enabled)
	{
		GameOverPanel.SetActive (enabled);
	}

	public void SetMainMenuEnabled (bool enabled)
	{
		MainMenuPanel.SetActive (enabled);
	}

	public void SetUIEnabled (bool enabled)
	{
		MainPanel.SetActive (enabled);
	}

}
