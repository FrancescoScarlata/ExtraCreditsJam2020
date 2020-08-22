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
    public GameObject listOfPreviousItems;


    /// <summary>
    /// Method called by the game manager during phase 1
    /// </summary>
    /// <param name="numOfItems"></param>
    public void InitializeItems(int numOfItems)
    {
        myMember.AskItems(numOfItems);

        // show items inside the list of the day

        // if the list of previous items is not empty, show the area with the previous items

        // show items insid te list of previous Items

    }

   // during phase 3 some animation when the items are taken by the family?
   //public void InizializePhase3Items

}
