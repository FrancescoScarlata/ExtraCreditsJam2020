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
    }

}
