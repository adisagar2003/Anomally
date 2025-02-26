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

    public float speed;
    private RunnerMovement? runnerMovement;
    // private stuff
    private Player player;
    private bool isInDetectableArea;

    [SerializeField] private float spawnTime;
    [SerializeField] private float attackCooldown = 1.2f;
    // Debug 
    [SerializeField] private string debugString;
    public bool flipped;    
    private void Start()
    {
        if (this == null) return;
        this.player = FindFirstObjectByType<Player>();
        runnerMovement = GetComponent<RunnerMovement>();
        StartCoroutine(SpawnStart());
        speed = runnerMovement.GetRunnerSpeed();
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    public void SetIsDetectableArea(bool isDetectable)
    {
        this.isInDetectableArea = isDetectable;
    }
    private void Update()
    {
        if (this == null) return;
        flipped = runnerMovement.flipped;
        debugString = $"Health: {health}\n" +
            $"Speed: {runnerMovement.GetRunnerSpeed()}\n" +
            $"In Detectable Area: {isInDetectableArea}\n" +
            $"Current State: {currentState}\n" +
            $"\t`  ```Runner Movement```\t \n" +
            $"";
    }

    private IEnumerator SpawnStart()
    {
        yield return new WaitForSeconds(attackCooldown);
        // Now runner will chase the player
        if (isInDetectableArea)
        {
        
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
        if (this == null) return; // Prevents the error

        if (runnerMovement == null) return; // Object has been destroyed, stop execution


        currentState = RunnerState.Charge;
        // Charge player.
        runnerMovement.ChargeTo(locationOfCollision);
        // new logic: If player is not found after hitting, then chase or attack again

        StartCoroutine(AfterAttack());
    
       
    }

    public IEnumerator AfterAttack()
    {

        yield return new WaitForSeconds(attackCooldown);
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
        player.TakeDamage(directionOfHurt);
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


    public void GetHurtByPlayer(float facingDirection)
    {
        currentState = RunnerState.Hurt;
        health -= 4.0f;
        if (health < 0.0f) Death();
        runnerMovement.KnockBack(facingDirection);
        StartCoroutine(HurtCoroutine());
    }

    public override void Death()

    {
        // Disable all scripts on this GameObject
        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }
        base.Death();
    }
    public IEnumerator HurtCoroutine()
    {
        yield return new WaitForSeconds(runnerMovement.GetKnockBackCooldown());
        currentState = RunnerState.Idle;    
    }
    public override void DisableAllAttacks()
    {
    }

    protected override void WaitForPlayerToRecover(float amt)
    {
        throw new System.NotImplementedException();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
