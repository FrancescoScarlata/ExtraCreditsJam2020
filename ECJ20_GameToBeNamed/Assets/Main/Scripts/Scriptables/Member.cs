using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for all the items for a member of the family
/// </summary>
[CreateAssetMenu(fileName = "FamilyMember", menuName = "Family/Member", order = 1)]
public class Member : ScriptableObject
{
    // 3 states of a member
    public Sprite memberOk;
    public Sprite memberInfected;
    public Sprite memberDead;

    public FamilyID myID;
    public MemberState myState = MemberState.normal;

    public Item [] itemsICanAskFor; 

    List<Item> itemsAskedThisRound;
    List<Item> itemsAskedAndNotBought;

    
    /// <summary>
    /// Method called to reset all the values when a run starts
    /// </summary>
    public void NewGame()
    {
        myState = MemberState.normal;
        // resets the state
        itemsAskedThisRound.Clear();
        // resets the lists of items
        itemsAskedAndNotBought.Clear();
    }

    /// <summary>
    /// Method called to ask the items at every phase.
    /// </summary>
    /// <param name="numberOfItems">number of items for this round. (so it can be decided level by level</param>
    public void AskItems(int numberOfItems)
    {
        // this will populate the asked this round list
        for(int i=0; i<numberOfItems; i++)
        {
            // take an item from the list of items available to this member
            itemsAskedThisRound.Add(itemsICanAskFor[Random.Range(0, itemsICanAskFor.Length)]);
        }
    }

    /// <summary>
    /// Method called at the phase 3. the member will check how many item asked are bought and other stuff
    /// </summary>
    public void CheckItemsBought( /* List of items bought with this member id */ Item[] itemsBought)
    {
        // checks the item bought.
        foreach(Item item in itemsBought)
        {
            // removed the item from the lists, with priority on the list of the day
            RemoveBoughtItemFromList(item);
        }

        //send the items not bought in the itemAskedAndNotBought list
        foreach(Item item in itemsAskedThisRound)
        {
            itemsAskedAndNotBought.Add(item);
        }
        itemsAskedThisRound.Clear();
    }

    /// <summary>
    /// From the list of items bought, one by one will be checked if that item is in those lists and will remove from them
    /// </summary>
    /// <param name="itemBought"></param>
    protected void RemoveBoughtItemFromList(Item itemBought)
    {
        Item tmp = itemsAskedThisRound.Find(x => x.myType == itemBought.myType);
        if(tmp!= null) // the item is here
        {
            itemsAskedThisRound.Remove(tmp);
            return;
        }
        else
        {
            tmp = itemsAskedAndNotBought.Find(x => x.myType == itemBought.myType);
            if (tmp != null) // the item is here
            {
                itemsAskedAndNotBought.Remove(tmp);
                return;
            }
        }
        Debug.LogWarning($"Error! The item {itemBought.myType} wasn't in neither of the 2 lists.");
    }

}

// enum to have differnet ids for the family members
public enum FamilyID
{
    mother,
    father,
    wife,
    son,
    daughter
}

// enum to have the possible states of a member
public enum MemberState
{
    normal,
    infected,
    dead
}
