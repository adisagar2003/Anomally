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

    private bool canInitiateSecondAttack = false;
    #region Cooldown
    public float hurtCooldown { get; private set; } = 0.2f;
    public float attackCooldown { get; private set; }
    #endregion

    #region Events
    // event: player got hurt
    public delegate void DamageDelegate(float damage);
    public static event DamageDelegate PlayerDamageEvent;
        
    // event: Player is dead
    public delegate void DeathDelegate();
    public static event DeathDelegate DeathEvent;

    #endregion

    #region Debug
    [SerializeField] private string debugData = "";
    #endregion
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerAnimHandle = GetComponent<PlayerAnimHandle>();
        rb2D = GetComponent<Rigidbody2D>();
        currentState = PlayerState.Idle;
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
            $"\n isOnGround: {playerMovement.isOnGround}" +
            $"\n Check for player movement: {playerMovement}"+
            $"\n Check for player combat: {playerCombat}"+
            $"\n Check for player input handler: {playerInputHandler}"+
            $"\n isOnGround: {playerMovement.isOnGround}";

    }


    #region State Management
    private void StateManagementPhysics()
    {
        StateManagementLocomotion();
    }

    private void StateManagementLocomotion()
    {
        if (playerMovement.isOnGround)
        {
            if (IsIdle()) currentState = Player.PlayerState.Run;

            else SetIdle();
        }
        else if (!playerMovement.isOnGround)
        {
            currentState = Player.PlayerState.Jump;
        }
    }

    private void SetIdle()
    {
        if ((rb2D.velocity.sqrMagnitude < 0.001f) && currentState != PlayerState.Attack)
        {
            currentState = PlayerState.Idle;
        }
    }

    private bool IsIdle()
    {
        return ((rb2D.velocity.sqrMagnitude > 0.001f)
                && currentState != PlayerState.Dash
                && currentState != PlayerState.Attack
                && currentState != PlayerState.Hurt
                );
    }

    #endregion
    #region Player Movement
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
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(playerMovement.dashCooldown);
        MovePlayer(0);
        currentState = PlayerState.Idle;
        rb2D.gravityScale = playerMovement.gravity;
    }

    #endregion


    #region Combat
    public void Attack()
    {
        GroundAttack();
        AirAttack();
    }

    private void GroundAttack()
    {
        if (currentState == PlayerState.Attack || currentState == PlayerState.Jump) return;
        playerMovement.MoveForwardByAttack();
        currentState = PlayerState.Attack;
        playerCombat.Attack();
        StartCoroutine(AttackCoroutine());
        playerMovement.StopMovement();
    }

    private void AirAttack()
    {
        if (currentState == PlayerState.Attack) return;
        canInitiateSecondAttack = true;
        currentState = PlayerState.Attack;
        playerCombat.Attack();
        
        StartCoroutine(AttackCoroutine());
    }

    void InitiateSecondAttack()
    {
        Debug.Log("Initiated Second Attack");   
        playerAnimHandle.SetSecondAttackAnimTrigger();
    }


    private IEnumerator AttackCoroutine()
    {
        
        yield return new WaitForSeconds(attackCooldown);
        canInitiateSecondAttack = false;
        currentState = Player.PlayerState.Idle;
        playerInputHandler.EnableInput();

    }

    public void TakeDamage(Vector2 hurtDirection,float amount = 3.0f)
    {
        if (currentState == PlayerState.Hurt) return;
        currentState = PlayerState.Hurt;
        PlayerDamageEvent(amount);

        // knock player back towards hurt Direction
        playerMovement.KnockBack(new Vector2(hurtDirection.x, 0));
        StartCoroutine(HurtCooldown());

        if (playerCombat.health < 0.0f)
        {
            Death();
        }
    }

    private IEnumerator HurtCooldown()
    {
        yield return new WaitForSeconds(hurtCooldown);
        currentState = PlayerState.Idle;
    }
    #endregion

    #region Death Handle
    public void Death()
    {
        // disable all input
        // send a signal that player is dead
        rb2D.velocity = Vector2.zero;
        if (playerInputHandler != null) playerInputHandler.OnDisable();
        playerInputHandler = null;
        DisableAllScripts();
        playerAnimHandle.SetDeathTrigger();
    }

    public void InvokeDeathEvent()
    {
        DeathEvent();
    }

#endregion
    private void DisableAllScripts()
    {
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour sc in scripts)
        {
            sc.enabled = false;
        }
    }

    private void OnDisable()
    {

        StopAllCoroutines();
    }
}
