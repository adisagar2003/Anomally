using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Controls")]
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpForce = 10.0f;

    public Player player;

    // private fields
    private Rigidbody2D rb2D;
    [Header("Jumping")]
    [SerializeField] private float gravityWhileFalling = 56.0f;
    [SerializeField] private float gravity = 24.0f;
    
    public bool isOnGround { get; private set; }
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
            Vector2 horizontalVelocity = new Vector2(xInput * playerSpeed, 0) + new Vector2(0,rb2D.velocity.y);
            rb2D.velocity = horizontalVelocity;
        }
    }

    public void Jump()
    {
        if (isOnGround)
        {
            Debug.Log("Jump!");
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
}
