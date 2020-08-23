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


    public override void StartPhase()
    {
        StartCoroutine(StartPhaseWithPauses());
    }

    IEnumerator StartPhaseWithPauses()
    {
        thisPhaseRoot.SetActive(true);

        // Member Updates
        // Moving items from asked to prev if not bought
        // Update the images if a member got infected etc
        // new infos



        UIManager.instance.FadeIn();
        yield return new WaitForSeconds(1.2f);

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
        GameManager.instance.sMList.ClearListEndDay();

        thisPhaseRoot.SetActive(false);

    }


    public void TestResetSupermarketList()
    {
        GameManager.instance.sMList.ClearListEndDay();
    }

}
