using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "distancingInfos", menuName = "Infos/DistacingInfos", order = 1)]
public class InfectionInfos : ScriptableObject
{

    public Sprite maskOverlay;
    public Sprite noMaskOverlay;

    public float radiusWithMask = 1.5f;
    public float radiusWithoutMask = 3f;

    public Color overlayColorWhenDistancing;
    public Color overlayColorNearSomeone;


    public float minChangeToGetItWithNoMaskPeople = 0.2f;
    public float increasedChancePerSecond = 0.05f;
}
