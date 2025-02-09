using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Player player;
    private Runner runner;

    private float initialYValue;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        rb2D = GetComponent<Rigidbody2D>();
        runner = GetComponent<Runner>();
        initialYValue = transform.position.y;
    }

    public void RunnerMovementChasePlayer()
    {
        if (player == null) return;
        Vector2 newPosition = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * runner.speed);
        // eliminate y value
        newPosition.y = initialYValue; 
        rb2D.MovePosition(newPosition);
    }

    private void FixedUpdate()
    {
        if (runner.currentState == Runner.RunnerState.Chase)
        {
            RunnerMovementChasePlayer();
        }
    }

}
