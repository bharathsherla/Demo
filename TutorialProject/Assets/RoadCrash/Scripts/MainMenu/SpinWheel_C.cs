using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using System;
using UnityEngine.UI;

public class SpinWheel_C : MonoBehaviour
{
	public enum SPINWHEEL_REWARDS
	{
		REWARD_1,
		REWARD_2,
		REWARD_3,
		REWARD_4,
		REWARD_5,
		REWARD_6,
	}
	public enum PaidSpinType
	{
		VIDEOS_ADS,
		GEMS,
	}
	#region Variables
	public List<int> prize;
	public GameObject spinWheelObject;
	[SerializeField]private Text reward1Text;
	[SerializeField]private Text reward2Text;
	[SerializeField]private Text reward3Text;

	private bool isStarted;                    // Flag that the wheel is spinning
	private float[] sectorsAngles;             // All sectors angles
	private float finalAngle;                  // The final angle is needed to calculate the reward
	private float startAngle = 0;              // The first time start angle equals 0 but the next time it equals the last final angle
	private float currentLerpRotationTime;     // Needed for spinning animation
	private int turnCost = 5;             // How much coins user waste when turn when wheel
	private int currentCoinsAmount = 0;


	// Here you can set time between two free turns
	private int _timerMaxHours = 24;
	private int _timerMaxMinutes = 0;
	private int _timerMaxSeconds = 0;

	// Remaining time to next free turn
	private int _timerRemainingHours = 0;
	private int _timerRemainingMinutes = 0;
	private int _timerRemainingSeconds = 0;

	private DateTime nextFreeTurnTime;

	// Set TRUE if you want to let players to turn the wheel for coins while free turn is not available yet
	private bool _isPaidTurnEnabled = true;

	// Set TRUE if you want to let players to turn the wheel for FREE from time to time
	private bool _isFreeTurnEnabled = true;

	// Flag that player can turn the wheel for free right now
	private bool _isFreeTurnAvailable = false;


	// Key name for storing in PlayerPrefs
	private const string LAST_FREE_TURN_TIME_NAME = "LastFreeTurnTimeTicks";



	[Header("Component References")]
	[SerializeField]private GameObject freeButton; // Which will Activate every 24 hours
	[SerializeField]private GameObject videoAdsButton;
	[SerializeField]private GameObject gemsButton;
	#endregion

	#region Public Methods
	/// <summary>
	///  Attach it to Close Button in Spin Wheel Panel.
	/// </summary>
	public void HideSpinWheelPanel()
	{
		this.GetComponent<UIView>().Hide();
	}

	public void TurnSpinWheel()
	{
		if (_isFreeTurnAvailable)
		{
			TurnWheelForFree();
		}
		else
		{
			// If we have enabled paid turns
			if (_isPaidTurnEnabled)
			{
				// If player have enough Gems
				
			}
		}
	}
	public void OnClickTurnSpinWheelButton(int index)
	{
		switch(index)
		{
			case 0: // Video Ads
				TurnWheelForVideoAds();
				break; 
			case 1: // Gems
				TurnWheelForGems();
				break;
		}
	}
	#endregion

	#region Private Methods
	private void TurnWheelForFree()
	{
		TurnWheel(true);
	}

	private void TurnWheelForGems()
	{
		if (DataManager.playerData.gems >= turnCost)
		{
			TurnWheel(false);
		}
		else
		{
			// Show popup that there is no Gems.
		}
	}

