using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class responsible for the general elements of the Game
/// </summary>
public class GameManager : MonoBehaviour
{
    // the element that you call to get this item
    public static GameManager instance;

    public SupermarketList sMList;
    public SupermarketManager sMMan;

    public _PhaseManager[] phases = new _PhaseManager[3]; // these will be be linked to the actal 3 phases

    public int currDay;
    public int currPhase = 0;
    public bool hasPlayerGotInfectiousToday = false;
    // public dayInfos;
    //


    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        if (newItem != null)
        {
            sMList.PickUpItem(newItem);
            //update the supermarket UI list in some way
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
    public void RemoveItem(Item itemToRemove)
    {
        sMMan.TakeBackItem(itemToRemove.myType);
        // update the supermarket list UI
    }
    



}
