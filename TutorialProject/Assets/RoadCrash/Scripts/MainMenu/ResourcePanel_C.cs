using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
///  This Class Hold the All the User Holding Game Cash Data like - cash , coins , Gems etc.. whenever user get some money Update the Resource Panel UI.
/// </summary>
public class ResourcePanel_C : MonoBehaviour
{
	private static ResourcePanel_C instance;
	public enum ResourceType
	{
		COINS,
		CASH,
	}

	#region Variables
	[Header(" Player Cash Text References")]
	[SerializeField] private Text playerLevel;
	[SerializeField] private Text playerCoins;
	[SerializeField] private Text playerGems;
	[SerializeField] private Slider playerXpSlider;
	#endregion


	#region Public Methods
	/// <summary>
	///  Whenever User Purchases something or getting Ingame Cash From any resource Call this method 
	///  to Update the Player Cash In resource Panel.
	/// </summary>
	public static void UpdatePlayerResources()
	{
		instance.playerCoins.text = IntParseToString(DataManager.playerData.money);
		instance.playerGems.text = IntParseToString(DataManager.playerData.gems);
	}

	public static void AddCoins(long coins)
	{
		DataManager.playerData.money += coins;
		DataManager.SaveData();
		UpdatePlayerResources();
	}

	public static void AddGems(long Gems)
	{
		DataManager.playerData.gems += Gems;
		DataManager.SaveData();
		UpdatePlayerResources();
	}

	public void Init()
	{
		UpdatePlayerResources();
		instance.playerLevel.text = (DataManager.playerData.playerLevel+1).ToString();
		instance.playerXpSlider.value = XpManager.GetPlayerXpRatio();
	}
	#endregion


	#region Private Methods


	/// <summary>
	///  Convert Long to String make it getting Values based on our Requirements.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string IntParseToString(long value)
	{
		string result = value.ToString();

		if (value >= 1000)
		{
			result = Mathf.Floor(((float)value / 100)) / 10 + "k";
		}

		if (value >= 1000000)
		{
			result = Mathf.Floor(((float)value / 10000)) / 100 + "M";
		}

		if (value >= 1000000000)
		{
			result = Mathf.Floor(((float)value / 10000000)) / 100 + "B";
		}

		if (value >= 1000000000000)
		{
			result = Mathf.Floor(((float)value / 1000000000)) / 1000 + "Q";
		}

		return result;
	}

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
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
		playerLevel.text = xpLevel.ToString();
		playerXpSlider.value = ((float)xpCount / (float)remainingXpCount);
	}
	#endregion

}
