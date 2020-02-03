using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	#region Variables
	private static DataSaver dataSaver;
	public static PlayerData playerData;
	public static GameSettings gameSettings;

	public static UpgradableItems upgradableItems; // Use this incase of CarPanel Only for Getting Car Data.

	#endregion

	#region Public Methods
	public static void LoadData()
	{
		object[] obj = dataSaver.LoadData();
		
		if(obj != null)
		{
			playerData = obj[0] as PlayerData;
			upgradableItems = obj[1] as UpgradableItems;
			gameSettings = obj[2] as GameSettings;
		}
	}


	public static void SaveData()
	{
		object[] obj = new object[3];

		obj[0] = playerData;
		obj[1] = upgradableItems;
		obj[2] = gameSettings;

		dataSaver.SaveData(obj);
	}
	#endregion

	#region Private Methods

	private void Awake()
	{
		Initialize();
	}
	private  void Initialize()
	{
		playerData = new PlayerData();
		upgradableItems = new UpgradableItems();
		gameSettings = new GameSettings();

		dataSaver = GetComponent<DataSaver>();
		LoadData();
	}

	#endregion
}
