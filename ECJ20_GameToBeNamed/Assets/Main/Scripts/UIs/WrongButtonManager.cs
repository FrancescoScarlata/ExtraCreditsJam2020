using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that is responsible for the wrong button behavious
/// </summary>
public class WrongButtonManager : MonoBehaviour
{
    public Image wrongIconImage;
    
    
    int myIndex; // index in the list of the wrong buttons  
    protected Item myItem;

    

    /// <summary>
    /// Method called by the Market list UI manager when a new button is setUp.
    /// Updates the Ico and the index it is in
    /// </summary>
    public void InititializeTheButton(Item newItem, int newIndex)
    {
        myItem = newItem;
        wrongIconImage.sprite = myItem.wrongItemIcon;
        wrongIconImage.enabled = true;
        UpdateMyIdex(newIndex);
    }

    /// <summary>
    /// This method is called when this button is removed from the list
    /// </summary>
    public void ResetItem()
    {
        wrongIconImage.enabled = false;
        wrongIconImage.sprite = null;
        myItem = null;        
    }


    /// <summary>
    /// This method is called by the Market List UIManager to update the correct index of the item inside the list of wrong buttons
    /// </summary>
    /// <param name="index"></param>
    public void UpdateMyIdex(int index)
    {
        myIndex = index;
    }


    /// <summary>
    /// Method called by the button clicked to say that the button ha being clicked
    /// </summary>
    public void RemoveItemFromList()
    {
        GameManager.instance.RemoveItem(myIndex);
    }


    /// <summary>
    /// Just for test purposes
    /// </summary>
    /// <returns></returns>
    public Item GetThisItem()
    {
        return myItem;
    }


}
