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

    [Header("DEBUG")]
    // these are for debugging purposes only
    [SerializeField] private float raycastDistance;
    [SerializeField] private GameObject raycastOriginPosition;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMovement(float xInput)
    {
        if (rb2D != null)
        {
            Vector2 horizontalVelocity = new Vector2(xInput * playerSpeed, 0);
            rb2D.velocity = horizontalVelocity;

        }
    }

    public void Jump()
    {

        // impulsive -y velocity;

        if (Physics2D.Raycast(transform.position, Vector2.down, raycastDistance))
        {
            Debug.Log("Player should jump");
            rb2D.AddForce(Vector2.up * jumpVelocity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(raycastOriginPosition.transform.position, transform.TransformDirection(Vector2.down)*raycastDistance, Color.yellow);
    }
}
