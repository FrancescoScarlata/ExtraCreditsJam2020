using System.Collections;
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

    public string[] notInfectionPhrases;

    [Header("The 2 parts of the infection phrase (they both need to have the same length)")]
    public string[] infectionPhrasesSeparated1;
    public string[] infectionPhrasesSeparated2;

    [Header("The 2 parts of the dead phrase (they both need to have the same length)")]
    public string[] deadPhrasesSep1;
    public string[] deadPhrasesSep2;

}
