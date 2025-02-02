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

    //private stuff goes here
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private Rigidbody2D rb2D;
    private PlayerInputHandler playerInputHandler;
    // event: player got hurt
    public delegate void DamageDelegate(float damage);
    public static event DamageDelegate DamageEvent;

    // event: Player is dead
    public delegate void DeathDelegate();
    public static event DeathDelegate DeathEvent;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        currentState = PlayerState.Idle;
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        StateManagementPhysics();
    }

    private void StateManagementPhysics()
    {
        if (playerMovement.isOnGround)
        {
            if (rb2D.velocity != Vector2.zero && currentState != PlayerState.Dash) currentState = Player.PlayerState.Run;
            if (rb2D.velocity == Vector2.zero && currentState != PlayerState.Dash) currentState = Player.PlayerState.Idle;
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
            currentState = PlayerState.Dash;
            playerMovement.Dash();
            StartCoroutine(DashCooldown());
        }
    }

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
        // start attack only if is in ground and running or idle
        if (currentState == PlayerState.Idle || currentState == PlayerState.Run)
        {
            currentState = PlayerState.Attack;
            // player movement: slight upward push 
            playerMovement.MoveForwardByAttack();
            // player combat: Implement collisionspheres.
            playerCombat.Attack();
        }
    }

    public void TakeDamage()
    {
        currentState = PlayerState.Hurt;
        DamageEvent(10.0f);
        if (playerCombat.health < 0.0f)
        {
            Death();
        }
    }

    public void Death()
    {
        // disable all input
        // send a signal that player is dead 
        playerInputHandler.OnDisable();
        playerInputHandler = null;
        Debug.Log("Player should die");
        DeathEvent();
    }
}
