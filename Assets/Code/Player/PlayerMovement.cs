using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Controls")]
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpVelocity = 10.0f;

    public Player player;

    // private fields
    private Rigidbody2D rb2D;
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
            Vector2 horizontalVelocity = new Vector2(xInput * playerSpeed, 0);
            rb2D.velocity = horizontalVelocity;

        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(raycastOriginPosition.transform.position, -transform.up * raycastDistance, Color.red);    
    }

    public void Jump()
    {
      
    }

    private void OnDrawGizmosSelected()
    {
    }
}
