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
        throw new System.NotImplementedException();
    }

    public override void EndPhase()
    {
        throw new System.NotImplementedException();
    }



    public void TestResetSupermarketList()
    {
        GameManager.instance.sMList.ClearListEndDay();
    }

}
