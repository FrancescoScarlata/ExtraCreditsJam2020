using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Scritpt that will handle the calls for the items in the list
/// </summary>
public class ItemIconWIthAmountManager : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI remainingText;
    public Item myItem;

    protected int remainingAmount;
    
    /// <summary>
    /// This will initialize this area, with the correct amounts
    /// </summary>
    /// <param name="remAmount"></param>
    public void InitializeMe(int remAmount)
    {
        icon.sprite = myItem.activeIcon;
        remainingAmount = remAmount;
        remainingText.text = "" + remainingAmount;
    }

    /// <summary>
    /// This method will be called when an item of this type is picked up.
    /// it will remove 1 from it
    /// </summary>
    public void ItemPickedUp()
    {
        remainingAmount-- ;
        remainingText.text = "" + remainingAmount;
    }


}
