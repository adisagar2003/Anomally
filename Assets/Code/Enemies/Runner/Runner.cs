/*
 * 
 *  Runs towards player 
 *  When reaching a certain distance, charges towards player
 *  If player hit, player knocks back and runner waits before charging again
 *  If player missed, wait for a time
 *  And chase player again if not in trigger area, else charge again
 *  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Runner : BaseEnemy
{
    public enum RunnerState
    {
        Idle,
        Chase,
        Charge,
        Attacking,
        Hurt
    }

    [SerializeField] public RunnerState currentState { get; private set; } = RunnerState.Idle;

    public float speed = 4.0f;
    private RunnerMovement runnerMovement;
    // private stuff
    private Player player;
    private bool isInDetectableArea;

    [SerializeField] private float spawnTime;
    // Debug 
    [SerializeField] private string debugString;
    public bool flipped;
    private void Start()
    {
        this.player = FindFirstObjectByType<Player>();
        runnerMovement = GetComponent<RunnerMovement>();
        StartCoroutine(SpawnStart());
    }


    
    public void SetIsDetectableArea(bool isDetectable)
    {
        this.isInDetectableArea = isDetectable;
    }
    private void Update()
    {
        flipped = runnerMovement.flipped;
        debugString = $"Health: {health}\n" +
            $"Speed: {speed}\n" +
            $"In Detectable Area: {isInDetectableArea}\n" +
            $"Current State: {currentState}\n" +
            $"\t`  ```Runner Movement```\t \n" +
            $"";
    }

    private IEnumerator SpawnStart()
    {
        yield return new WaitForSeconds(1.4f);
        // Now runner will chase the player
        if (isInDetectableArea)
        {
            Attack();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        currentState = RunnerState.Chase;
       
    }
    public void Attack(Vector2 locationOfCollision)
    {
        currentState = RunnerState.Charge;
        // Charge player.
        runnerMovement.ChargeTo(locationOfCollision);
        // new logic: If player is not found after hitting, then chase or attack again
        StartCoroutine(AfterAttack());
        runnerMovement.StopRunner();
       
    }

    public IEnumerator AfterAttack()
    {

        yield return new WaitForSeconds(1.5f);
        if (isInDetectableArea) AttackAgainTowardsPlayer();
        else ChasePlayer();

    }

    private void AttackAgainTowardsPlayer()
    {
        if (isInDetectableArea)
        {
            if (player != null)
            {
                Attack(player.transform.position);
            }
        }
    }
    public void GiveDamageToPlayer(Vector2 directionOfHurt)
    {
        player.TakeDamage(directionOfHurt, flipped);
        currentState = RunnerState.Attacking;
        StartCoroutine(AfterAttack());
    }

    public void SetState(RunnerState state)
    {
        currentState = state;
    }

    public override void Damage(float damageAmount)
    {

    }

    public override void DisableAllAttacks()
    {
    }

    public override void Attack()
    {
        Debug.Log("Future implementation");
    }
}
