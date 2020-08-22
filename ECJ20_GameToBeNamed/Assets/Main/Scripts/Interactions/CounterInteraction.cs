using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the iteraction this will be used by the player to go finish the supermarket phase.
/// </summary>
public class CounterInteraction : _Interactions
{
    /// <summary>
    /// Method called to finish the phase
    /// </summary>
    public override void Interact()
    {
        Debug.Log("End phase!");

        // this will stop the phase, make a fade and then call the next phase [to do]
        //GameManager.instance.NextPhase();
    }


}
