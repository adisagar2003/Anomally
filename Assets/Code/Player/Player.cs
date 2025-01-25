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
        Dash
    }

    public PlayerState currentState;

    //private stuff goes here
    private PlayerMovement playerMovement;
    private Rigidbody2D rb2D;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
        Debug.Log("Start of dash");
        yield return new WaitForSeconds(playerMovement.dashCooldown);
        Debug.Log("Cooldown done should change to idle");
        MovePlayer(0);
        currentState = PlayerState.Idle;
        rb2D.gravityScale = playerMovement.gravity;
    }
}
