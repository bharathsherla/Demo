using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CarBuyingType
{
	COINS,
	GEMS,
	REWARDED_VIDEO,
	LOCKED, // Always Lock the Player Current Car.
}
public class Car_C : Upgrade
{

	#region Variables
		public CarItem carData; // This will Contain All Car Data like price, level etc..


	[Header(" Item Details")]
	public Image carImage;
	public Image buyingIcon;
	public Text carPriceText;

	[Header("Car Item Resources")]
	public Sprite carlockedIcon, carUnlockedIcon;
	public Sprite carbuyingIcon_Coins, carBuyingIcon_Gems, carBuyingIcon_video;
	public Sprite buyButton_On, buyButton_Off;

	public CarBuyingType carBuyingType;
	public Button buyButton; // Incase if the Car is Unlocked Disable interaction and change the Button Image.
	public Image buyButtonImage;
	public Text buyButtonText;
	public GameObject lockedObject, adsObject, gemsObject, coinsObject; // Based on Car Purchase Type Show the Content in Button.
	#endregion


	#region Public Methods

	/// <summary>
	///  Once CarPanel is Open Update ALl the Cars UI based on the CarData.
	/// </summary>
	public void UpdateUI(CarItem carItem, CarBuyingType carBuyingType)
	{
		 this.carData = carItem;
		 this.carBuyingType = carBuyingType;
		if (!carData.isUnlocked || this.carBuyingType == CarBuyingType.LOCKED) // means car is Locked
		{
			UpdateUIOnCarLocked();
		}
		else
		{
			if(carBuyingType == CarBuyingType.COINS)
			{
				buyingIcon.sprite = carbuyingIcon_Coins;
				coinsObject.SetActive(true);
				adsObject.SetActive(false);
				gemsObject.SetActive(false);
			}
			else if(carBuyingType == CarBuyingType.GEMS)
			{
				buyingIcon.sprite = carBuyingIcon_Gems;
				coinsObject.SetActive(false);
				adsObject.SetActive(false);
				gemsObject.SetActive(true);
			}
			else if(carBuyingType == CarBuyingType.REWARDED_VIDEO)
			{
				buyingIcon.sprite = carBuyingIcon_video;
				coinsObject.SetActive(false);
				adsObject.SetActive(true);
				gemsObject.SetActive(false);
			}

			carImage.sprite = carUnlockedIcon;
			
			//buyButtonText.text = "Buy";
			if(DataManager.playerData.money >= carData.carPrice)
			{
				
				buyButtonImage.sprite = buyButton_On;
				buyButton.interactable = true;
			}
			else
			{
				buyButtonImage.sprite = buyButton_Off;
				buyButton.interactable = false;
			}
			lockedObject.SetActive(false);
		}
		carPriceText.text = ResourcePanel_C.IntParseToString(carData.carPrice);
	}

	/// <summary>
	///  Add this Method to Button Click and SlotManager Creating Random Car buying button.
	/// </summary>
	public void UpgradeCar()
	{
		if (!SlotManager.instance.isSlotEmpty) // Incase there is no Slots just dont do remaining Operations. // Do the SlotAnimations incase if the Slot is Full.
			return;

		base.UpgradeItem(carData, carBuyingType);
		if (!purchased)
			return;

		// Incase of Purchase is Done Add the Logic Here.
		Upgraded(carData);
	}
	#endregion


	#region Private Methods
	
	/// <summary>
	///  Incase if the car is Locked Set the Properties.
	/// </summary>
	private void UpdateUIOnCarLocked()
	{
		lockedObject.SetActive(true);
		coinsObject.SetActive(false);
		adsObject.SetActive(false);
		gemsObject.SetActive(false);
		carImage.sprite = carlockedIcon;
		buyButtonImage.sprite = buyButton_Off;
		buyButtonText.text = "Locked";
		buyButton.interactable = false;
	}
	#endregion
	
}
