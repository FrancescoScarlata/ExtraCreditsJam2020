using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible to handle the MarketList UI Updates etc.
/// </summary>
public class MarketListUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Image> listOfCorrectItemsToBuy;
    
    public GameObject listOfWrongItemsGO;
    public List<WrongButtonManager> listOfWrongItems;

    public Text remainingAndTotalText;

    protected SupermarketList marketList;
    List<ItemInSMList> generalList;
    /// <summary>
    /// Method that should be called by the phase 2 manager to get the correct list icons shown
    /// </summary>
    /// <param name="marketList"></param>
    public void InitializeMarketList(SupermarketList mList)
    {
        marketList = mList;
        // reset each item in the list of correct items by disabling each image.
        foreach(Image im in listOfCorrectItemsToBuy)
        {
            im.enabled = false;
        }

        //disable the listOfWrongItemsGO
        listOfWrongItemsGO.SetActive(false);
        // and reset the wrong items buttons (they can be clicked, so they need to be buttons)


        generalList = mList.GetGeneralList();

        // for each element in the market list, insert the not active state of the icon
        for(int i=0; i<generalList.Count; i++)
        {
            listOfCorrectItemsToBuy[i].sprite = generalList[i].itemInfo.notActiveIcon;
            listOfCorrectItemsToBuy[i].enabled = true;
        }

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
    public void UpdateUIList(UpdateType typeOfUpdate, int index) //we can have a enum for the type of operation and an index
    {
        if (marketList == null)
            InitializeMarketList(GameManager.instance.sMList);
        switch (typeOfUpdate)
        {
            case UpdateType.addInCorrect:
                // in the index, change from not active to active
                listOfCorrectItemsToBuy[index].sprite = generalList[index].itemInfo.activeIcon;
                // some particle effect on the position of the icon might be cool to give a feedback
                break;

            case UpdateType.addInWrong:
                if (index == 1) 
                {
                    // first item, we need to activate the list of wrong item GO
                    listOfWrongItemsGO.SetActive(true);
                }

                // update the button in the index position-1 with the new infos
                listOfWrongItems[index - 1].InititializeTheButton(marketList.GetItemFromWrongAt(index - 1), index - 1); // to test out
                Debug.Log($"[POST] The last add in the list is: {listOfWrongItems[index - 1].GetThisItem().name} in index {index - 1}");
                // some particle effect on the position of the icon might be cool to give a feedback

                break;

            case UpdateType.removeFromWrong:

                // update the button in the index position by disabling it and put it to the end of the list
                listOfWrongItems[index].ResetItem();
                WrongButtonManager tmp = listOfWrongItems[index];
                listOfWrongItems.RemoveAt(index);
                listOfWrongItems.Add(tmp); // it should add as last item
                if (index == 0)
                {
                    // if there are not any more wrong items, the wrong list of item should be hidden again
                   
                    listOfWrongItemsGO.SetActive(false); // here can go some animation, time permitted
                }
                else
                {
                    //Updates the buttonw with the new index in the list
                    for(int i=0; i<listOfWrongItems.Count; i++)
                    {
                        if(listOfWrongItems[i].wrongIconImage.enabled)
                        {
                            listOfWrongItems[i].UpdateMyIdex(i);
                        }
                    }
                }
                // some particle effect on the position of the icon might be cool to give a feedback
                break;
        }

        // update the Text with the taken / Total
        remainingAndTotalText.text = $" {marketList.numOfItemTaken} of {generalList.Count}";
    }



    /// <summary>
    /// This method will be used when an item has been updated in the wrong list.
    /// It will update the index of the button so that it will sta correctly when some item is pick up or removed.
    /// We'll see the implementation
    /// </summary>
    protected void UpdateWrongListOfButtos()
    {

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
