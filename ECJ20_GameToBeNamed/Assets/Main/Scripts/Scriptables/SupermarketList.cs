using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Class responsible for the main list that will be used during the phase 2.
/// This will contain all the items from the members. Both the current and previous asks items.
/// </summary>
public class SupermarketList : ScriptableObject
{
	List<ItemInSMList> generalListItems= new List<ItemInSMList>();
	List<Item> wrongListItems= new List<Item>();

	[Header("Curr Number of items taken during phase 2")]
	public int numOfItemTaken;


	/// <summary>
	/// Method called to put all the member items in the supermarket list.
	/// Each member will call it by the P1 Game manager
	/// </summary>
	/// <param name="items"></param>
	/// <param name="member"></param>
	public void AddMemberItems(List<Item> items, MemberTypes member)
	{
		// adds each element in the general list
		foreach (Item item in items)
		{
			generalListItems.Add(new ItemInSMList(item, member));
		}
	}


	/// <summary>
	/// Method called during the interaction with the section when an item is picked up from a game manager.
	/// This will add in the general list if the item has been asked, otherwise it will go in the wrong list
	/// </summary>
	/// <param name="itemPicked"></param>
	public void PickUpItem(Item itemPicked)
    {
		for(int i=0; i<generalListItems.Count; i++)
        {
			if (generalListItems[i].itemInfo.myType == itemPicked.myType && generalListItems[i].hasBeenPickedUp == false)
            {
				generalListItems[i].hasBeenPickedUp = true;

				numOfItemTaken++; // so that the updates know the curr amount
				return;
            }
        }
		// if it reaches here, it means that the item was not asked. Therefore, it goes in the wrong list
		wrongListItems.Add(itemPicked);
		numOfItemTaken++;
		// check in the ui if this is the first item in the wrong list, to show the different list area

    }
	
	/// <summary>
	/// Method called from the UI when an Item is removed from 
	/// </summary>
	/// <param name="item"></param>
	public void RemoveItem(int itemIndex)
    {
		wrongListItems.RemoveAt(itemIndex);
		numOfItemTaken--;
		// update num of items curr taken

    }

	/// <summary>
	/// Method called during phase 3 to get all the bought items.
	/// The GM will then call each member and give to each ot them their bought items to remove. [to do]
	/// </summary>
	/// <returns>the only items that were bought and asked</returns>
	public List<ItemInSMList> GetListOfBoughtItems()
    {
		List<ItemInSMList> tmpList= new List<ItemInSMList>();
		foreach(ItemInSMList itemInList in generalListItems)
        {
			if (itemInList.hasBeenPickedUp)
				tmpList.Add(itemInList);
        }
		return tmpList;
    }
	/// <summary>
	/// Method called by the phase 3 GM when the day is concluded after the checking of the items etc
	/// </summary>
	public void ClearListEndDay()
    {
		generalListItems.Clear();
		wrongListItems.Clear();
		numOfItemTaken = 0;
    }

}

/// <summary>
/// Class with the info about the Item in the general info list
/// </summary>
public class ItemInSMList
{
	public Item itemInfo;
	public MemberTypes memberAsking;
	public bool hasBeenPickedUp;

	public ItemInSMList(Item item,MemberTypes member)
    {
		itemInfo = item;
		memberAsking = member;
		hasBeenPickedUp = false;
    }
}