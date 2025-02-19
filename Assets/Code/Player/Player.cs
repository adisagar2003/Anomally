using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Jump, 
        Run,
        Dash,
        Attack,
        Hurt,
        Death
    }

    public PlayerState currentState;

    #region Private Items
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private Rigidbody2D rb2D;
    private PlayerInputHandler playerInputHandler;
    private PlayerAnimHandle playerAnimHandle;
    #endregion
    public float hurtCooldown { get; private set; }  = 0.2f;
    public float attackCooldown { get; private set; }
    // event: player got hurt
    public delegate void DamageDelegate(float damage);
    public static event DamageDelegate PlayerDamageEvent;
        
    // event: Player is dead
    public delegate void DeathDelegate();
    public static event DeathDelegate DeathEvent;

    // For debugging purpodses
    [SerializeField] private string debugData = "";
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerAnimHandle = GetComponent<PlayerAnimHandle>();
        currentState = PlayerState.Idle;
        rb2D = GetComponent<Rigidbody2D>();
        attackCooldown = playerCombat.GetAttackCooldown();
    }

    private void FixedUpdate()
    {
        StateManagementPhysics();
    }


    public float GetFacingDirection()
    {
        return playerMovement.facingDirection;
    }

    private void Update()
    {
        debugData = $"Hello," +
            $" {playerCombat.health} \n Speed: {rb2D.velocity.ToString()}" +
            $" \n currentState: {currentState.ToString()} " +
            $"\n canDash: {playerMovement.canDash}" +
            $"\n isOnGround: {playerMovement.isOnGround}";

    }



    private void StateManagementPhysics()
    {
        if (playerMovement.isOnGround)
        {
            // shift  state to run
            if ((rb2D.velocity.sqrMagnitude > 0.001f)
                && currentState != PlayerState.Dash
                && currentState != PlayerState.Attack
                && currentState != PlayerState.Hurt
                ) {
                currentState = Player.PlayerState.Run;
                }
            // shift state to idle
            else if ((rb2D.velocity.sqrMagnitude < 0.001f) && currentState != PlayerState.Attack)
            {
                currentState = PlayerState.Idle;
            }
        }
        else if (!playerMovement.isOnGround)
        {
            currentState = Player.PlayerState.Jump;
        }
    }

    public void MovePlayer(float xInput)
    {
        if (currentState == PlayerState.Idle || currentState == PlayerState.Run)  playerMovement.HandleMovement(xInput);
    }

    public void Jump()
    {
        if (currentState == PlayerState.Idle || currentState == PlayerState.Run)
        {
            currentState = PlayerState.Jump;
            playerMovement.Jump();
        }
        
    }

    public void Dash()
    {
        if (currentState == PlayerState.Idle || currentState == PlayerState.Run)
        {
            playerMovement.Dash();
            StartCoroutine(DashCooldown());
        }
    }

    // Future migration: to PlayerMovement.cs
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(playerMovement.dashCooldown);
        MovePlayer(0);
        currentState = PlayerState.Idle;
        rb2D.gravityScale = playerMovement.gravity;
    }


    // Combat 
    public void Attack()
    {
        GroundAttack();
        AirAttack();
    }

    private void GroundAttack()
    {
        if (currentState == PlayerState.Attack || currentState == PlayerState.Jump) return;
        playerMovement.StopMovement();
        playerInputHandler.DisableInput();
        currentState = PlayerState.Attack;
        playerCombat.Attack();
        StartCoroutine(AttackCoroutine());
    }

    private void AirAttack()
    {
        if (currentState == PlayerState.Attack) return;
        playerInputHandler.DisableInput();
        currentState = PlayerState.Attack;
        playerCombat.Attack();
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
     
        yield return new WaitForSeconds(attackCooldown);
        currentState = Player.PlayerState.Idle;
        playerInputHandler.EnableInput();

    }

    public void TakeDamage()
    {
        if (currentState == PlayerState.Hurt) return;
        currentState = PlayerState.Hurt;
        PlayerDamageEvent(10.0f);
        if (playerCombat.health < 0.0f)
        {
            Death();
        }
        StartCoroutine(HurtCooldown());
    }

    public void TakeDamage(float damage)
    {
        if (currentState == PlayerState.Hurt) return;
        currentState = PlayerState.Hurt;
        PlayerDamageEvent(damage);
        if (playerCombat.health < 0.0f)
        {
            Death();
        }
        StartCoroutine(HurtCooldown());
    }

    public void TakeDamage(Vector2 hurtDirection, bool flipped,float amount = 3.0f)
    {
        if (currentState == PlayerState.Hurt) return;
        Debug.Log("Player took damage at" + hurtDirection.ToString());
        currentState = PlayerState.Hurt;
        PlayerDamageEvent(amount);
        if (playerCombat.health < 0.0f)
        {
            Death();
        }

        // knock player back towards hurt Direction
        playerMovement.KnockBack(new Vector2(hurtDirection.x, 0), flipped);
        StartCoroutine(HurtCooldown());
    }

    [ContextMenu("Take Damage Left")]
    public void TakeDamageLeft()
    {
        if (currentState == PlayerState.Hurt) return;
        currentState = PlayerState.Hurt;
        PlayerDamageEvent(10.0f);
        if (playerCombat.health < 0.0f)
        {
            Death();
        }

        // knock player back towards hurt Direction
        playerMovement.KnockBack(Vector2.left,false);
        StartCoroutine(HurtCooldown());
    }

    private IEnumerator HurtCooldown()
    {
        yield return new WaitForSeconds(hurtCooldown);
        currentState = PlayerState.Idle;
    }
    public void Death()
    {
        // disable all input
        // send a signal that player is dead
        if (playerInputHandler != null) playerInputHandler.OnDisable();
        playerInputHandler = null;
        DeathEvent();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
