using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;

    [Header("Movement Controls")]
    [SerializeField] private float playerSpeed = 10.0f;
    private float facingDirection = 1.0f;
    [SerializeField] private float dashIntensity = 10.0f;
    [SerializeField] private float dashLerpTime = 10.0f;
    public float dashCooldown = 1.4f;
    [SerializeField] private float maxSpeed = 60.0f;

    [Header("Jumping")]
    [SerializeField] private float gravityWhileFalling = 56.0f;
    public float gravity { get; private set; } = 24.0f;
    [SerializeField] private float jumpForce = 10.0f;
    public bool isOnGround { get; private set; }

    // private fields
    private Rigidbody2D rb2D;

    
    [Header("DEBUG")]
    // these are for debugging purposes only
    [SerializeField] private float raycastDistance;
    [SerializeField] private GameObject raycastOriginPosition;
    [SerializeField] private LayerMask layerMask;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        IsOnGround();
        HandleGravityScale();
    }

    public void IsOnGround()
    {
       if (Physics2D.Raycast(raycastOriginPosition.transform.position, -transform.up, raycastDistance,layerMask))
       {
            isOnGround = true;
       }

       else
        {
            isOnGround = false;
        }
    }
    public void HandleMovement(float xInput)
    {
        if (rb2D != null)
        {
            HandleSpriteFlip(xInput);
            Vector2 horizontalVelocity = new Vector2(xInput * playerSpeed, 0) + new Vector2(0, rb2D.velocity.y);
            rb2D.velocity = horizontalVelocity;
        }
    }

    /*
    ``````````````````MOVED TO Player.cs
    private void StateManagement()
    {
        if (isOnGround)
        {
            if (rb2D.velocity != Vector2.zero) player.currentState = Player.PlayerState.Run;
            if (rb2D.velocity == Vector2.zero) player.currentState = Player.PlayerState.Idle;
        }
        else if (!isOnGround)
        {
            player.currentState = Player.PlayerState.Jump;
        }
    }
    */

    public void Jump()
    {
        if (isOnGround)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void HandleSpriteFlip(float xInput)
    {
        if (xInput != 0)
        {
            facingDirection = xInput;
        }
    }
    // handles gravity scale while on air 
    void HandleGravityScale()
    {
        if (!isOnGround && rb2D.velocity.y <= 0 )
        {
            rb2D.gravityScale = gravityWhileFalling;
        }
        else if (!isOnGround && rb2D.velocity.y > 0)
        {
            rb2D.gravityScale = gravity;
        }
        if (isOnGround)
        {
            rb2D.gravityScale = gravity;
          
        }
    }

    public void Dash()
    {
        Debug.Log("Should be dashing");
        rb2D.gravityScale = 0;
        float initialY = transform.position.y;
        rb2D.velocity = new Vector2(dashIntensity * facingDirection, 0);
        Debug.Log(transform.position.y - initialY);
        if (Mathf.Abs(transform.position.y - initialY) > 10)
        {
            rb2D.velocity = Vector2.zero;
        }
    }

}
