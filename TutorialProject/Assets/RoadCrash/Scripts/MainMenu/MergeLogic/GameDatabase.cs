using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "RoadCrash/GameDatabase", fileName ="GameDatabase")]
public class GameDatabase : ScriptableObject
{
	public List<Sprite> items;
}
