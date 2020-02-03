using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using Doozy.Engine.Nody;

public class CarPanel_C : MonoBehaviour
{

	#region Variables
	public List<Car_C> cars = new List<Car_C>();

	public static List<long> carPriceMultiplier = new List<long>()
	{
		20,
		100,
		400,
		1500,
		5500,
		20000,
		72500,
		262500,
		950000,
		3437500,
		12437500,
		45000000,
		162812500,
		589062500,
		2131250000,
		7710937500,
		27898437500,
		100937500000,
		365195312500,
		1321289062500,
		4780468750000,
		17295898437500,
		62577148437500,
		226406250000000,
		819145507812500,
	};
	#endregion


	#region Public Methods

	public void Init()
	{
		if (DataManager.upgradableItems != null)
			LoadCarItemData();
	}
	/// <summary>
	///  Add this method to close Button in CarBuying Panel.
	/// </summary>
	public void HideCarPanelView()
	{
		SaveCarItemData();
		this.gameObject.GetComponent<UIView>().Hide();
	}


	public void SaveCarItemData()
	{
		DataManager.upgradableItems.carItems.Clear();

		for (int i = 0; i < cars.Count; i++)
		{
			DataManager.upgradableItems.carItems.Add(cars[i].carData); // Add the Car Data to the Upgradable Data and Update the UI.
			cars[i].UpdateUI(cars[i].carData, cars[i].carBuyingType);
		}
		DataManager.SaveData();
		LoadCarItemData();
	}

	/// <summary>
	///  Buy the Car from Slot Manager based on the Random Number.
	/// </summary>
	/// <param name="index"></param>
	public void BuyCar(int index)
	{
		cars[index].UpgradeCar();
	}

	#endregion



	#region Private Methods

	/// <summary>
	///  Laod the Car Item Data to All childerns from the Updgradable Data on Start
	/// </summary>
	private void LoadCarItemData()
	{
		if (DataManager.upgradableItems.carItems.Count > 0)
			for (int i = 0; i < cars.Count; i++)
			{


				if (DataManager.playerData.playerUnlockedCarIndex >= 4 && DataManager.upgradableItems.carItems[i].isUnlocked)
				{
					if (i == DataManager.playerData.playerUnlockedCarIndex)
					{
						// Unlock the car
						cars[i].UpdateUI(DataManager.upgradableItems.carItems[i], CarBuyingType.LOCKED);
					}
					else if (i == DataManager.playerData.playerUnlockedCarIndex - 1)
					{
						// Buy car with Video Ads
						cars[i].UpdateUI(DataManager.upgradableItems.carItems[i], CarBuyingType.REWARDED_VIDEO);
					}
					else if (i == DataManager.playerData.playerUnlockedCarIndex - 2)
					{
						// Buy with Gem
						cars[i].UpdateUI(DataManager.upgradableItems.carItems[i], CarBuyingType.GEMS);
					}
					else
					{
						// Buy with Coins
						cars[i].UpdateUI(DataManager.upgradableItems.carItems[i], CarBuyingType.COINS);
					}
				}
				else
				{
					//if (DataManager.upgradableItems.carItems[i].isUnlocked)
					//{
					//	cars[i].UpdateUI(DataManager.upgradableItems.carItems[i], CarBuyingType.COINS);
					//}
					//else
					//{
					//	cars[i].UpdateUI(DataManager.upgradableItems.carItems[i], CarBuyingType.LOCKED);
					//}


					if(DataManager.upgradableItems.carItems[i].isUnlocked && i == 0)
					{
						cars[i].UpdateUI(DataManager.upgradableItems.carItems[i], CarBuyingType.COINS);
					}
					else
					{
						cars[i].UpdateUI(DataManager.upgradableItems.carItems[i], CarBuyingType.LOCKED);
					}
				}

			}
	}

	private void OnEnable()
	{
		if(DataManager.upgradableItems != null)
			LoadCarItemData();
	}

	
	#endregion




}
