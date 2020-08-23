﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Scriptable with te infos about the reactions.
/// Here you can find the color for the bubble bg and the phrase
/// </summary>
[CreateAssetMenu(fileName = "reactionInfos", menuName = "Infos/Reactions", order = 1)]
public class MemberReactionInfos : ScriptableObject
{
    public Color[] reactionColors= new Color[3]; // pos, neutral, neg

    public string[] positivePhrases;

    public string[] neutralPhrases;

    public string[] negativePhrases;

}
