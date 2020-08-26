using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

/// <summary>
/// Class responsible for the AI elements inside
/// </summary>
public class AIDirector : MonoBehaviour
{
    

    public ClientController [] clientControllers;
    public PathManager myPathManager;

    public Transform entranceTransform;

    public Transform VertexForInitialSpawning;

    public Vector2 edgesOfSpawn;

    protected DayInfo todayInfos;

    protected int[] valuesForItemPick = new int[12];
    protected int sumOfValues;


    public void InitializeDirector(DayInfo newInfos)
    {
        todayInfos = newInfos;

        HideClients();

        // get the number of clients needed
        for(int i=0; i<todayInfos.amountOfPeople; i++) // setup the infected element
        {
            // check the infected based on the number of infected inside
            if (i < todayInfos.amountOfSickPeople)
                clientControllers[i].isInfected = true;
            else
                clientControllers[i].isInfected = false;
        }

        //get the probailities based on the promos
        CalcultateTheOddQuantities();
        StartCoroutine(SpawnClients());


    }

    IEnumerator SpawnClients()
    {

        // initialize those clients
        for (int i = 0; i < todayInfos.amountOfPeople; i++) // setup the infected element
        {
            // maybe i can do this part in an IEnumerator
            Vector2 startingPosition = (Vector2)VertexForInitialSpawning.position + new Vector2(Random.Range(0, edgesOfSpawn.x), Random.Range(0, edgesOfSpawn.y));
            clientControllers[i].Reposition(startingPosition);
            clientControllers[i].StartMoving(myPathManager.GetThePath(DecideType()), i, this);
            yield return new WaitForSeconds(0.7f);
        }
       
    }


    /// <summary>
    /// Method called by the client controller to get back in the game
    /// </summary>
    /// <param name="clientIndex"></param>
    public void RestartClient(int clientIndex)
    {
        clientControllers[clientIndex].StopMoving();
        clientControllers[clientIndex].Reposition(entranceTransform.position);
        clientControllers[clientIndex].ResetClient();
        // decide the item
        clientControllers[clientIndex].StartMoving(myPathManager.GetThePath(DecideType()), clientIndex, this);
    }

    /// <summary>
    /// Method called to close the clients
    /// </summary>
    public void HideClients()
    {
        for (int i = 0; i < clientControllers.Length; i++)
        {
            clientControllers[i].StopMoving();
            clientControllers[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// It will also count the item in promos with triple chance.
    /// This will update the values when the new infos is given, so it can be used after that when the type needs to be decided
    /// </summary>
    protected void CalcultateTheOddQuantities()
    {
        sumOfValues = 0;
        for(int i= 0; i<valuesForItemPick.Length; i++)
        {
            if (todayInfos.sectionsWithPromos[i])
                valuesForItemPick[i] = 3;
            else
                valuesForItemPick[i] = 1;
            sumOfValues += valuesForItemPick[i];
        }
    }

    /// <summary>
    /// This method is called to get a random item from all the items in the store.
    /// It will also count the item in promos with triple chance
    /// </summary>
    /// <returns></returns>
    protected ItemType DecideType()
    {
        int randomValue = Random.Range(0, sumOfValues);
        int tmp = 0;
        for(int i=0; i<valuesForItemPick.Length; i++)
        {
            tmp += valuesForItemPick[i];
            if (randomValue < tmp)
            {
                return (ItemType)i; // this will return the Item type, because the have the same length and same order
            }
        }


        return ItemType.Medicine;
    }

}
