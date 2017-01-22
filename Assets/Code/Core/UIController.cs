using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{

	[SerializeField] private GameObject MainPanel;
	[SerializeField] private GameObject MainMenuPanel;
	[SerializeField] private GameObject GameOverPanel;
	[SerializeField] private GameObject ScorePanel;

	[SerializeField] private Text TextScore;
	[SerializeField] private Text TextLives;

	protected UIController ()
	{
		
	}

	public void OnButtonPlayClick ()
	{
		Game.Instance.LoadNextLevel ();
		SetMainMenuEnabled (false);
		SetScorePanelEnabled (true);
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

	public void ShowGameOverDialog ()
	{
		SetMainMenuEnabled (false);
		SetGameOverDialogEnabled (true);
	}

	public void SetScorePanelEnabled (bool enabled)
	{
		ScorePanel.SetActive (enabled);
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
