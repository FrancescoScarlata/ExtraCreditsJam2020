using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for the animation of the characters, both player and IA
/// </summary>
public class CharAnimationController : MonoBehaviour
{
    public Animator myAnim;

    public GameObject upFace;
    public GameObject downFace;


    protected float speed;

    protected bool isGoingUp = false;

    /// <summary>
    /// Method called to update the character animation so that it is updating
    /// </summary>
    /// <param name="newSpeed"></param>
    /// <param name="newIsGoingUp"></param>
    public void UpdateCharacterAnimation(float newSpeed, bool newIsGoingUp)
    {

        myAnim.SetFloat("Speed", newSpeed);

        if (isGoingUp != newIsGoingUp)
        {
            if(newIsGoingUp)
            {               
                downFace.SetActive(false);
                upFace.SetActive(true);
            }
            else
            {
                upFace.SetActive(false);
                downFace.SetActive(true);
            }
            isGoingUp = newIsGoingUp;
        }

    }


}
