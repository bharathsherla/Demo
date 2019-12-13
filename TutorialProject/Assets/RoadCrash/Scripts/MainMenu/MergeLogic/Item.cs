using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	#region Variables

	public int id;
	public Slot parentSlot;
	public Image itemImage;

	#endregion

	#region Public Methods

	public void Init(int id, Slot parentSlot, Sprite itemImage)
	{
		this.id = id;
		this.parentSlot = parentSlot;
		this.itemImage.sprite = itemImage;
	}

	#endregion
}