	private void TurnWheel(bool isFree)
	{
		currentLerpRotationTime = 0f;

		// Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
		//_sectorsAngles = new float[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };
		sectorsAngles = new float[] { 60, 120, 180, 240, 300, 360 };

		int fullTurnovers = 5;

		// Choose random final sector
		float randomFinalAngle = sectorsAngles[UnityEngine.Random.Range(0, sectorsAngles.Length)];

		// Set up how many turnovers our wheel should make before stop
		finalAngle = -(fullTurnovers * 360 + randomFinalAngle);

		// Stop the wheel
		isStarted = true;

		

		// Decrease money for the turn if it is not free turn
		if (!isFree)
		{
			DataManager.playerData.gems -= turnCost;
			DataManager.SaveData();
			ResourcePanel_C.UpdatePlayerResources();
		}
		else
		{
			// At this step you can save current time value to your server database as last used free turn
			// We can't save long int to PlayerPrefs so store this value as string and convert to long later
			PlayerPrefs.SetString(LAST_FREE_TURN_TIME_NAME, DateTime.Now.Ticks.ToString());

			// Restart timer to next free turn
			SetNextFreeTime();
		}
	}
	private void TurnWheelForVideoAds()
	{

	}

	private void SetNextFreeTime()
	{
		// Reset the remaining time values
		_timerRemainingHours = _timerMaxHours;
		_timerRemainingMinutes = _timerMaxMinutes;
		_timerRemainingSeconds = _timerMaxSeconds;

		// Get last free turn time value from storage
		// We can't save long int to PlayerPrefs so store this value as string and convert to long
		nextFreeTurnTime = new DateTime(Convert.ToInt64(PlayerPrefs.GetString(LAST_FREE_TURN_TIME_NAME, DateTime.Now.Ticks.ToString())))
								.AddHours(_timerMaxHours)
								.AddMinutes(_timerMaxMinutes)
								.AddSeconds(_timerMaxSeconds);

		_isFreeTurnAvailable = false;
		DisplayUI(_isFreeTurnAvailable);
	}

	private void Update()
	{
		// Show timer only if we enable free turns
		if (_isFreeTurnEnabled)
			UpdateFreeTurnTimer();

		if (!isStarted)
			return;

		// Animation time
		float maxLerpRotationTime = 4f;

		// increment animation timer once per frame
		currentLerpRotationTime += Time.deltaTime;

		// If the end of animation
		if (currentLerpRotationTime > maxLerpRotationTime || spinWheelObject.transform.eulerAngles.z == finalAngle)
		{
			currentLerpRotationTime = maxLerpRotationTime;
			isStarted = false;
			startAngle = finalAngle % 360;
			GiveAwardByAngle();
			//StartCoroutine(HideCoinsDelta());
		}
		else
		{
			// Calculate current position using linear interpolation
			float t = currentLerpRotationTime / maxLerpRotationTime;

			// This formula allows to speed up at start and speed down at the end of rotation.
			// Try to change this values to customize the speed
			t = t * t * t * (t * (6f * t - 15f) + 10f);

			float angle = Mathf.Lerp(startAngle, finalAngle, t);
			spinWheelObject.transform.eulerAngles = new Vector3(0, 0, angle);
		}
	}
	// Change remaining time to next free turn every 1 second
	private void UpdateFreeTurnTimer()
	{
		// Don't count the time if we have free turn already
		if (_isFreeTurnAvailable)
			return;

		// Update the remaining time values
		_timerRemainingHours = (int)(nextFreeTurnTime - DateTime.Now).Hours;
		_timerRemainingMinutes = (int)(nextFreeTurnTime - DateTime.Now).Minutes;
		_timerRemainingSeconds = (int)(nextFreeTurnTime - DateTime.Now).Seconds;

		// If the timer has ended
		if (_timerRemainingHours <= 0 && _timerRemainingMinutes <= 0 && _timerRemainingSeconds <= 0)
		{
			//NextFreeTurnTimerText.text = "  Ready!";
			// Now we have a free turn
			_isFreeTurnAvailable = true;
			DisplayUI(_isFreeTurnAvailable);
		}
		else
		{
			// Show the remaining time
			//NextFreeTurnTimerText.text = "  " + String.Format("{0:00}:{1:00}:{2:00}", _timerRemainingHours, _timerRemainingMinutes, _timerRemainingSeconds);
			// We don't have a free turn yet
			_isFreeTurnAvailable = false;
		}
	}
	private void GiveAwardByAngle()
	{
		switch((int)startAngle)
		{
			case 0:
				RewardUser(SPINWHEEL_REWARDS.REWARD_1);
				break;
			case -60:
				RewardUser(SPINWHEEL_REWARDS.REWARD_2);
				break;
			case -120:
				RewardUser(SPINWHEEL_REWARDS.REWARD_3);
				break;
			case -180:
				RewardUser(SPINWHEEL_REWARDS.REWARD_4);
				break;
			case -240:
				RewardUser(SPINWHEEL_REWARDS.REWARD_5);
				break;
			case -300:
				RewardUser(SPINWHEEL_REWARDS.REWARD_6);
				break;
		}
	}

