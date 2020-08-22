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

}
