using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherMovement : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb2D;
    private float direction = -1;
    [SerializeField] private float speed = 10;
    [SerializeField] private float recoilSpeed= 10;
    [SerializeField] private float dashSpeed;
    // minimum distance to stop dashing at from the player
    [SerializeField] private float targetReachedMinDistance = 0.2f;

    [Header("Combat")]
    [SerializeField] private float knockbackStrength = 24.0f;

    private Vector3 targetPosition;
    bool isMovingToPosition = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<Player>();
    }
    public void DashTowardsPlayer(Vector3 targetPosition)
    {
        isMovingToPosition = true;
        // set the local private variable to the passed param
        this.targetPosition = targetPosition;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isMovingToPosition = false;
    }
    public void ChasePlayer()
    {
        
        Vector2 newPosition = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x,transform.position.y), Time.deltaTime * speed);
       
        rb2D.MovePosition(newPosition);
    }

    private void FixedUpdate()
    {
        DashDasherTowardsObject();
    }

    
    private void DashDasherTowardsObject()
    {
        if (XDistanceTowardsTargetPosition() > this.targetReachedMinDistance && isMovingToPosition)
        {
            // only changing the x value 
            Vector3 newPosition = new Vector3(
                Mathf.Lerp(transform.position.x, targetPosition.x + .4f, Time.deltaTime * dashSpeed)
                , transform.position.y
                , transform.position.z);

            rb2D.MovePosition(newPosition);

            if (Mathf.Abs(Vector3.Distance(targetPosition, transform.position)) < 0.91f)
            {
                Debug.Log("```Reached Destination`````");
               
                isMovingToPosition = false;
                

            }
        }
    }

    [ContextMenu("Knockback")]
    public void KnockbackFromRight()
    {
        Knockback(Vector2.left); // Example: Apply knockback to the left
    }

    public void Knockback(Vector2 knockbackDirection)
    {
        rb2D.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Force);
    }



    
    private float XDistanceTowardsTargetPosition()
    {
        return Math.Abs(targetPosition.x - transform.position.x);
    }
}
