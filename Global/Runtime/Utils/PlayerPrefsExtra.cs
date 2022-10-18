using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsExtra
{
	#region Bool -----------------------------------------------------------------------------------------

	public static bool GetBool(string key)
	{
		return (PlayerPrefs.GetInt(key, 0) == 1);
	}

	public static bool GetBool(string key, bool defaultValue)
	{
		return (PlayerPrefs.GetInt(key, (defaultValue ? 1 : 0)) == 1);
	}

	public static void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, (value ? 1 : 0));
	}
    #endregion
}
