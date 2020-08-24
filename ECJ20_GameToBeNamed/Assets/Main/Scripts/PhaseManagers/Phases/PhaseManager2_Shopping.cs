using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// Class responsible for the second phase of the cycle: the supermarket phase
/// </summary>
public class PhaseManager2_Shopping : _PhaseManager
{
    public GameObject timerObjects;
    public TextMeshProUGUI timer;

    public AIDirector AIDir;

    public Transform player;
    public Transform entranceTransform;

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
      

        GameManager.instance.sMMan.RestockTheSections(thisDayInfos.sectionsToRestock);
        GameManager.instance.sMMan.SetSectionsPromos(thisDayInfos.sectionsWithPromos);

        player.position = entranceTransform.position;

        //Initialize the People with the positioning etc -> TO DO the AI director etc
        AIDir.InitializeDirector(thisDayInfos);

        UIManager.instance.FadeIn();
        GameManager.instance.mListUIMan.InitializeMarketList(GameManager.instance.sMList);
        yield return new WaitForSeconds(1.2f);

        timerObjects.SetActive(thisDayInfos.isTimerOn);
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
            timer.text = $" {Mathf.FloorToInt( time/60)} : {time%60}";
        }
        //Timer is ended
        GameManager.instance.NextPhase();
    }



    IEnumerator EndPhaseWithPauses()
    {
        UIManager.instance.FadeIn();
        AIDir.HideClients();
        // stop the AI from moving
        yield return new WaitForSeconds(1.2f);
        
        thisPhaseRoot.SetActive(false);
    }

}
