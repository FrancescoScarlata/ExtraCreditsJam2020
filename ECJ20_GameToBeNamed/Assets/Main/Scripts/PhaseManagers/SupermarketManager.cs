using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will handle the removal of the wrong elements instead of having to look for a single section
/// </summary>
public class SupermarketManager : MonoBehaviour
{
    // these should be ordered by the enum items so we can use that directly instead of having to look for the correct one
    public SectionManager[] sections; 


    /// <summary>
    /// Method called when a wrong item is taken out from the wrong list inside the supermarketList
    /// </summary>
    /// <param name="type"></param>
    public void TakeBackItem(ItemType type)
    {
        sections[(int)type].PlaceBackItem();
    }

    /// <summary>
    /// Method called from the Phase 2 script to restock the items requested
    /// </summary>
    /// <param name="restocks"></param>
    public void RestockTheSections(bool[] restocks)
    {
        for(int i=0; i<sections.Length; i++)
        {
            if (restocks[i])
                sections[i].Restock();
        }
    }

    /// <summary>
    /// Method called by the phase 2 manager to have set some sections in promos
    /// </summary>
    /// <param name="promos"></param>
    public void SetSectionsPromos(bool[] promos)
    {
        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].isInPromo = promos[i];
        }
    }

}
