using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	public enum CurrentMainMenuPanel
	{
		DASHBOARD,
		SETTINGS_PANEL,
		STORE_PANEL,
		CAR_SELECTION,
		CAR_UPGRADE,
		SPIN_WHEEL_PANEL,
		VIP_PANEL,
	}

	#region Variables
	[SerializeField]private UIView[] mainMenuUIPanles; // No other Components needs to Access it.
	public CurrentMainMenuPanel selectedMainMenuPanel; // Using this so that in future show ads based on User in which panel is.
	[SerializeField]private InputField nameInputField;

	#endregion

	#region Public Methods

	/// <summary>
	/// Index needs to same same Above Enumeration
	///        DASHBOARD - 0
	///        SETTINGS_PANEL - 1 
	///        STORE_PANEL - 2
	///        CAR_SELECTION - 3
	///        CAR_UPGRADE - 4
	///        SPIN_WHEEL_PANEL - 5
	///        VIP_PANEL - 6
	/// </summary>
	/// <param name="index"></param>
	///  This Class will Just hold to Show the UI As All Panel Are Popup type DASHBoard Panel is always there - close popup need to be done From Individual Component.
	public void ShowMainMenuPanels(int index)
	{
		switch(index)
		{
			case 0:
				mainMenuUIPanles[0].Show();
				selectedMainMenuPanel = CurrentMainMenuPanel.DASHBOARD;
				break;
			case 1:
				mainMenuUIPanles[1].Show();
				selectedMainMenuPanel = CurrentMainMenuPanel.SETTINGS_PANEL;
				break;
			case 2:
				mainMenuUIPanles[2].Show();
				selectedMainMenuPanel = CurrentMainMenuPanel.STORE_PANEL;
				break;
			case 3:
				mainMenuUIPanles[3].Show();
				selectedMainMenuPanel = CurrentMainMenuPanel.CAR_SELECTION;
				break;
			case 4:
				mainMenuUIPanles[4].Show();
				selectedMainMenuPanel = CurrentMainMenuPanel.CAR_UPGRADE;
				break;
			case 5:
				mainMenuUIPanles[5].Show();
				selectedMainMenuPanel = CurrentMainMenuPanel.SPIN_WHEEL_PANEL;
				break;
			case 6:
				mainMenuUIPanles[6].Show();
				selectedMainMenuPanel = CurrentMainMenuPanel.VIP_PANEL;
				break;
			case 7:// Load Gameplay Screen
				SceneManager.LoadScene("GamePlay");
				break;
		}
	}

	// Incase if the User Closes Anypopup Set currentPanel Status to DashBoard.
	public void SetCurrentPanelStatus()
	{
		selectedMainMenuPanel = CurrentMainMenuPanel.DASHBOARD;
	}
	#endregion

	#region Private Methods
	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
	}

	#endregion
}
