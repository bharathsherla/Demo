using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
	#region Varibles
	public Slot[] slots;

	public Dictionary<int, Slot> slotDictionary = new Dictionary<int, Slot>();
	public ItemInfo currentItem;
	public GameDatabase itemsDatabase;
	#endregion


	#region Public Methods
	#endregion

	#region Private Methods
	private void Start()
	{
		for(int i = 0; i < slots.Length;i++)
		{
			slots[i].slotId = i;
			slotDictionary.Add(i, slots[i]);
		}
	}

	private void Update()
	{
		//if(Input.GetMouseButtonDown(0))
		{
			SendRayCast();
		}
	}

	private void SendRayCast()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.WorldToScreenPoint(Input.mousePosition), Vector2.zero);
		if(hit.collider != null)
		{
			Debug.Log("SlotManager - hit object is " + hit.collider.gameObject.name);
		}
	}
	#endregion
}
