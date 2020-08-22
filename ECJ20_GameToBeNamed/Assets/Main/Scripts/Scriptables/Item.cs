using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class with the infos about the items
/// </summary>
[CreateAssetMenu(fileName = "newItem", menuName = "Infos/Item", order = 1)]
public class Item : ScriptableObject
{
	public ItemType myType;
	public Sprite activeIcon;
	public Sprite notActiveIcon; // used on the list when the item is not picked up yet
	public Sprite wrongItemIcon; // used when the 

	public Sprite itemInBasket; // the sprite that can be viewed inside the basket/shelf

}

public enum ItemType
{
	Medicine,
	Milk,
	Bread,
	Eggs,
	Beans,
	Meat,
	ToiletPaper,
	Soap,
	Fish,
	Fruit,
	Chocolate,
	Salt
}
