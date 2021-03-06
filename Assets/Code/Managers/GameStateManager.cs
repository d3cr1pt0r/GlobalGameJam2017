﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager
{
	private const string Tag = "GameStateManager";

	public Action OnGoalItemHitsSafeNet;
	public Action OnGoalItemHitsGround;

	public Action OnDebreeItemHitsSafeNet;
	public Action OnDebreeItemHitsGround;

	public Action OnGameOver;
	public Action OnLevelComplete;

	private static GameStateManager instance;

	private GameStateManager ()
	{
		Log.LogDebug (Tag, "Awake");
	}

	public static GameStateManager Instance {
		get {
			if (instance == null) {
				instance = new GameStateManager ();
			}	
			return instance;
		}
	}

	public int Lives { get; private set; }

	public int Score { get; private set; }

	public int NrGoalItemsCought { get; private set; }

	public int NrDebreeItemsCought { get; private set; }

	public void SetLives (int lives)
	{
		Lives = lives;
		UpdateUI ();
	}

	public void UpdateUI ()
	{
		UIController.Instance.SetScore (Score);
		UIController.Instance.SetLives (Lives);
	}

	public void GoalItemHitsSafeNet ()
	{	
		NrGoalItemsCought++;
		Score += 10;

		AudioController.Instance.PlayMusic (MusicType.CollectedSound);

		UpdateUI ();

		Log.LogDebug (Tag, "GoalItemsHitsSafeNet NrGoalItemsCought: {0}", NrGoalItemsCought);

		if (OnGoalItemHitsSafeNet != null)
			OnGoalItemHitsSafeNet ();
	}

	public void GoalItemHitsGround ()
	{
		AudioController.Instance.PlayMusic (MusicType.FailSound);

		Lives--;

		UpdateUI ();

		UIController.Instance.SetLives (Lives);

		Log.LogDebug (Tag, "GoalItemHitsGround Lives: {0}", Lives);

		if (OnGoalItemHitsGround != null)
			OnGoalItemHitsGround ();	

		CheckGameOver ();
	}

	public void DebreeItemHitsSafeNet ()
	{
		AudioController.Instance.PlayMusic (MusicType.FailSound);

		Lives--;
		NrDebreeItemsCought++;

		UpdateUI ();

		Log.LogDebug (Tag, "GoalItemHitsGround NrDebreeItemsCought: {0} Lives: {1}", NrDebreeItemsCought, Lives);

		if (OnDebreeItemHitsSafeNet != null)
			OnDebreeItemHitsSafeNet ();

		CheckGameOver ();
	}

	public void DebreeItemHitsGround ()
	{
		Log.LogDebug (Tag, "DebreeItemHitsGround");

		AudioController.Instance.PlayMusic (MusicType.FailSound);

		if (OnDebreeItemHitsGround != null)
			OnDebreeItemHitsGround ();	
	}

	public void LevelComplete ()
	{
		Log.LogDebug (Tag, "LevelComplete");

		if (OnLevelComplete != null)
			OnLevelComplete ();
	}

	public void CheckGameOver ()
	{
		if (Lives <= 0 && Game.Instance.IsPlaying) {
			if (OnGameOver != null) {
				Log.LogDebug (Tag, "OnGameOver");
				OnGameOver ();
			}
		}
	}

	public void ResetScore ()
	{
		Score = 0;
		UpdateUI ();
	}

}
