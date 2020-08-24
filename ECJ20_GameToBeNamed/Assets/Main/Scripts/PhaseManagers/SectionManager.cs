using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for the section element, the part with the infos when the sections items.
/// 
/// </summary>
public class SectionManager : MonoBehaviour
{

    public Item myItem;
    public int numOfItemRemaning;
    public int maxItems; // don't know if is usefull
    [Header("This should have the same amount as maxItem")]
    public GameObject[] spritesShown;
    public bool isInPromo = false; // don't know if this s still usefull
    public bool IsEmpty
    {
        get { return numOfItemRemaning <= 0; }
    }

    /// <summary>
    /// This method will be called from the phase 2 when the item is restockable.
    /// </summary>
    public void Restock()
    {
        numOfItemRemaning = maxItems;
        foreach(GameObject sprite in spritesShown)
        {
            //sprite.SetActive(true); TO ADD!
        }

    }

    /// <summary>
    /// Method called during the interaction from the player to take the item.
    /// It will return the item taken.
    /// </summary>
    public Item TakeItem()
    {
        if (!IsEmpty)
        {
            // calls the game manager supermarketList to add the item
            numOfItemRemaning--;
            if (spritesShown[numOfItemRemaning])
                spritesShown[numOfItemRemaning].SetActive(false);

            // some animation here on the item taken
            return myItem;
        }
        return null;
    }

    /// <summary>
    /// Method called by the Supermarket manager to get back the item.
    /// It will increase of 1 item the shop.
    /// </summary>
    public void PlaceBackItem()
    {
        if(spritesShown[numOfItemRemaning])
            spritesShown[numOfItemRemaning].SetActive(true); 
        numOfItemRemaning++;    
    }


}
