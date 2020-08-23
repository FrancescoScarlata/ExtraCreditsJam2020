using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TO DO
/// </summary>
public class PhaseManager3_DayResults : _PhaseManager
{

    public override void StartPhase()
    {
        //fade
        thisPhaseRoot.SetActive(true);

        // Member Updates
            // Moving items from asked to prev if not bought
            // Update the images if a member got infected etc
            // new infos

    }

    public override void EndPhase()
    {
        //fade
        thisPhaseRoot.SetActive(false);
        GameManager.instance.sMList.ClearListEndDay();
        //
    }



    public void TestResetSupermarketList()
    {
        GameManager.instance.sMList.ClearListEndDay();
    }

}
