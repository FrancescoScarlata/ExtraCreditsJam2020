using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for the elements in the inside the phase 1
/// </summary>
public class PhaseManager1_PreShopping : _PhaseManager
{
    [Header("GameObject of the button to show when the journal is on")]
    public GameObject nextButton;
    // -> to describe this better and write there, but for now it's ok i guess
    public TextMeshProUGUI dateText;
    public GameObject journalElements; // elements that should be a go that has inside the journal etc
    public Image newspaperImage;
    [Header("The UI elements")]
    public MemberManager[] familyMembers;
    public Button maskButton;
    public Button shopButton;
    public Text takeMaskText;


    protected DayInfo thisDayInfos;
    protected bool useMask = false;

    /// <summary>
    /// Method called by the game manager to start the phase 1.
    /// </summary>
    public override void StartPhase()
    {
        UIManager.instance.FadeIn();
        Debug.Log("I'm in start phase!");
        thisDayInfos = GameManager.instance.dayInfos[GameManager.instance.currDay];
        useMask = false;
        // open the phase 1 screen
        base.thisPhaseRoot.SetActive(true);
        journalElements.SetActive(true);

        StartCoroutine(DisplayTheJournal());
        
    }

    /// <summary>
    /// Method called by the next button when the journal is displayed
    /// </summary>
    public void SkipJournal()
    {
        nextButton.SetActive(false);
        journalElements.SetActive(false);
        
        // this will start the members showed and the other UI one piece at the time
        StartCoroutine(StartPhase1AfterJournal());
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
        GameManager.instance.isPlayerWithMask = useMask;
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
    /// Method called to end this phase.
    /// </summary>
    public override void EndPhase()
    {
        StartCoroutine(EndPhaseWithPauses());
    }

    /// <summary>
    /// Method called during the phase one.
    /// It will start the display journal and wait a few seconds before enabling to skip the journal
    /// </summary>
    /// <returns></returns>
    protected IEnumerator DisplayTheJournal()
    {
        newspaperImage.sprite = thisDayInfos.newsPaperOfTheDay;
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
        dateText.gameObject.SetActive(true);
        dateText.text = thisDayInfos.currentDateToShow;
        yield return new WaitForSeconds(1);

        //reset all the MemberManagerUI
        foreach(MemberManager member in familyMembers)
        {
            member.ResetImages();
        }

        // Activate the members active of this day
        // show the new items and eventual previous items
        for (int i=0; i<thisDayInfos.memberActive.Length; i++)
        {
            if (thisDayInfos.memberActive[i])
            {
                familyMembers[i].gameObject.SetActive(true);
                familyMembers[i].InitializeItems(Random.Range(thisDayInfos.minNumOfItemsAsked, thisDayInfos.maxNumOfItemsAsked));
                yield return new WaitForSeconds(1f);
            }
            else
            {
                // Activate the members with old items, but not ask for new items
                // show the previous items
                if (familyMembers[i].myMember.GetPrevItems().Count > 0)
                {
                    familyMembers[i].gameObject.SetActive(true);
                    familyMembers[i].ShowPreviousListItems();
                    yield return new WaitForSeconds(0.7f);
                }
            }
            
        }

        // show the buttons
        //maskButton.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        shopButton.gameObject.SetActive(true);


    }



    IEnumerator EndPhaseWithPauses()
    {
        UIManager.instance.FadeOut();
        foreach (MemberManager member in familyMembers)
        {
            GameManager.instance.sMList.AddMemberItems(member.myMember.GetItemsToBuy(), member.myMember.myID);
        }
        yield return new WaitForSeconds(1.5f);
        // Fade should be called from the game manager
       
        thisPhaseRoot.SetActive(false);

        dateText.gameObject.SetActive(false);
        maskButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);

        // populates the supermarket List
        foreach (MemberManager member in familyMembers)
        {
            member.gameObject.SetActive(false);
        }
        Debug.Log("Family lists added in the supermarketList");
    }

}
