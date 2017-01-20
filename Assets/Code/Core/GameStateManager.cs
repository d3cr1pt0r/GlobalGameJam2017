using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	private string Tag = "ScoreManager";
	private int Score;

	void Awake ()
	{
		Log.LogDebug ("ScoreManager Awake()");

		Score = 0;
	}

	public void SetScore (int score)
	{
		Score = score;
	}

	public int GetScore ()
	{
		return Score;
	}

	public void IncreaseScore (int amount)
	{
		Score += amount;
	}

	public void DecreaseScore (int amount)
	{
		Score -= amount;
	}

}
