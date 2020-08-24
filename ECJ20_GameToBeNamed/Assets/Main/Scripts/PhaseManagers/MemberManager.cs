using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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
        
        switch (myMember.myState)
        {
            case MemberState.normal:
                memberImage.sprite = myMember.memberOk;
                break;
            case MemberState.infected:
                memberImage.sprite = myMember.memberInfected;
                break;
            case MemberState.dead:
                memberImage.sprite = myMember.memberDead;
                gameObject.SetActive(false);
                return;
        }
       
        
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
        // hides the images
        foreach(Image item in itemIconsInDaily)
        {
            item.enabled = false;
        }
        foreach (Image item in itemIconsInPrevious)
        {
            item.enabled = false;
        }
        // hides the object with the previous items
        listOfPreviousItems.SetActive(false);
    }

    /// <summary>
    /// Method called to show the daily items
    /// </summary>
    public void ShowTodayListItems()
    {
        List<Item> todayItems = myMember.GetTodayItems();
        for (int i = 0; i < todayItems.Count; i++)
        {
            itemIconsInDaily[i].sprite = todayItems[i].activeIcon;
            itemIconsInDaily[i].enabled = true;
        }
        if (myMember.GetPrevItems().Count > 0)
        {
            ShowPreviousListItems();
        }
    }


    /// <summary>
    /// Method called to show the previous days items
    /// </summary>
    public void ShowPreviousListItems()
    {
        List<Item> prevItems = myMember.GetPrevItems();
        if (prevItems.Count == 0)
            return;

        listOfPreviousItems.SetActive(true);

        for (int i = 0; i < prevItems.Count; i++)
        {
            itemIconsInPrevious[i].sprite = prevItems[i].activeIcon;
            itemIconsInPrevious[i].enabled = true;
        }
    }



    // during phase 3 some animation when the items are taken by the family?
    //public void InizializePhase3Items

}
