using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Player player;
    private Runner runner;

    private float initialYValue;
    [SerializeField] private float runnerSpeed = 20.0f;
    [SerializeField] private Transform debugChargeLocation;
    private Vector2? newLocationToGo = null;
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

    
    // Charging player logic

    public void ChargeTo(Vector2? chargeLocation = null )
    {
        
        if (chargeLocation == null) return;
        newLocationToGo = chargeLocation;
        Vector2 velocity = rb2D.velocity;
        Vector2 newPosition = Vector2.SmoothDamp(transform.position, (Vector2) chargeLocation,ref velocity,Time.deltaTime * runner.speed,runnerSpeed);
        // eliminate y value
        newPosition.y = initialYValue;
        rb2D.MovePosition(newPosition);
    }

    // For Debug 
    [ContextMenu("Charge to Debug")]
    public void ChangeToDebugLocation()
    {
        Vector2 velocity = rb2D.velocity;
        Vector2 newPosition = Vector2.SmoothDamp(transform.position, debugChargeLocation.position, ref velocity, Time.deltaTime * runner.speed, runnerSpeed);
        rb2D.MovePosition(newPosition);
    }

    private void FixedUpdate()
    {
        if (runner.currentState == Runner.RunnerState.Chase)
        {
            RunnerMovementChasePlayer();
        }

        if (runner.currentState == Runner.RunnerState.Charge)
        {
            ChargeTo(newLocationToGo);
        }
    }

}
