using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This class need to Attach to the CarAnimOnMerge GameObject 
///  once Success full merge has Done play the animation.
/// </summary>
public class CarMergeAnimPanel : MonoBehaviour
{

	#region Variables
	[SerializeField] private Image car1Image;
	[SerializeField] private Image car2Image;

	private float delayToDeactivateObject = 0.5f;
	#endregion


	#region Public Methods
	public void PlayCarMergeAnimation(Transform parentSlotPosition,int car1Id, int car2Id)
	{
		car1Image.sprite = SlotManager.instance.itemsDatabase.items[car1Id];
		car2Image.sprite = SlotManager.instance.itemsDatabase.items[car2Id];
		transform.SetParent(SlotManager.instance.itemParent.transform);
		transform.position = parentSlotPosition.position;
		gameObject.SetActive(true);
		DataManager.playerData.spinWheelCash += (car1Id * 200);
		DataManager.SaveData();
		Invoke(nameof(DeactivateOnAnimComplete), delayToDeactivateObject);
	}
	#endregion

	#region Private Methods
	private void DeactivateOnAnimComplete()
	{
		this.gameObject.SetActive(false);
	}
	#endregion
}
