using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;

public class StorePanel_C : MonoBehaviour
{
	#region Variables
	#endregion

	#region Public Methods

	/// <summary>
	///  Attach to Close Button in Store Panel.
	/// </summary>
	public void HideStorePanel()
	{
		this.GetComponent<UIView>().Hide();
	}
	#endregion

	#region Private Methods
	#endregion
}
