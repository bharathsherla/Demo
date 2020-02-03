using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{

	public static SlotManager instance;
	#region Varibles
	public Slot[] slots;

	public Dictionary<int, Slot> slotDictionary = new Dictionary<int, Slot>();
	public ItemInfo currentItem;
	public GameDatabase itemsDatabase;
	public GameObject garageIsFullObject; // When the Garage is Full Show and Deactivate the Object.
	private WaitForSeconds delayToDeactivateGarageFullObject = new WaitForSeconds(0.2f);

	// Item Parent
	public Transform itemParent; // On Dragging of an item set the parent as this.
	public GameObject item;
	public bool isSlotEmpty => GetEmptySlotIndex() == -1 ? false : true;


	[Header("Car Item References")]
	[SerializeField]private Image carImage;
	[SerializeField]private Text carPrice;

	[Header("Car Merge Animation")]
	public CarMergeAnimPanel carMergePanel;

	private int randomCarNumber;
	[SerializeField]private Button carGenerateButton;
	[SerializeField] private CarUnlockedPopup_C carUnlockedPopup;
	#endregion


	#region Public Methods

	// Button click Events
	public void GenerateItem()
	{
		//if(GetEmptySlotIndex() != -1 && DataManager.playerData.money >= GameManager.instance.carPanel_C.cars[randomCarNumber].carData.carPrice)
		//{
		//	DataManager.playerData.money -= GameManager.instance.carPanel_C.cars[randomCarNumber].carData.carPrice;
		//	DataManager.SaveData();

		//	GenerateItemInSlot();
		//}else
		//{
		//	StartCoroutine(ShowGarageFullObject());
		//	Debug.Log("SlotManager - All Slots are Filled up");
		//}
		GameManager.instance.carPanel_C.BuyCar(randomCarNumber);
		CreateRandomItem();
	}

	/// <summary>
	///  On Success full purchase of Item from SlotManager - Generate item in Slot and Create another Random Item Car.
	/// </summary>
	public void GenerateItemInSlot()
	{
		
			var slot = slots[GetEmptySlotIndex()];
			var item = Instantiate(this.item, slot.gameObject.transform) as GameObject;
			item.GetComponent<Item>().Init(randomCarNumber, slot, itemsDatabase.items[randomCarNumber], slot.gameObject.transform); // As per Testing we are using 0 index item.
			CreateRandomItem();
	}

	/// <summary>
	///  Incase if the User want to Buy from Car Panel the Use this Method.
	/// </summary>
	/// <param name="index"></param>
	public void GenerateItemInSlot(int index)
	{
		if (GetEmptySlotIndex() != -1)
		{
			var slot = slots[GetEmptySlotIndex()];
			var item = Instantiate(this.item, slot.gameObject.transform) as GameObject;
			item.GetComponent<Item>().Init(index, slot, itemsDatabase.items[index], slot.gameObject.transform); // As per Testing we are using 0 index item.
			//CreateRandomItem();
			//CreateItem(index);
		}
		else
		{
			StartCoroutine(ShowGarageFullObject());
			Debug.Log("SlotManager - All Slots are Filled up");
		}
	}

	public void IncreasePlayerUnlockedCar()
	{
		Debug.Log("SlotManager - IncreasePlayerUnlockedCar -> Player Unlocked Car Index - " + DataManager.playerData.playerUnlockedCarIndex);
		DataManager.playerData.playerUnlockedCarIndex += 1;
		DataManager.upgradableItems.carItems[DataManager.playerData.playerUnlockedCarIndex].isUnlocked = true;
		DataManager.SaveData();
		carUnlockedPopup.ShowCarUnlockedPopup();
		Debug.Log("SlotManager - IncreasePlayerUnlockedCar -> Player Unlocked Car Index After - " + DataManager.playerData.playerUnlockedCarIndex);
		// Open Popup to Show Player Car Unlocked.
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
			Destroy(this.gameObject);
		}
	}
	private void Start()
	{
		for(int i = 0; i < slots.Length;i++)
		{
			slots[i].slotId = i;
			slotDictionary.Add(i, slots[i]);
		}

		if(DataManager.playerData.slotData.Count > 0)
		{
			for(int i = 0; i < slots.Length;i++)
			{
				slots[i].slotData.slotItemId = DataManager.playerData.slotData[i].slotItemId;
				if (slots[i].slotData.slotItemId != -1)
					GenerateItemInSlot(slots[i], slots[i].slotData.slotItemId);
			}
		}else
		{
			for (int i = 0; i < slots.Length; i++)
			{
				DataManager.playerData.slotData.Add(slots[i].slotData);
			}
			DataManager.SaveData();
		}
		CreateRandomItem();
	}
	
	private IEnumerator ShowGarageFullObject()
	{
		garageIsFullObject.SetActive(true);
		yield return delayToDeactivateGarageFullObject;
		garageIsFullObject.SetActive(false);
	}

	private int GetEmptySlotIndex()
	{
		for(int i = 0; i < slots.Length;i++)
		{
			if (slots[i].currentSlotState == Slot.SlotStates.EMPTY)
				return i;
		}
		return -1;
	}
	private Slot GetEmptySlot()
	{
		for(int i = 0; i < slots.Length;i++)
		{
			if (slots[i].currentSlotState == Slot.SlotStates.EMPTY)
				return slots[i];
		}
		return null;
	}

	private void CreateRandomItem()
	{
		//randomCarNumber = UnityEngine.Random.Range(0, DataManager.playerData.playerUnlockedCarIndex);
		randomCarNumber = UnityEngine.Random.Range(0, GetHighestRandomNumber());
		carImage.sprite = itemsDatabase.items[randomCarNumber];
		//carPrice.text = ResourcePanel_C.IntParseToString(GameManager.instance.carPanel_C.cars[randomCarNumber].carData.carPrice);
		if(DataManager.upgradableItems.carItems.Count > 0)
			carPrice.text = ResourcePanel_C.IntParseToString(DataManager.upgradableItems.carItems[randomCarNumber].carPrice);
		// Incase if the User Dont Have enough money Show Black car Image or else show the Normal Image. 
		//@- Bharath implement it.
		if (GameManager.instance.carPanel_C.cars[randomCarNumber].carData.carPrice <= DataManager.playerData.money)
			carGenerateButton.interactable = true;
		else
			carGenerateButton.interactable = false;
	}


	private void CreateItem(int index)
	{
		carImage.sprite = itemsDatabase.items[index];
		carPrice.text = ResourcePanel_C.IntParseToString(GameManager.instance.carPanel_C.cars[index].carData.carPrice);

		if (GameManager.instance.carPanel_C.cars[index].carData.carPrice <= DataManager.playerData.money)
			carGenerateButton.interactable = true;
		else
			carGenerateButton.interactable = false;
	}

	private int GetHighestRandomNumber()
	{
		int maxIndex = 0;
		for(int i = 0; i <= DataManager.playerData.playerUnlockedCarIndex;i++)
		{
			if (GameManager.instance.carPanel_C.cars[i].carBuyingType == CarBuyingType.COINS && GameManager.instance.carPanel_C.cars[i].carData.isUnlocked)
				maxIndex = i;
			//if(DataManager.upgradableItems != null && DataManager.upgradableItems.carItems[i].isUnlocked && GameManager.instance.carPanel_C.cars[i].carBuyingType == CarBuyingType.COINS)
			//{
			//	maxIndex = i;
			//}
		}
		return maxIndex;
	}


	// Oncase if there is any Item in slot then closes the app the save the data and whenever User Open the App show the previous Data.
	private void GenerateItemInSlot(Slot slot, int itemIndex)
	{
		var item = Instantiate(this.item, slot.gameObject.transform) as GameObject;
		item.GetComponent<Item>().Init(itemIndex, slot, itemsDatabase.items[itemIndex], slot.gameObject.transform);
	}
	#endregion
}
