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

    public static PlayerController instance;

    private void Awake()
    {
        if(instance = null)
        {
            instance = this;
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

        if(movement.x < 0)
        {
            sprRend.flipX = true;
        }
        else if(movement.x > 0)
        {
            sprRend.flipX = false;
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

    }

    public void InteractionExit(_Interactions inter)
    {

    }
}
