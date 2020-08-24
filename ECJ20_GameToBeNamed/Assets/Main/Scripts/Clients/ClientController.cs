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
    public float distanceToReachANode = 1f;

    [Header("Components to setup")]
    public CharAnimationController myAnim;
    public Rigidbody2D myRB;

    protected bool itemPickedUp=false;

    protected PathToFollow myPath;
    protected int currNextNodeIndex;
    protected bool isMoving;

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
        transform.position = newPosition;
    }


    public void StopMoving()
    {
        isMoving = false;
        myRB.velocity = Vector2.zero;
    }

    /// <summary>
    /// Called by the AI direction when it needs the player back in the game after it was out of the game
    /// </summary>
    /// <param name="myNewPath"></param>
    public void StartMoving(PathToFollow myNewPath)
    {
        myPath = myNewPath;
        currNextNodeIndex = 0;
        isMoving = true;
        StartCoroutine(ClientBehaviour());
    }



    /// <summary>
    /// Method cal
    /// </summary>
    /// <param name="section"></param>
    protected void PickUpItem(SectionManager section)
    {
        Debug.Log("ItemPicked");
        //section.TakeItem();
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
        while (isMoving && currNextNodeIndex < myPath.nodes.Length)
        {
            dir = myPath.nodes[currNextNodeIndex].transform.position - transform.position;
            // reach the current node
            while (dir.magnitude > distanceToReachANode)
            {
                myRB.velocity = dir.normalized * movSpeed * Time.deltaTime;
                yield return null;
            }

            myRB.velocity = Vector2.zero;
            // if it is not the last one in the path (that is the exit
            if (currNextNodeIndex < myPath.nodes.Length - 1)
            {
                // do a wait if asked
                timeToWait = NodeReached();
                yield return new WaitForSeconds(timeToWait);
                currNextNodeIndex++;
            }
            else
            {
                //Call the AI directon to tell that we reached the exit
            }
        }
    }


}

public enum ClientState
{
    LOOKINFORITEM,
    GOINTOPAY
}
