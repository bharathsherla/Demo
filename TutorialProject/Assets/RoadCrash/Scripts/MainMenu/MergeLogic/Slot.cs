using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, ICashGenerator
{
	public enum SlotStates
	{
		FULL,
		EMPTY,
	}
	public enum SlotType
	{
		MERGE,
		DELETE,
	}
	#region Variables
	public int slotId;
	public SlotStates currentSlotState = SlotStates.EMPTY;
	public SlotType currentSlotType;
	public Item item;
	public Text slotCashText;

	private float maxTimeToGiveCash = 1.5f;
	private bool canGenerateCash = false;
	private float timer = 0f;

	public SlotData slotData;
	#endregion


	#region Public Methods


	public void OnDrop(PointerEventData eventData)
	{
		AssignItemToSlot(eventData);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log("OnPointerExit");
	}

	public void SetItem(Item item)
	{
		this.item = item;
		ChangeSlotState(SlotStates.FULL);
	}

	private void AssignItemToSlot(PointerEventData eventData)
	{
		Debug.Log("Slot - Dragged Item " + eventData.pointerDrag.name + " Name - " + gameObject.name);
	    var	item = eventData.pointerDrag.GetComponent<Item>();

		if(currentSlotType == SlotType.MERGE)
		{
			if (currentSlotState == SlotStates.EMPTY)
			{
				// Add item to this slot.
				item.parentObject = this.transform;
				item.parentSlot = this;
				this.item = item;
				ChangeSlotState(SlotStates.FULL);
			}
			else if (currentSlotState == SlotStates.FULL)
			{
				// Slot is Full 
				if (this.item.id == item.id)
				{
					// Merge Items.
					Debug.Log("Slot -  Merge Two Items");
					SlotManager.instance.carMergePanel.PlayCarMergeAnimation(this.transform, this.item.id, item.id);
					this.item.gameObject.SetActive(false);
					Invoke(nameof(DelayTogenerateItemInSlot), 0.5f);
					Destroy(item.gameObject);
				}
				else
				{
					// Swap the Items
					Debug.Log("Slot - Swap Two Items");
					//var dummyItem = item;
					int dummyItemId = item.id;
					Slot dummySlot = item.parentSlot;
					Transform dummyParent = item.parentSlot.transform;//item.parentObject;
					item.Init(this.item.id, item.parentSlot, SlotManager.instance.itemsDatabase.items[this.item.id], item.parentObject);
					this.item.Init(dummyItemId, this, SlotManager.instance.itemsDatabase.items[dummyItemId], this.gameObject.transform);
				}
			}
		}else if(currentSlotType == SlotType.DELETE)
		{
			Debug.Log("Slot - Item Deleted");
			Destroy(item.gameObject);
		}
		
		
	}

	public void ChangeSlotState(SlotStates currentState)
	{
		currentSlotState = currentState;

		if (currentState == SlotStates.FULL)
		{
			canGenerateCash = true;
			if(item != null)
				DataManager.playerData.slotData[slotId].slotItemId = item.id;

		}
		else
		{
			canGenerateCash = false;
			DataManager.playerData.slotData[slotId].slotItemId = -1; // -1 means there is not Item in the Slot.
			timer = 0f;
		}
			
	}

	/// <summary>
	/// Incase of Item merge Give Some time dely to Show because Carmerge Animation will happen here.
	/// </summary>
	private void DelayTogenerateItemInSlot()
	{
		if (item.id == DataManager.playerData.playerUnlockedCarIndex)
			SlotManager.instance.IncreasePlayerUnlockedCar();
		XpManager.UpdarePlayerXp((this.item.id + 1) * 10);
		this.item.Init(this.item.id + 1, this, SlotManager.instance.itemsDatabase.items[this.item.id + 1], this.gameObject.transform);
		this.item.gameObject.SetActive(true);

	}

	public void AddCash()
	{
		if(currentSlotState == SlotStates.FULL)
		{
			DataManager.playerData.money += SlotManager.instance.itemsDatabase.carCashValues[item.id];
			PlayCashEarnedTextAnimation();
			DataManager.SaveData();
			ResourcePanel_C.UpdatePlayerResources();
			//Debug.Log("Slot - Money Added to Player Resource - " +  SlotManager.instance.itemsDatabase.carCashValues[item.id]);
			// Showing Cash Text.
		}
	}

	private void PlayCashEarnedTextAnimation()
	{
		slotCashText.text = "+" + ResourcePanel_C.IntParseToString(SlotManager.instance.itemsDatabase.carCashValues[item.id]);
		slotCashText.transform.SetAsLastSibling();
		slotCashText.gameObject.SetActive(true);
		slotCashText.transform.DOLocalMove(new Vector3(0f, 130f, 0f), 1f).SetEase(Ease.Linear).OnComplete(OnAnimCompleted); // OutQuart
	}
	private void OnAnimCompleted()
	{
		slotCashText.gameObject.SetActive(false);
		slotCashText.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	private void Update()
	{
		if(canGenerateCash)
		{
			timer += Time.deltaTime;
			if(timer >= maxTimeToGiveCash)
			{
				timer = 0f;
				AddCash();
			}
		}
	}

	#endregion
}
