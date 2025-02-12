using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;


    [Header("Movement Controls")]
    [SerializeField] private float playerSpeed = 10.0f;
    private float facingDirection = 1.0f;
    // Dashing
    [SerializeField] private float dashIntensity = 10.0f;
    [SerializeField] private float dashLerpTime = 10.0f;
    public float dashCooldown = 1.4f;
    [SerializeField] private float maxSpeed = 60.0f;
    public bool canDash { get; private set; } = true;
    [SerializeField] private float canDashAgainAfter = 2.2f;
    [Header("Jumping")]
    [SerializeField] private float gravityWhileFalling = 56.0f;
    public float gravity { get; private set; } = 24.0f;
    [SerializeField] private float jumpForce = 10.0f;
    public bool isOnGround { get; private set; }

    // private fields
    private Rigidbody2D rb2D;
    [SerializeField] private float forwardForce = 2.0f;
    
    [Header("DEBUG")]
    // these are for debugging purposes only
    [SerializeField] private float raycastDistance;
    [SerializeField] private GameObject raycastOriginPosition;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackTime = 2.0f;
    private bool isKnockingBack = false;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        IsOnGround();
        HandleGravityScale();
        KnockingBackHandle();
    }


    private void Update()
    {
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

        if (xInput == 1)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (xInput == -1)
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
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

   
    // when attacking, sligtly move forward;
    public void MoveForwardByAttack()
    {
        rb2D.AddForce(new Vector2(facingDirection * forwardForce, 0), ForceMode2D.Impulse);
    }

    public void Dash()
    {
        if (canDash == false) return;
        player.currentState = Player.PlayerState.Dash;  
        rb2D.gravityScale = 0;
        rb2D.velocity = new Vector2(dashIntensity * facingDirection, 0);
        canDash = false;
        StartCoroutine(DashCooldown());
    }

    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(canDashAgainAfter);
        canDash = true;
    }

    public void KnockBack(Vector2 direction, bool side)
    {

        StartCoroutine(StopTheVelocity());
        if (side == true)
        {
            rb2D.velocity = new Vector2(1*knockbackForce, 0);
        }

        else if (side == false)
        {
            rb2D.velocity = new Vector2(-1* knockbackForce, 0);
        }
        

    }

    // For debugging purposes
    [ContextMenu("KnockBack")]
    public void KnockBack()
    {
        StartCoroutine(StopTheVelocity());
    }

    public IEnumerator StopTheVelocity()
    {
        //float gravscale = rb2d.gravityscale;
        //rb2d.gravityscale = 0;
        //isknockingback = true;a
        yield return new WaitForSeconds(knockbackTime);
        Debug.Log("Back Again");
        rb2D.velocity = Vector2.zero;
        player.currentState = Player.PlayerState.Idle;
        //rb2D.velocity = Vector2.zero;
        //isKnockingBack = false;
        //rb2D.gravityScale = gravScale;
       
    }

    private void KnockingBackHandle()
    {
        if (isKnockingBack)
        {
            rb2D.velocity = (Vector2.left * knockbackForce);
        }
    }
}
