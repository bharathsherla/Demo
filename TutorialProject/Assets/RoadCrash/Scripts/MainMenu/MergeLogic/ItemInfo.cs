using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

	#region variables
	public int itemId;
	public int slotid;
	public Image itemImage;
	#endregion

	#region Public Methods
	public void Init(int id, int slotId, Sprite itemSprite)
	{
		this.itemId = id;
		this.slotid = slotId;
		this.itemImage.sprite = itemSprite;
	}
	#endregion

	#region Private Methods
	#endregion
}
