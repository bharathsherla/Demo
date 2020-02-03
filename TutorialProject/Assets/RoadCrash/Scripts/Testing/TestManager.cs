using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
	#region Variables

	#endregion

	#region Public Methods

	public void AddCoins()
	{
		DataManager.playerData.money += 100000;
		DataManager.playerData.spinWheelCash = 3500;
		DataManager.SaveData();

		//GameManager.instance.resourcePanel_C.UpdatePlayerResources();
		ResourcePanel_C.UpdatePlayerResources();
	}

	public void AddGems()
	{
		DataManager.playerData.gems += 500;
		DataManager.SaveData();

		//GameManager.instance.resourcePanel_C.UpdatePlayerResources();
		ResourcePanel_C.UpdatePlayerResources();
	}
	#endregion

	#region Private Methods

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
		//	DataManager.playerData.money = 100;
		//	DataManager.SaveData();
			//IncreasePlayerUnlockedCarIndex();
		}
	}
	private void IncreasePlayerUnlockedCarIndex()
	{
		DataManager.playerData.playerUnlockedCarIndex += 1;
		DataManager.upgradableItems.carItems[DataManager.playerData.playerUnlockedCarIndex].isUnlocked = true;
		DataManager.SaveData();

		Debug.Log("TestManager, test Player Car Unlocked increased to " + DataManager.playerData.playerUnlockedCarIndex);
	}
	#endregion
}
