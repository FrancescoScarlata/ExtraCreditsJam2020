using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Class responsible for the general elements of the Game
/// </summary>
public class GameManager : MonoBehaviour
{
    // the element that you call to get this item
    public static GameManager instance;

    public SupermarketList sMList;
    public SupermarketManager sMMan;
    public MarketListUIManager mListUIMan;

    public _PhaseManager[] phases = new _PhaseManager[3]; // these will be be linked to the actal 3 phases

    public int currDay;
    public int currPhase = 0;
    public bool hasPlayerGotInfectiousToday = false;
    public DayInfo [] dayInfos;
    //


    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Method to initialize the things at the start of a run.
    /// For now it's just to reset the member elements
    /// </summary>
    public void NewGame()
    {
        foreach(MemberManager member in ((PhaseManager1_PreShopping)phases[0]).familyMembers)
        {
            member.myMember.NewGame(); // this will clear the item lists, so that no item will be there
        }
    }

    /// <summary>
    /// Method called to move to the next phase
    /// </summary>
    public void NextPhase()
    {
        // the next phase will open after a fade in/out
        
        // next phase called
    }


    /// <summary>
    /// Method called by the section interactions
    /// </summary>
    /// <param name="newItem">the item that was picked up</param>
    public void InsertItem(Item newItem)
    {
        int resultPickUp;
        if (newItem != null)
        {
            resultPickUp= sMList.PickUpItem(newItem);
            //update the supermarket UI list in some way
            if (resultPickUp > 0)
                mListUIMan.UpdateUIList(UpdateType.addInCorrect, resultPickUp);
            else
                mListUIMan.UpdateUIList(UpdateType.addInWrong, -resultPickUp);
        }
        else
        {
            // feedback that not item pick up
            
        }
    }

    /// <summary>
    /// Method called from the SupermarketUI manager when a item is removed
    /// </summary>
    /// <param name="itemToRemove"></param>
    public void RemoveItem(int index)
    {
        Item itemRemoved = sMList.GetItemFromWrongAt(index);
        sMList.RemoveItem(index);
        sMMan.TakeBackItem(itemRemoved.myType);
        // update the supermarket list UI
        mListUIMan.UpdateUIList(UpdateType.removeFromWrong, index);
    }
    



}
