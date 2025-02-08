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
    [SerializeField] private float dashSpeed = 20.0f;
    // minimum distance to stop dashing at from the player
    [SerializeField] private float targetReachedMinDistance = 0.2f;
    private Vector3 targetPosition;
    bool isMovingToPosition = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindAnyObjectByType<Player>();
    }
    public void DashTowardsPlayer(Vector3 targetPosition)
    {
        isMovingToPosition = true;
        // set the local private variable to the passed param
        this.targetPosition = targetPosition;
    }

    public void ChasePlayer()
    {

    }

    private void FixedUpdate()
    {
        if (Math.Abs(targetPosition.x - transform.position.x) > this.targetReachedMinDistance && isMovingToPosition)
        {
            Debug.Log(Math.Abs(targetPosition.x - transform.position.x));
            Debug.Log("Moving Towards the position");
            // only changing the x value 
            Vector3 newPosition = new Vector3(
                Mathf.Lerp(transform.position.x, targetPosition.x, Time.deltaTime * dashSpeed)
                ,transform.position.y
                ,transform.position.z);

            rb2D.MovePosition(newPosition);
            if (Vector3.Distance(targetPosition, transform.position) < 0.1)
            {
                Debug.Log("```Reached Destination`````");
                isMovingToPosition = false;

            }
        }
    }
}
