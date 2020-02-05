using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;


/// <summary>
///  After Confirming the Game name change the Name to That.
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	#region Variables
	public CarPanel_C carPanel_C;
	public ResourcePanel_C resourcePanel_C;
	public XpLevelPopup_C xpLevelPopup;

	public UIView testSettingsView;
	[SerializeField]private GameObject[] carModels;
	[SerializeField] private Slider speedSlider;
	[SerializeField] private Slider AccelarationSlider;
	#endregion

	#region Public Methods
	public void Initialize()
	{
		carPanel_C.Init();
		XpManager.Initialize();
		resourcePanel_C.Init();
		UpdateCar(DataManager.playerData.playerUnlockedCarIndex);
	}

	/// <summary>
	///  Based on the PlayerSelected Car Index show the car Model.
	/// </summary>
	public void UpdateCar(int playerCarIndex)
	{
		for(int i = 0; i < carModels.Length;i++)
		{
			carModels[i].SetActive(false);
		}
		carModels[playerCarIndex].SetActive(true);
		speedSlider.value = CarData.GetCarSpeed();
		AccelarationSlider.value = CarData.GetCarAccelaration();
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

	private void OnEnable()
	{
		XpManager.OnXpCountUpdated += OnXpValueUpdated;
	}

	private void OnDisable()
	{
		XpManager.OnXpCountUpdated -= OnXpValueUpdated;
	}

	private void OnXpValueUpdated(int xpLevel, long xpCount, long remainingXpCount, bool hasIncreasedPlayerXpLevel)
	{
		if(hasIncreasedPlayerXpLevel)
		{
			Debug.Log("GameManager, Player Level increased");
			xpLevelPopup.ShowXpLevelPopup(xpLevel);
		}
	}

	#endregion
}
