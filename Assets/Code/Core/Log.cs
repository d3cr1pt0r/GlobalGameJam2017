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

	public static void LogDebug (string tag, string s, params object[] args)
	{
		if (LogEnabled) {
			Debug.Log (tag + ": " + string.Format (s, args));
		}
	}

	public static void LogWarning (string tag, string s, params object[] args)
	{
		if (LogEnabled) {
			Debug.LogWarning (tag + ": " + string.Format (s, args));
		}
	}

	public static void LogError (string tag, string s, params object[] args)
	{
		if (LogEnabled) {
			Debug.LogError (tag + ": " + string.Format (s, args));
		}
	}

}
