using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;

// This Class will Handle to Show the Player Experiance Level Based on the Data.
public class XpLevelPopup_C : MonoBehaviour
{
	#region Variables
	[SerializeField] private Text xpLevelCount;
	[SerializeField] private Text coinsRewardText;
	[SerializeField] private Text gemsRewardText;
	#endregion

	#region Public Methods


	/// <summary>
	///  Once Player Experiance Level has Increased show the Xp Level Popup.
	/// </summary>
	/// <param name="xpLevel"></param>
	public void ShowXpLevelPopup(int xpLevel)
	{
		xpLevelCount.text = (xpLevel+1).ToString();
		GetComponent<UIView>().Show();
	}

	/// <summary>
	///  Attach it to Close Button in XpLevel Popup.
	/// </summary>
	public void CloseXpLevelPopup()
	{
		GetComponent<UIView>().Hide();
	}
	#endregion

	#region Private Methods
	#endregion
}
