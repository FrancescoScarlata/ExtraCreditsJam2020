using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

/// <summary>
/// Class responsible for the PlayerInfection 
/// </summary>
public class PlayerInfectionController : MonoBehaviour
{
    public CircleCollider2D myColl; // the circle collider that will check for clients
    public SpriteRenderer myRend;
    public InfectionInfos myInfos; // the scriptable object with all the infos for the infections

    protected float startingTime=0;
    protected bool isSomeoneInfected;

    protected List<Collider2D> clientCollisions=new List<Collider2D>();
    public void OnEnable()
    {
        if (GameManager.instance && GameManager.instance.isPlayerWithMask)
        {
            myColl.radius = myInfos.radiusWithMask;
            // also we need the feedback circle
            myRend.sprite = myInfos.maskOverlay;
        }
        else
        {
            myColl.radius = myInfos.radiusWithoutMask;
            //circle bigger
            myRend.sprite = myInfos.noMaskOverlay;
        }
        clientCollisions.Clear();
        myRend.color = myInfos.overlayColorWhenDistancing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Client")
        {
            if (clientCollisions.Count == 0) // this will check and reset the time inside
            {
                startingTime = Time.time;
                isSomeoneInfected = false;
            }
                
            myRend.color = myInfos.overlayColorNearSomeone;
            //do something here
            clientCollisions.Add(collision);
            // checks if person is infected
            if (collision.GetComponent<ClientController>().isInfected)
                isSomeoneInfected = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Client")
        {
            myRend.color = myInfos.overlayColorWhenDistancing;
            //do something here
            clientCollisions.Remove(collision);
            if (clientCollisions.Count == 0) // no other people inside
            {
                //if one person around was infected, then calcultate
                if(isSomeoneInfected)
                    CalculateChanceOfInfection(Time.time - startingTime);
            }
        }

        
    }

    /// <summary>
    /// In theory this is wrong, because we should calculate for each person and keep for each person a score.
    /// This way is faster to calculate.
    /// If at least one person during the not distancing period was infected, it will give an high chance of infection
    /// </summary>
    /// <param name="duration"></param>
    protected void CalculateChanceOfInfection(float duration)
    {
        Debug.Log("CalculatingChance! Someone was infectious");
        //              0.2 min                                      + 0.05 * second (to tune maybe)
        float chance = myInfos.minChangeToGetItWithNoMaskPeople + duration * myInfos.increasedChancePerSecond;

        if (Random.Range(0, 1f) <= chance)
        {
            GameManager.instance.PlayerInfection();
            Debug.Log($"Infected! with {chance*100}%");
        }
    }

}
