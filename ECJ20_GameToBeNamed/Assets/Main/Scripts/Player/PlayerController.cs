using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages Player behaviours, mainly movement. Virtually everything related to the display (sprites et al.) is presently 
/// for testing purposes and will most certainly be changed.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(0f, 20f)]
    float moveSpeed = 5f;

    SpriteRenderer sprRend;
    Rigidbody2D rb;
    Vector2 movement;

    protected _Interactions interactible;

    protected List<_Interactions> interactiblesObjects = new List<_Interactions>(); 

    public static PlayerController instance;

    public CharAnimationController myAnimController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            Debug.Log("Instance created.");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprRend = GetComponentInChildren<SpriteRenderer>();
    }

    // Receive inputs
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical"));
        if(myAnimController)
            myAnimController.UpdateCharacterAnimation(movement.sqrMagnitude, movement.y > 0);
       // if(movement.x < 0)
       // {
       //     sprRend.flipX = true;
       //}
       //else if(movement.x > 0)
       //{
       //     sprRend.flipX = false;
       // }

        if (Input.GetButtonDown("Interact") && interactible != null)
        {
            interactible.Interact();
        }
    }

    // Execute movement
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Could instead do rb.velocity for a bit of a ramp-up to speed, not sure which feels better?
    }

    public void InteractionEnter(_Interactions inter)
    {
        interactiblesObjects.Add(inter);

        //Debug.Log("Entered collision area " + inter.name);
        UpdateTheInteractibles();
    }

    public void InteractionExit(_Interactions inter)
    {
        interactiblesObjects.Remove(inter);
        //Debug.Log("Exited collision area " + inter.name);
        UpdateTheInteractibles();
    }


    /// <summary>
    /// Method called a new interactible item is added or removed
    /// </summary>
    protected void UpdateTheInteractibles()
    {
        if (interactiblesObjects.Count > 0)
        {
            interactible = interactiblesObjects[interactiblesObjects.Count - 1];
        }
        else
            interactible = null;
       
    }

}
