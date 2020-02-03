using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This Class will Hold All the Player Data.
/// </summary>
[System.Serializable]
public class PlayerData 
{
	#region Variables
	public string playerName;
	public bool isUserPlayedTutorial;
	public long money;
	public long gems;
	public long spinWheelCash;
	public bool canShowAds;
	public int playerLevel;
	public long playerXpCount;
	public int playerUnlockedCarIndex; // this will range from 0 to total cars User Unlocked through merge system.


	public List<SlotData> slotData = new List<SlotData>();
	// Add the Upgradable Data here.
	#endregion
}

[System.Serializable]
public class UpgradableItems
{
	 // Store All the Upgradable items in this class.
	public List<CarItem> carItems = new List<CarItem>();
}

[System.Serializable]
public class GameSettings
{
	public bool isSoundOn = true;
	public bool isVibrationOn = true;
}

[System.Serializable]
public class SlotData
{
	public int slotItemId = -1;
}

