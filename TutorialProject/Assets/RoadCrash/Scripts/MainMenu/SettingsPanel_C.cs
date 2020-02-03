using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;

public class SettingsPanel_C : MonoBehaviour
{
	#region Variables
	[SerializeField] private Image buttonImage;
	[SerializeField] private Sprite soundOn;
	[SerializeField] private Sprite soundOff;

	[SerializeField] private Text soundText;
	[SerializeField] private Text vibrationText;
	#endregion

	#region Public Methods
	/// <summary>
	///  Add this to Close Button.
	/// </summary>
	public void HideSettingsPanel()
	{
		this.GetComponent<UIView>().Hide();
	}

	/// <summary>
	///  Attach it to Sound Button.
	/// </summary>
	public void OnSoundButtonClicked()
	{
		if(DataManager.gameSettings.isSoundOn)
		{
			// Off the SOund 
			AudioListener.volume = 0;
			buttonImage.sprite = soundOff;
			//soundText.text = "Sound OFF";
			DataManager.gameSettings.isSoundOn = false;
			DataManager.SaveData();
		}
		else if(!DataManager.gameSettings.isSoundOn)
		{
			// on Sound 
			AudioListener.volume = 1;
			buttonImage.sprite = soundOn;
			//soundText.text = "Sound ON";
			DataManager.gameSettings.isSoundOn = true;
			DataManager.SaveData();
		}
	}

	/// <summary>
	///  Attach the Method to Vibration Button
	/// </summary>
	public void OnVibrateButtonClicked()
	{
		if(DataManager.gameSettings.isVibrationOn)
		{
			// Off the Vibration 
			//vibrationText.text = "Vibrate OFF";
			DataManager.gameSettings.isVibrationOn = false;
			DataManager.SaveData();
		}
		else if(!DataManager.gameSettings.isVibrationOn)
		{
			// On Vibration
			//vibrationText.text = "Vibrate ON";
			DataManager.gameSettings.isVibrationOn = true;
			DataManager.SaveData();
		}
	}
	#endregion


	#region Private Methods
	#endregion
}
