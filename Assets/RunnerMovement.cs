using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Player player;
    private Runner runner;

    private float initialYValue;
    [SerializeField] private float runnerSpeed = 14.0f;
    [SerializeField] private float runnerChargeSpeed = 14.0f;
    [SerializeField] private Transform debugChargeLocation;
    private float knockBackCooldown = 1.0f;
    private Vector2? newLocationToGo = null;
    public bool flipped = false;
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
        // eliminate y value
        Vector2 directionTowardsPlayer = (player.transform.position - transform.position).normalized;
        // Set only the X velocity while keeping the Y velocity unchanged
        rb2D.velocity = new Vector2(directionTowardsPlayer.x * runner.speed, rb2D.velocity.y);
    }

    public float GetRunnerSpeed()
    {
        return this.runnerSpeed;
    }

    public void StopRunner()
    {
        rb2D.velocity = Vector2.zero;
    }
    private void HandleRunnerFlip()
    {
        if (rb2D == null) return;
        if (rb2D.velocity.x < 0)
        {
            flipped = false;
            runner.transform.localScale = new Vector3(Mathf.Abs(runner.transform.localScale.x) * -1, runner.transform.localScale.y, runner.transform.localScale.z);
        }
        else if (rb2D.velocity.x > 0)
        {
            flipped = true;
            runner.transform.localScale = new Vector3(Mathf.Abs(runner.transform.localScale.x), runner.transform.localScale.y, runner.transform.localScale.z);
        }
    }

    
    // Charging player logic

    public void ChargeTo(Vector2? chargeLocation = null )
    {
        
        if (chargeLocation == null) return;
        newLocationToGo = chargeLocation;
        Vector2 chargeDirection = ((Vector2)(chargeLocation - transform.position)).normalized;
        
        rb2D.velocity = new Vector2(chargeDirection.x * runnerChargeSpeed, rb2D.velocity.y);
    }

    // For Debug 
    [ContextMenu("Charge to Debug")]
    public void ChangeToDebugLocation()
    {
        Vector2 velocity = rb2D.velocity;
        Vector2 chargeDirection = ((Vector2)(debugChargeLocation.position - transform.position)).normalized;
        rb2D.velocity = new Vector2(chargeDirection.x * runnerChargeSpeed, rb2D.velocity.y);
    }

    private void Update()
    {

    }

    public void KnockBack(float direction)
    {
        rb2D.velocity = direction * new Vector2(1,0);
        StartKnockBackCoroutine();
    }

    public IEnumerator StartKnockBackCoroutine()
    {
        yield return new WaitForSeconds(knockBackCooldown);
        rb2D.velocity = Vector2.zero;
    }

    public float GetKnockBackCooldown()
    {
        return knockBackCooldown;
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

        HandleRunnerFlip();

    }

}
