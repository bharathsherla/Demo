using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This Class just Hold the Data of Xp Level 
/// </summary>
public class XpData 
{
	private static int playerCurrentXpLevel; // Player Xp Level.
	private static long playerXpCount; // Total Xp Count Player Holds.


	public static long[] xpDifference = {
								    40,
									60,
									120,
									240,
									480,
									960,
									1920,
									3840,
									7680,
									15360,
									30720,
									61440,
									122880,
									245760,
									491520,
									983040,
									1966080,
									3932160,
									7864320,
									15728640,
									31457280,
									62914560,
									125829120,
									251658240,
									503316480,
									1006632960,
									2013265920,
									4026531840,
									8053063680,
									16106127360,
									32212254720,
									64424509440,
									128849018880,
									257698037760,
									515396075520,
									1030792151040,
									2061584302080,
									4123168604160,
								   };


	public static long PlayerXpCount
	{
		get { return playerXpCount; }
	}
	public static int PlayerXpLevel
	{
		get { return playerCurrentXpLevel; }
	}
	/// <summary>
	///  Get the Xp Count needed to Increase Xp Level.
	/// </summary>
	/// <returns></returns>
	public static long GetRequiredXpCount()
	{
		return xpDifference[playerCurrentXpLevel];
	}

	/// <summary>
	///  Once Game Starts Set the Values 
	/// </summary>
	/// <param name="xpLevel"></param>
	/// <param name="xpCount"></param>
	public static void SetPlayerXpLevel(int xpLevel, long xpCount)
	{
		playerCurrentXpLevel = xpLevel;
		playerXpCount = xpCount;
	}

	public static void SetPlayerXpCount(long xpCount)
	{
		playerXpCount += xpCount;
		DataManager.playerData.playerXpCount += xpCount;
		DataManager.SaveData();
	}
	public static void SetPlayerXpLevel(int xpLevel)
	{
		playerCurrentXpLevel = xpLevel;
		DataManager.playerData.playerLevel = xpLevel;
		DataManager.SaveData();
	}

	public static void ResetXpCount(long xpCount)
	{
		playerXpCount = xpCount;
		DataManager.playerData.playerXpCount = xpCount;
		DataManager.SaveData();
	}
}
