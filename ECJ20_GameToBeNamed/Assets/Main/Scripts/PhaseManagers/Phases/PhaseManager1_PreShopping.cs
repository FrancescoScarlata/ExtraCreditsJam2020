using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for the elements in the inside the phase 1
/// </summary>
public class PhaseManager1_PreShopping : _PhaseManager
{

    [Header("GameObject that is the root for all the stuffin the Phase 1 screen")]
    public GameObject phase1Elements;
    [Header("GameObject of the button to show when the journal is on")]
    public GameObject nextButton;
    // -> to describe this better and write there, but for now it's ok i guess
    public GameObject dateObjects; 

    [Header("The UI of the mmbers")]
    public MemberManager[] familyMembers;
    public Text takeMaskText;


    protected DayInfo thisDayInfos;
    protected bool useMask = false;

    /// <summary>
    /// Method called by the game manager to start the phase 1.
    /// </summary>
    public override void StartPhase()
    {
        thisDayInfos = GameManager.instance.dayInfos[GameManager.instance.currDay];
        useMask = false;
        // open the phase 1 screen
        phase1Elements.SetActive(true);
        
        StartCoroutine(DisplayTheJournal());
       

    }

    /// <summary>
    /// Method called by the next button when the journal is displayed
    /// </summary>
    public void SkipJournal()
    {
        nextButton.SetActive(false);
        // this will start the members showed and the other UI one piece at the time
        StartCoroutine(StartPhase1AfterJournal());
    }


    /// <summary>
    /// Method called from the "Go Shop" button
    /// This will then call the GameManagerTo Start the next phase
    /// </summary>
    public void GoShopping()
    {
        GameManager.instance.NextPhase();
    }

    /// <summary>
    /// Method called by the button to take or not take the mask.
    /// Can have a better text or just an icon with the mask yes/no check
    /// </summary>
    public void UseTheMask()
    {
        if (useMask)
        {
            useMask = false;
            takeMaskText.text = "Take the mask";
        }
        else
        {
            useMask = true;
            takeMaskText.text = "Don't take the mask";
        }
            
    }


    /// <summary>
    /// Method called to end this phase.
    /// </summary>
    public override void EndPhase()
    {
        // Fade should be called from the game manager
        phase1Elements.SetActive(false);
    }

    /// <summary>
    /// Method called during the phase one.
    /// It will start the display journal and wait a few seconds before enabling to skip the journal
    /// </summary>
    /// <returns></returns>
    protected IEnumerator DisplayTheJournal()
    {
        yield return new WaitForSeconds(2); // after 2 seconds, the Next button will appear
        nextButton.SetActive(true);
    }

    /// <summary>
    /// TO DO
    /// This method is called To make the phase 1 element show with the right times 
    /// </summary>
    /// <returns></returns>
    protected IEnumerator StartPhase1AfterJournal()
    {
        yield return null;

        // add the date in place -> after maybe with an animation
        dateObjects.SetActive(true);
        yield return new WaitForSeconds(1);

        //reset all the MemberManagerUI
        

        // Activate the members active of this day
        // show the new items and eventual previous items

        // Activate the members with old items, but not ask for new items
        // show the previous items



        // show the buttons

        // I

    }


}
