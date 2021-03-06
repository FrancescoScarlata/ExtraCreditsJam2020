﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is an abstract class for the interactions elements.
/// This parent has a public Method that is called by the player when interacting
/// </summary>
public abstract class _Interactions : MonoBehaviour {


    // it will be just the button after
    public string messageToSayWhenInteracting;

    public GameObject hintButtonInteraction;
    public SimpleFMODAudioSource sfxOnInteraction;

    protected Collider2D coll;

    protected virtual void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Action function that is called by the player for the interaction.
    /// </summary>
    public abstract void Interact();


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Add this element to the player controller so that it can be called
        if (collision.tag == "Player")
        {
            PlayerController.instance.InteractionEnter(this);
            if (hintButtonInteraction)
            {
                hintButtonInteraction.SetActive(true);
            }
        }
        
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Delete this element from the player controller so that it can't be called outside
            PlayerController.instance.InteractionExit(this);
            if (hintButtonInteraction)
            {
                hintButtonInteraction.SetActive(false);
            }
        }
    }

}
