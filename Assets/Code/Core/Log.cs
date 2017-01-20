using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Log
{

	public static bool LogEnabled = true;

	public static void SetLogEnabled (bool enabled)
	{
		LogEnabled = enabled;
	}

	public static void LogDebug (string s)
	{
		if (LogEnabled) {
			Debug.Log (s);
		}
	}

	public static void LogWarning (string s)
	{
		if (LogEnabled) {
			Debug.LogWarning (s);
		}
	}

	public static void LogError (string s)
	{
		if (LogEnabled) {
			Debug.LogError (s);
		}
	}

}
