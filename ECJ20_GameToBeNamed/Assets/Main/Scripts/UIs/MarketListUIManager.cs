using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible to handle the MarketList UI Updates etc.
/// </summary>
public class MarketListUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemIconWIthAmountManager [] correctItems; // 12 items

    public TextMeshProUGUI remainingAndTotalText;

    protected SupermarketList marketList;
    List<ItemInSMList> generalList;
    /// <summary>
    /// Method that should be called by the phase 2 manager to get the correct list icons shown
    /// </summary>
    /// <param name="marketList"></param>
    public void InitializeMarketList(SupermarketList mList)
    {
        marketList = mList;
        generalList = mList.GetGeneralList();
        int[] itemsAmount = new int[12];
        // calculate te amount
        foreach(ItemInSMList item in generalList)
        {
            itemsAmount[(int)item.itemInfo.myType]++;
        }

        for(int i=0; i<correctItems.Length; i++)
        {
            correctItems[i].InitializeMe(itemsAmount[i]);
        }

        // update the Text with the taken / Total
        remainingAndTotalText.text = $" {marketList.numOfItemTaken} of {generalList.Count}";
    }

    private void OnEnable()
    {
        //just for now, this should be called by the phase 2 manager
        InitializeMarketList(GameManager.instance.sMList);
    }


    /// <summary>
    /// Method called to have an UI update when an item is changed (on both addiction and removal)
    /// </summary>
    /// <param name="marketList"></param>
    public void UpdateUIList(ItemType itemTypeAdded) //we can have a enum for the type of operation and an index
    {
        //TO CHANGE
        if (marketList == null)
            InitializeMarketList(GameManager.instance.sMList);

        correctItems[(int)itemTypeAdded].ItemPickedUp(); // removes an item from the list

        // update the Text with the taken / Total
        remainingAndTotalText.text = $" {marketList.numOfItemTaken} of {generalList.Count}";
    }


}

/// <summary>
/// enum that will elencate the status of the update happened in the supermarketList
/// </summary>
public enum UpdateType
{
    addInCorrect,
    addInWrong,
    removeFromWrong
}
