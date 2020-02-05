using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;

public class CarUnlockedPopup_C : MonoBehaviour
{
	#region Variables
	[SerializeField]private Image carImage;
	[SerializeField]private Slider accelarationSlider;
	[SerializeField]private Slider speedSlider;
	#endregion

	#region Public Methods

	/// <summary>
	///  Attach it to Close Button
	/// </summary>
	public void HideCarUnlockedPopup()
	{
		GameManager.instance.UpdateCar(DataManager.playerData.playerUnlockedCarIndex);
		GetComponent<UIView>().Hide();
	}

	/// <summary>
	/// If the User Unlocks New Car Call this Method to Open popup to show Which Car User is Unlocked.
	/// </summary>
	public void ShowCarUnlockedPopup()
	{
		carImage.sprite = SlotManager.instance.itemsDatabase.items[DataManager.playerData.playerUnlockedCarIndex];
		// Change Accelaration Slider Value - 
		// Change Speed Slider Value
		GetComponent<UIView>().Show();
	}
	#endregion

	#region Private Methods
	#endregion
}
