using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager
{
	public Action OnGoalItemHitsSafeNet;
	public Action OnGoalItemHitsGround;

	public Action OnDebreeItemHitsSafeNet;
	public Action OnDebreeItemHitsGround;

	public Action OnGameOver;
	public Action OnLevelComplete;

	private static GameStateManager instance;

	private GameStateManager ()
	{
		Log.LogDebug ("ScoreManager Awake()");
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

	public void GoalItemHitsSafeNet ()
	{	
		NrGoalItemsCought++;

		if (OnGoalItemHitsSafeNet != null)
			OnGoalItemHitsSafeNet ();
	}

	public void GoalItemHitsGround ()
	{
		Lives--;

		if (OnGoalItemHitsGround != null)
			OnGoalItemHitsGround ();	

		CheckGameOver ();
	}

	public void DebreeItemHitsSafeNet ()
	{
		Lives--;
		NrDebreeItemsCought++;

		if (OnDebreeItemHitsSafeNet != null)
			OnDebreeItemHitsSafeNet ();

		CheckGameOver ();
	}

	public void DebreeItemHitsGround ()
	{
		if (OnDebreeItemHitsGround != null)
			OnDebreeItemHitsGround ();	
	}

	public void CheckGameOver ()
	{
		if (Lives <= 0) {
			if (OnGameOver != null) {
				OnGameOver ();
			}
		}
	}

	private void CheckLevelComplete ()
	{

	}

}
