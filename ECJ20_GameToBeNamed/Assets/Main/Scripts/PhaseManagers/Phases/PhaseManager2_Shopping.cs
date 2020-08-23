using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class responsible for the second phase of the cycle: the supermarket phase
/// </summary>
public class PhaseManager2_Shopping : _PhaseManager
{
    // Timer Text

    // public AIDirector

    protected DayInfo thisDayInfos;

    WaitForSeconds waitASec = new WaitForSeconds(1);

    public override void StartPhase()
    {
        StartCoroutine(StartPhaseWithPauses());

    }

    IEnumerator StartPhaseWithPauses()
    {      
        thisPhaseRoot.SetActive(true);
        thisDayInfos = GameManager.instance.dayInfos[GameManager.instance.currDay];
        // initialize the supermarket UI list
        GameManager.instance.mListUIMan.InitializeMarketList(GameManager.instance.sMList);

        GameManager.instance.sMMan.RestockTheSections(thisDayInfos.sectionsToRestock);
        GameManager.instance.sMMan.SetSectionsPromos(thisDayInfos.sectionsWithPromos);

        //Initialize the People with the positioning etc -> TO DO the AI director etc

        UIManager.instance.FadeIn();
        yield return new WaitForSeconds(1.5f);

        if (thisDayInfos.isTimerOn)
        {
            // Start Timer
            StartCoroutine(StartTimer(thisDayInfos.secondsOfTimer + 5));// just to give 5 seconds more, but probably it's not necessary
        }

        //Enable player controller, otherwise it can start moving first ?
    }


    public override void EndPhase()
    {
        StartCoroutine(EndPhaseWithPauses());

    }

    /// <summary>
    /// Method called to start the timer. When it ends
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    protected IEnumerator StartTimer(float time)
    {
 
        while (time > 0)
        {
            time -= 1;
            yield return waitASec;  
            // Text display time remaining in a legible way
        }
        //Timer is ended
        GameManager.instance.NextPhase();
    }



    IEnumerator EndPhaseWithPauses()
    {
        UIManager.instance.FadeIn();
        // stop the AI from moving
        yield return new WaitForSeconds(1.2f);
        
        thisPhaseRoot.SetActive(false);
    }

}
