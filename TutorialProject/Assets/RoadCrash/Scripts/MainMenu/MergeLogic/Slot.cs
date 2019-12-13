using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	public enum SlotStates
	{
		FULL,
		EMPTY,
	}
	#region Variables
	public int slotId;
	public SlotStates currentSlotState = SlotStates.EMPTY;
	public Item item;
	#endregion


	#region Public Methods
	#endregion
}
