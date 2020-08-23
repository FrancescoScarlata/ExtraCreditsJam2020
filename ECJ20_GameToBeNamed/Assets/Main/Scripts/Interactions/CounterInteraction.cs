using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the iteraction this will be used by the player to go finish the supermarket phase.
/// </summary>
public class CounterInteraction : _Interactions
{
    protected bool isInteractible = true;
    protected float lastTImeInteracted=0;
    /// <summary>
    /// Method called to finish the phase
    /// </summary>
    public override void Interact()
    {
        Debug.Log("End phase!");
        if (lastTImeInteracted+2<Time.time)
        {
            lastTImeInteracted = Time.time;
            // this will stop the phase, make a fade and then call the next phase [to do]
            GameManager.instance.NextPhase();
        }
       
    }



}
