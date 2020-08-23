using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class that will handle the feedback infos
/// </summary>
public class MemberFeedbackController : MonoBehaviour
{
    public Image memberImage;

    public Image bubbleBGImage;
    public TextMeshProUGUI feedbackText;
    public Member myMember;

    public MemberReactionInfos reactions;

    /// <summary>
    /// Method called from the phase 3 to update the values
    /// </summary>
    /// <param name="feedType"></param>
    public void UpdateFeedback(FeedbackType feedType)
    {
        // updates the member icon
        switch (myMember.myState) 
        {
            case MemberState.normal:
                memberImage.sprite = myMember.memberOk;
                break;
            case MemberState.infected:
                memberImage.sprite = myMember.memberInfected;
                break;
            case MemberState.dead:
                memberImage.sprite = myMember.memberDead;
                break;
        }

        // updates the message and bg
        switch (feedType) 
        {
            case FeedbackType.positive:
                bubbleBGImage.color = reactions.reactionColors[0];
                feedbackText.text = reactions.positivePhrases[Random.Range(0, reactions.positivePhrases.Length)];

                break;

            case FeedbackType.neutral:
                bubbleBGImage.color = reactions.reactionColors[1];
                feedbackText.text = reactions.neutralPhrases[Random.Range(0, reactions.positivePhrases.Length)];

                break;

            case FeedbackType.negative:
                bubbleBGImage.color = reactions.reactionColors[2];
                feedbackText.text = reactions.negativePhrases[Random.Range(0, reactions.positivePhrases.Length)];

                break;
        }
    }


}

/// <summary>
/// Enum for the state of the feedback
/// </summary>
public enum FeedbackType
{
    positive,
    neutral,
    negative
}
