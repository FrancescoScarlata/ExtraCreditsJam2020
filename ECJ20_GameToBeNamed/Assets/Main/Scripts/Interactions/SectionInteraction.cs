using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that will handle the iteraction 
/// </summary>
public class SectionInteraction : _Interactions
{
    public SectionManager mySection;

    public override void Interact()
    {
        // call the game manager to add my section item in the supermarket list

        Debug.Log("Interaction is happening. Yay.");
        if (!mySection.IsEmpty)
        {
            GameManager.instance.InsertItem(mySection.myItem);
            Debug.Log("Interaction succeeded.");
        }
        else
        {
            Debug.Log("Interaction failed, the container is empty");
        }
       
    }

}
