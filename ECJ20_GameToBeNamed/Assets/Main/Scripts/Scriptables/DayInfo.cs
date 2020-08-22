using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will have inside all the infos about the current day, so that we ca pick customize the parameters for each day
/// </summary>
[CreateAssetMenu(fileName = "newDay", menuName = "Infos/DayInfo", order = 1)]
public class DayInfo : ScriptableObject
{

    [Header("Phase 1 parameters")]

    public DateTime currentDateToShow; // if necessary

    //public int numberOFamilyfMembers;
    public bool[] memberActive = new bool[5]; // probably just 4 to be in the same side i guess
    public int minNumOfItemsAsked;
    public int maxNumOfItemsAsked;

   

    [Header("Phase 2 parameters")]
    public int amountOfPeople;
    public int amountOfSickPeople;

    // this will give which section in the supermarket phase should be restocked
    public bool[] sectionsToRestock= new bool [12];

    // this will give which section in the supermarket phase should be in promo
    public bool[] sectionsWithPromos = new bool[12];

    public bool isTimerOn;

    public float secondsOfTimer;


}

