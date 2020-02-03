
[System.Serializable]
public class CarItem 
{
	public int carIndex;
	public string carName;
	public long carPrice;
	public bool isUnlocked;
	public CarSpecs carSpec;
}


[System.Serializable]
public class CarSpecs
{
	public int accelaration;
	public int speed;
	public int carLevel;
}
