using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This Class Will Handle All the Cash Generator Logic to the User
///          1) Incase if the Slot is Full Generate Cash Based on the Item Available in that Slot
///          2) Cash Multiplier 
///          3) Watch Video to Double the Coins
///          4) UI Video Ads Button for Reward.
/// </summary>
public class MainMenuCashGenerator : MonoBehaviour
{
	#region Variables
	[SerializeField]private Slot[] slots;

	private float delayToGenerateCash = 2f;
	public int levelNumber;
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void Start()
	{
		//InvokeRepeating(nameof(GenerateCashInSlots), delayToGenerateCash, delayToGenerateCash);
	}

	/// <summary>
	///  Incase if the Slots are Full Generate Cash to the User.
	/// </summary>
	private void GenerateCashInSlots()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			if (slots[i].currentSlotState == Slot.SlotStates.FULL)
				slots[i].AddCash();
		}
	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log(" Progression - " + (levelNumber / 10 + levelNumber % 10) * 100 * Mathf.Pow(10, levelNumber / 10));
		}
	}
	#endregion
}
