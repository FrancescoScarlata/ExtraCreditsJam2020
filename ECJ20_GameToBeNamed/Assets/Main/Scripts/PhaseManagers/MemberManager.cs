using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for the behaviour of the member.
/// - the UI settings
/// - The ask for items etc
/// </summary>
public class MemberManager : MonoBehaviour
{
    public Member myMember;

    [Header("UI elements")]
    public Image memberImage;
    public GameObject listOfTheDay;
    public Image[] itemIconsInDaily;
    public GameObject listOfPreviousItems;
    public Image[] itemIconsInPrevious;

    /// <summary>
    /// Method called by the game manager during phase 1
    /// </summary>
    /// <param name="numOfItems"></param>
    public void InitializeItems(int numOfItems)
    {
        myMember.AskItems(numOfItems);

        // show items inside the list of the day
        ShowTodayListItems();
        // if the list of previous items is not empty, show the area with the previous items
        // show items inside te list of previous Items
        ShowPreviousListItems();
    }

    /// <summary>
    /// Method called to reset all the sprites and making them not enabled.
    /// </summary>
    public void ResetImages()
    {
        
    }

    /// <summary>
    /// Method called to watch the daily items
    /// </summary>
    public void ShowTodayListItems()
    {

    }


    /// <summary>
    /// Method called to watch the previous days items
    /// </summary>
    public void ShowPreviousListItems()
    {

    }


    // during phase 3 some animation when the items are taken by the family?
    //public void InizializePhase3Items

}
