using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Classwith some infos about the client
/// </summary>
public class ClientController : MonoBehaviour
{
    [Header("ClientInfos")]
    public float movSpeed=3;
    public bool isInfected;
    public float minTimeToBuyAnItem = 2f;
    public float maxTimeToBuyAnItem = 4f;
    public float distanceToReachANode = 0.2f;
    public Color infectedColor=Color.green;

    [Header("Components to setup")]
    public CharAnimationController myAnim;
    public Rigidbody2D myRB;
    public SimpleFMODAudioSource cough;
    public SimpleFMODAudioSource sneeze;

    protected bool itemPickedUp=false;

    protected int indexForDirector=-1;
    protected PathToFollow myPath;
    protected int currNextNodeIndex;
    protected bool isMoving;
    protected AIDirector myDir;

    /// <summary>
    /// Method called to reset the client pick up
    /// </summary>
    public void ResetClient()
    {
        itemPickedUp = false;
        myPath = null;
    }

    /// <summary>
    /// Method called by the AI director. Will stop the client, make it start from a differentPosition
    /// </summary>
    /// <param name="newPosition"></param>
    public void Reposition(Vector3 newPosition)
    {
        gameObject.SetActive(false);
        transform.position = newPosition;
    }


    public void StopMoving()
    {
        isMoving = false;
        myRB.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called by the AI direction when it needs the player back in the game after it was out of the game
    /// </summary>
    /// <param name="myNewPath"></param>
    public void StartMoving(PathToFollow myNewPath,int index, AIDirector dir)
    {
        if (myDir == null)
            myDir = dir;
        
        indexForDirector = index;
        myPath = myNewPath;
        currNextNodeIndex = 0;
        isMoving = true;
        itemPickedUp = false;

        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ClientBehaviour());
    }



    /// <summary>
    /// Method cal
    /// </summary>
    /// <param name="section"></param>
    protected void PickUpItem(SectionManager section)
    {
        Debug.Log("ItemPicked");
        if(section)
            section.TakeItem();
    }

    /// <summary>
    /// Method called to get the wait time, and also checks which type of node was reached
    /// </summary>
    /// <returns></returns>
    protected float NodeReached()
    {
        if (myPath.nodes[currNextNodeIndex].isThisASectionNode)
        {
            PickUpItem(myPath.nodes[currNextNodeIndex].mySection);
            return Random.Range(minTimeToBuyAnItem, maxTimeToBuyAnItem);
        }
        // do a wait if asked // here because the bluish looks bad for a long period of time
        if (isInfected)
        {

            if (Random.Range(0, 2) % 2 == 0)
            {
                cough.Play();
            }
            else
                sneeze.Play();

            myAnim.TurnBluish(infectedColor);
        }

        return myPath.nodes[currNextNodeIndex].timeToWaitInThisNode;
    }

    /// <summary>
    /// Method that handles the movement of the character
    /// </summary>
    /// <returns></returns>
    IEnumerator ClientBehaviour()
    {
        Vector2 dir;
        float timeToWait;
        while (isMoving &&myPath!=null && currNextNodeIndex < myPath.nodes.Length)
        {
            dir = myPath.nodes[currNextNodeIndex].transform.position - transform.position;
            // reach the current node
            while (dir.magnitude >= distanceToReachANode)
            {
                //Debug.Log($"Distance: {dir.magnitude}");
                myRB.velocity = dir.normalized * movSpeed*Time.deltaTime;
                if (myAnim)
                    myAnim.UpdateCharacterAnimation(myRB.velocity.sqrMagnitude, myRB.velocity.y > 0);
                yield return null;
                dir = myPath.nodes[currNextNodeIndex].transform.position - transform.position;
            }
            //Debug.Log("Reached Node!");
            myRB.velocity = Vector2.zero;
            // if it is not the last one in the path (that is the exit
            if (currNextNodeIndex < myPath.nodes.Length - 1)
            {
               
                    
                timeToWait = NodeReached();
                yield return new WaitForSeconds(timeToWait+Random.Range(-0.5f, 0.5f));
                if(isInfected)
                    myAnim.TurnBackNormal();
                currNextNodeIndex++;
            }
            else
            {
                //Call the AI directon to tell that we reached the exit
                myDir.RestartClient(indexForDirector);
                isMoving = false;
            }
        }
    }
}

public enum ClientState
{
    LOOKINFORITEM,
    GOINTOPAY
}
