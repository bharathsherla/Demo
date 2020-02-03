using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region Variables

	public int id;
	public Slot parentSlot;
	public Image itemImage;
	public Transform parentObject; // Incase if it dragging set the parent to ItemParent or OnDrop of the object set the parent to slot.

	private Vector3 startPosition;
	#endregion

	#region Public Methods

	public void Init(int id, Slot parentSlot, Sprite itemImage, Transform itemParent)
	{
		this.id = id;
		this.parentSlot = parentSlot;
		this.itemImage.sprite = itemImage;
		this.parentObject = itemParent;
		parentSlot.SetItem(this);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		startPosition = transform.position;
		transform.position = eventData.position;
		parentObject = SlotManager.instance.itemParent;
		transform.SetParent(parentObject);
		parentSlot.item = null;
		if (parentSlot != null)
			parentSlot.ChangeSlotState(Slot.SlotStates.EMPTY);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		transform.localScale = Vector3.one * 1.2f;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (parentSlot != null)
		{
			parentObject = parentSlot.transform;
			parentSlot.ChangeSlotState(Slot.SlotStates.FULL);
			parentSlot.item = this;
		}
		transform.SetParent(parentObject);
		transform.localPosition = Vector3.zero;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		transform.localScale = Vector3.one;
	}

	#endregion


	#region Private Methods
	#endregion
}