	/// <summary>
	///  Reward_1 -> Coins 
	///  Reward_2 -> Cars 
	///  Reward_3 -> Coins 
	///  Reward_4 -> Gems - 20 
	///  Reward_5 -> Coins 
	///  Reward_6 -> Coins - 80
	/// </summary>
	/// <param name="rewardType"></param>
	private void RewardUser(SPINWHEEL_REWARDS rewardType)
	{
		switch(rewardType)
		{
			case SPINWHEEL_REWARDS.REWARD_1:
				Debug.Log("SpinWheel_C, Rewarding User - Reward1 20 Gems");
				ResourcePanel_C.AddGems(20);
				break;
			case SPINWHEEL_REWARDS.REWARD_2:
				ResourcePanel_C.AddCoins(DataManager.playerData.spinWheelCash);
				Debug.Log("SpinWheel_C, Rewarding User - Reward2 coins Pack 1");
				break;
			case SPINWHEEL_REWARDS.REWARD_3:
				Debug.Log("SpinWheel_C, Rewarding User - Reward3 cars");
				break;
			case SPINWHEEL_REWARDS.REWARD_4:
				Debug.Log("SpinWheel_C, Rewarding User - Reward4 coins pack 2");
				ResourcePanel_C.AddCoins(DataManager.playerData.spinWheelCash * 2);
				break;
			case SPINWHEEL_REWARDS.REWARD_5:
				Debug.Log("SpinWheel_C, Rewarding User - Reward5 100 Gems");
				ResourcePanel_C.AddGems(100);
				break;
			case SPINWHEEL_REWARDS.REWARD_6:
				Debug.Log("SpinWheel_C, Rewarding User - Reward6 coins Pack 3");
				ResourcePanel_C.AddCoins(DataManager.playerData.spinWheelCash * 3);
				break;
		}
	}

	private void DisplayUI( bool status)
	{
		freeButton.SetActive(status);
		videoAdsButton.SetActive(!status);
		gemsButton.SetActive(!status);

		reward1Text.text = ResourcePanel_C.IntParseToString(DataManager.playerData.spinWheelCash);
		reward2Text.text = ResourcePanel_C.IntParseToString(DataManager.playerData.spinWheelCash * 2);
		reward3Text.text = ResourcePanel_C.IntParseToString(DataManager.playerData.spinWheelCash * 3);

	}
	
	private void OnEnable()
	{
		if (_isFreeTurnEnabled)
		{
			if (!PlayerPrefs.HasKey(LAST_FREE_TURN_TIME_NAME))
			{
				//print("Very first free spin: " + DateTime.Now.AddDays(-1).Ticks.ToString());

				PlayerPrefs.SetString(LAST_FREE_TURN_TIME_NAME, DateTime.Now.AddDays(-7).Ticks.ToString());
			}

			// Start our timer to next free turn
			SetNextFreeTime();
		}
		else
		{
			//NextTurnTimerWrapper.gameObject.SetActive(false);
		}
		DisplayUI(_isFreeTurnAvailable);
	}

	#endregion
}
