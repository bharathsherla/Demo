using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
///  This Class Need to update the Player XpData 
/// </summary>
public class XpManager 
{
	public static Action<int, long, long, bool> OnXpCountUpdated;
	
	private static int playerCurrentXpLevel;
	private static long playerXpCount;
	private static bool hasIncreasePlayerXpLevel;


	#region Public Methods


	public static void Initialize()
	{
		XpData.SetPlayerXpLevel(DataManager.playerData.playerLevel, DataManager.playerData.playerXpCount);
	}
	public static int PlayerXpLevel
	{
		get { return playerCurrentXpLevel; }
	}

	public static long PlayerXpCount
	{
		get { return playerXpCount; }
	}
	public static void UpdarePlayerXp(long count)
	{
		XpData.SetPlayerXpCount(count);
		CalCulateXp();
	}

	public static float GetPlayerXpRatio()
	{
		return (float)XpData.PlayerXpCount / (float)XpData.GetRequiredXpCount();
	}

	#endregion

	#region Private Methods

	private static void CalCulateXp()
	{
		if (XpData.PlayerXpCount - XpData.GetRequiredXpCount() >= 1)
		{
			// Increase Xp number.
			XpData.ResetXpCount(XpData.PlayerXpCount - XpData.GetRequiredXpCount());
			XpData.SetPlayerXpLevel(XpData.PlayerXpLevel + 1);
			hasIncreasePlayerXpLevel = true;
		}
		else
			hasIncreasePlayerXpLevel = false;
		if (OnXpCountUpdated != null)
			OnXpCountUpdated(XpData.PlayerXpLevel, XpData.PlayerXpCount, XpData.GetRequiredXpCount(),hasIncreasePlayerXpLevel);
		Debug.Log("XpManager - Player Xp Level - " + XpData.PlayerXpLevel + " Player Xp Count - " + XpData.PlayerXpCount + " RequiredXp - " + XpData.GetRequiredXpCount());
	}
	#endregion
	
}
