using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;


/// <summary>
///  After Confirming the Game name change the Name to That.
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	#region Variables
	public CarPanel_C carPanel_C;
	public ResourcePanel_C resourcePanel_C;

	public UIView testSettingsView;
	#endregion

	#region Public Methods
	public void Initialize()
	{
		carPanel_C.Init();
		XpManager.Initialize();
		resourcePanel_C.Init();
	}
	#endregion

	#region Private Methods
	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			DestroyImmediate(this.gameObject);
		}
		//AddCoins();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			// Open Settings panel to Test.
			DataManager.playerData.playerUnlockedCarIndex += 1;
			DataManager.SaveData();
		}else if(Input.GetKeyDown(KeyCode.O))
		{
			testSettingsView.Hide();
		}
	}

	public void AddCoins()
	{
		DataManager.playerData.money += 100000;
		DataManager.SaveData();

		ResourcePanel_C.UpdatePlayerResources();
	}

	private void Start()
	{
		Initialize();
	}
	#endregion
}
