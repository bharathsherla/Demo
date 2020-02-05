using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : MonoBehaviour
{
	private static Dictionary<int, CarSpecsData> carData = new Dictionary<int, CarSpecsData>()
	{
		{ 0, new CarSpecsData{ carSpeed = 0.2f, carAccelation = 0.1f}},
		{ 1, new CarSpecsData{ carSpeed = 0.22f, carAccelation = 0.12f}},
		{ 2, new CarSpecsData{ carSpeed = 0.24f, carAccelation = 0.14f}},
		{ 3, new CarSpecsData{ carSpeed = 0.26f, carAccelation = 0.16f}},
		{ 4, new CarSpecsData{ carSpeed = 0.28f, carAccelation = 0.18f}},
		{ 5, new CarSpecsData{ carSpeed = 0.3f, carAccelation = 0.22f}},
		{ 6, new CarSpecsData{ carSpeed = 0.32f, carAccelation = 0.24f}},
		{ 7, new CarSpecsData{ carSpeed = 0.34f, carAccelation = 0.26f}},
		{ 8, new CarSpecsData{ carSpeed = 0.36f, carAccelation = 0.28f}},
		{ 9, new CarSpecsData{ carSpeed = 0.38f, carAccelation = 0.3f}},
		{ 10, new CarSpecsData{ carSpeed = 0.4f, carAccelation = 0.32f}},
		{ 11, new CarSpecsData{ carSpeed = 0.42f, carAccelation = 0.34f}},
		{ 12, new CarSpecsData{ carSpeed = 0.44f, carAccelation = 0.36f}},
		{ 13, new CarSpecsData{ carSpeed = 0.46f, carAccelation = 0.38f}},
		{ 14, new CarSpecsData{ carSpeed = 0.48f, carAccelation = 0.4f}},
		{ 15, new CarSpecsData{ carSpeed = 0.5f, carAccelation = 0.42f}},
		{ 16, new CarSpecsData{ carSpeed = 0.52f, carAccelation = 0.44f}},
		{ 17, new CarSpecsData{ carSpeed = 0.54f, carAccelation = 0.46f}},
		{ 18, new CarSpecsData{ carSpeed = 0.56f, carAccelation = 0.48f}},
		{ 19, new CarSpecsData{ carSpeed = 0.58f, carAccelation = 0.5f}},
		{ 20, new CarSpecsData{ carSpeed = 0.6f, carAccelation = 0.52f}},
		{ 21, new CarSpecsData{ carSpeed = 0.62f, carAccelation = 0.54f}},
		{ 22, new CarSpecsData{ carSpeed = 0.64f, carAccelation = 0.56f}},
		{ 23, new CarSpecsData{ carSpeed = 0.66f, carAccelation = 0.58f}},
		{ 24, new CarSpecsData{ carSpeed = 0.68f, carAccelation = 0.6f}},
		{ 25, new CarSpecsData{ carSpeed = 0.7f, carAccelation = 0.62f}},
		{ 26, new CarSpecsData{ carSpeed = 0.72f, carAccelation = 0.64f}},
		{ 27, new CarSpecsData{ carSpeed = 0.74f, carAccelation = 0.66f}},
		{ 28, new CarSpecsData{ carSpeed = 0.76f, carAccelation = 0.68f}},
		{ 29, new CarSpecsData{ carSpeed = 0.78f, carAccelation = 0.7f}},
		{ 30, new CarSpecsData{ carSpeed = 0.8f, carAccelation = 0.72f}},
		{ 31, new CarSpecsData{ carSpeed = 0.82f, carAccelation = 0.74f}},
		{ 32, new CarSpecsData{ carSpeed = 0.84f, carAccelation = 0.76f}},
		{ 33, new CarSpecsData{ carSpeed = 0.86f, carAccelation = 0.78f}},
		{ 34, new CarSpecsData{ carSpeed = 0.88f, carAccelation = 0.8f}},
		{ 35, new CarSpecsData{ carSpeed = 0.9f, carAccelation = 0.82f}},
		{ 36, new CarSpecsData{ carSpeed = 0.92f, carAccelation = 0.84f}},
		{ 37, new CarSpecsData{ carSpeed = 0.94f, carAccelation = 0.86f}},
		{ 38, new CarSpecsData{ carSpeed = 0.96f, carAccelation = 0.88f}},
		{ 39, new CarSpecsData{ carSpeed = 0.98f, carAccelation = 0.9f}},
	};

	public static float GetCarSpeed()
	{
		return carData[DataManager.playerData.playerUnlockedCarIndex].carSpeed;
	}

	public static float GetCarAccelaration()
	{
		return carData[DataManager.playerData.playerUnlockedCarIndex].carAccelation;
	}

	// Call this When the Game Starts.
	public static void Initialize()
	{

	}

}

public struct CarSpecsData
{
	public float carSpeed;
	public float carAccelation;

	public CarSpecsData(float carSpeed, float carAccelation)
	{
		this.carSpeed = carSpeed;
		this.carAccelation = carAccelation;
	}
}
