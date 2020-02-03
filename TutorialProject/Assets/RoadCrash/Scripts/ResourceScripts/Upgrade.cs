using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

	protected bool purchased;
	public virtual void UpgradeItem(CarItem carData, CarBuyingType carbuyingResource)
	{
		switch (carbuyingResource)
		{
			case CarBuyingType.COINS:
				BuyCarWithCoins(carData);
				break;
			case CarBuyingType.GEMS:
				BuyCarWithGems(carData);
				break;
			case CarBuyingType.REWARDED_VIDEO:
				BuyCarWithAds(carData);
				break;
		}
		
	}

	protected void Upgraded(CarItem carData)
	{
		UpdateUI();

		SlotManager.instance.GenerateItemInSlot(carData.carIndex);
		Debug.Log("Upgrade - Car data is Upgraded ");
	}

	private void UpdateUI()
	{
		//GameManager.instance.resourcePanel_C.UpdatePlayerResources();
		ResourcePanel_C.UpdatePlayerResources();
		GameManager.instance.carPanel_C.SaveCarItemData(); // On every Car purchase do this.
	}

	private void BuyCarWithCoins(CarItem carData)
	{

		if (DataManager.playerData.money >= carData.carPrice)
		{
			DataManager.playerData.money -= carData.carPrice;
			// Increase the SpinWheel reward Coins.
			DataManager.playerData.spinWheelCash += (carData.carPrice / 2);
			DataManager.SaveData();
			// Increase the Price Based on Formula 
			carData.carPrice = CarPanel_C.carPriceMultiplier[carData.carIndex] + carData.carPrice + DataManager.playerData.playerLevel;
			purchased = true;
		}
	}

	private void BuyCarWithGems(CarItem carData)
	{
		if(DataManager.playerData.gems >= 4)
		{
			DataManager.playerData.gems -= 4;
			DataManager.SaveData();
			purchased = true;
		}
	}

	private void BuyCarWithAds(CarItem carData)
	{

	}
    
}
