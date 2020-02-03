using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataSaver : MonoBehaviour
{
	#region Variables

	public string fileName = "playerData";
	#endregion

	#region Public Methods
	/// <summary>
	///  Load the Data From the Binary File.
	/// </summary>
	/// <returns></returns>
	public object[] LoadData()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Debug.Log("DataSaver - " + Application.persistentDataPath);
		if (!File.Exists(Application.persistentDataPath + "/" + fileName + ".bin"))
			return null;

		using (FileStream fileStream = File.Open(Application.persistentDataPath + "/" + fileName + ".bin", FileMode.OpenOrCreate))
		{
			object[] obj = binaryFormatter.Deserialize(fileStream) as object[];
			return obj;
		}
	}

	/// <summary>
	///  Save the Data to the Binary File.
	/// </summary>
	/// <param name="obj"></param>
	public void SaveData(object[] obj)
	{
		// Create a local instance of Binamy Formatter
		BinaryFormatter binaryFormatter = new BinaryFormatter();


		using (FileStream fileStream = File.Open(Application.persistentDataPath + "/" + fileName + ".bin", FileMode.OpenOrCreate))
		{
			binaryFormatter.Serialize(fileStream, obj);
			fileStream.Close();
		}
	}

	#endregion
}
