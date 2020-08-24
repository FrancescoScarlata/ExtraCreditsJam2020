using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class responsible for the Phase 3 elements
/// </summary>
public class PhaseManager3_DayResults : _PhaseManager
{

    // UI elements
    public TextMeshProUGUI itemPickedText;
    public TextMeshProUGUI itemsTotalText;

    public MemberFeedbackController[] familyFeedbacks; // it has to update based on which member was active today

    public TextMeshProUGUI playerInfectionResultText;

    public string infectionTaken = "You feel a bit light-headed on your way home.During dinner, ";
    public Member[] members; // the 4 scriptable in the same order as below

    public Image[] bottomMemberIcons; // the 4 members, always there

    public Button continueButton;

    protected SupermarketList smList;



    public override void StartPhase()
    {
        smList = GameManager.instance.sMList;
        StartCoroutine(StartPhaseWithPauses());
    }

    IEnumerator StartPhaseWithPauses()
    {
        thisPhaseRoot.SetActive(true);

        continueButton.gameObject.SetActive(false);

        itemPickedText.text = smList.numOfItemTaken.ToString();
        itemsTotalText.text = smList.GetGeneralList().Count.ToString();

        int[] numItemsAskedForMember= new int[4];
        for( int i=0; i<members.Length; i++)
        {
            numItemsAskedForMember[i]= members[i].GetItemsToBuy().Count;
        }

        List<Item>[] itemsPerMember = new List<Item>[4];

        for (int i=0; i<itemsPerMember.Length; i++ )
        {
            itemsPerMember[i] = new List<Item>();
        }

        // divides the items per each member.
        foreach(ItemInSMList item in smList.GetGeneralList())
        {
            if (item.hasBeenPickedUp)
            {
                //Debug.Log($" memberasking: {(int)item.memberAsking}");
                //Debug.Log($" List: {itemsPerMember[(int)item.memberAsking]}");
                itemsPerMember[(int)item.memberAsking].Add(item.itemInfo);
            }
               

        }

        // sends to the members the elemens bought
        for (int i = 0; i < itemsPerMember.Length; i++)
        {
            members[i].CheckItemsBought(itemsPerMember[i].ToArray());
        }

        // calculate the amount of item asked before and the amount of curr items bought
        int[] remainingItemsForMember = new int[4];
        for (int i = 0; i < members.Length; i++)
        {
            remainingItemsForMember[i] = members[i].GetItemsToBuy().Count;
        }

        // checks who got infected : this is the infection review Text
        if (GameManager.instance.isPlayerInfected)
        {
            bool isOkPersonFound = false;
            // check if which person is OK. fist ok will get sick
            for (int i = 0; i < members.Length; i++)
            {
                if (members[i].myState == MemberState.normal) // say infected result
                {
                    members[i].myState = MemberState.infected;
                    isOkPersonFound = true;
                    int phraseToSay = Random.Range(0, familyFeedbacks[0].reactions.infectionPhrasesSeparated1.Length);
                    playerInfectionResultText.text = familyFeedbacks[0].reactions.infectionPhrasesSeparated1[phraseToSay] +
                                        members[i].myID.ToString() + familyFeedbacks[0].reactions.infectionPhrasesSeparated2[phraseToSay];
                }

            }
            if (!isOkPersonFound) 
            {
                //if are all infected, someone would die
                for (int i = 0; i < members.Length; i++)
                {
                    if (members[i].myState == MemberState.infected) // say dead result
                    {
                        members[i].myState = MemberState.dead;
                        int phraseToSay = Random.Range(0, familyFeedbacks[0].reactions.deadPhrasesSep1.Length);
                        playerInfectionResultText.text = familyFeedbacks[0].reactions.deadPhrasesSep1[phraseToSay] +
                                        members[i].myID.ToString() + familyFeedbacks[0].reactions.deadPhrasesSep2[phraseToSay];
                    }

                }
            }
        }
        else // player not infected
        {
            playerInfectionResultText.text = familyFeedbacks[0].reactions.notInfectionPhrases[Random.Range(0, familyFeedbacks[0].reactions.notInfectionPhrases.Length)];
        }

        //depending of the calculations give the player, update the reaction
        for (int i=0; i<familyFeedbacks.Length; i++)
        {
            if (numItemsAskedForMember[i] > 0)
            {
                familyFeedbacks[i].gameObject.SetActive(true);
                if (remainingItemsForMember[i] == 0)
                    familyFeedbacks[i].UpdateFeedback(FeedbackType.positive);
                else
                {
                    if (numItemsAskedForMember[i] > remainingItemsForMember[i])
                    {
                        familyFeedbacks[i].UpdateFeedback(FeedbackType.neutral);
                    }
                    else
                    {
                        familyFeedbacks[i].UpdateFeedback(FeedbackType.negative);
                    }
                }
            }
            else
            {
                familyFeedbacks[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < members.Length; i++)
        {
            switch(members[i].myState)
            {
                case MemberState.normal:
                    bottomMemberIcons[i].sprite = members[i].memberOk;
                    break;
                case MemberState.infected:
                    bottomMemberIcons[i].sprite = members[i].memberInfected;
                    break;
                case MemberState.dead:
                    bottomMemberIcons[i].sprite = members[i].memberDead;
                    break;
            }
        }

        UIManager.instance.FadeIn();
        yield return new WaitForSeconds(3f);

        continueButton.gameObject.SetActive(true);

    }



    public override void EndPhase()
    {
        StartCoroutine(EndPhaseWithPauses());
    }


    IEnumerator EndPhaseWithPauses()
    {
        UIManager.instance.FadeOut();
        
        yield return new WaitForSeconds(1.2f);

        // clear all suff
        for (int i = 0; i < familyFeedbacks.Length; i++)
        {
            familyFeedbacks[i].gameObject.SetActive(false);
        }
        GameManager.instance.sMList.ClearListEndDay();

        thisPhaseRoot.SetActive(false);

    }


    public void TestResetSupermarketList()
    {
        GameManager.instance.sMList.ClearListEndDay();
    }

}
