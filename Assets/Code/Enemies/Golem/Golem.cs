using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Golem : BaseEnemy
{

    private Player playerRef;

    public GolemStateMachine golemStateMachine;
    public GolemIdleState golemIdleState;
    public GolemWalkState golemWalkState;
    public GolemAttackState golemAttackState;
    public GolemHurtState golemHurtState;
    public GolemDeathState golemDeathState;

    [SerializeField] private string debugString = "";

    #region Movement
    private float speed = 10.0f;
    private Rigidbody2D rb;
    #endregion

    #region Player Detection
    public bool isInDetectableArea { get; private set; } = false;
    #endregion

    #region Combat
    [SerializeField] private float knockbackForce = 100.0f;
    #endregion
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        playerRef = FindFirstObjectByType<Player>();
    }


    private void LookAtPlayer()
    {
        float amt = Vectors.FindDirectionTowardsPlayer(playerRef, transform.position).x;

        if (amt > 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        if (amt < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Awake()
    {
        golemStateMachine = new GolemStateMachine();
        golemIdleState = new GolemIdleState(this, golemStateMachine);
        golemWalkState = new GolemWalkState(this, golemStateMachine);
        golemAttackState = new GolemAttackState(this, golemStateMachine);
        golemHurtState = new GolemHurtState(this, golemStateMachine);
        golemDeathState = new GolemDeathState(this, golemStateMachine);
        rb = GetComponent<Rigidbody2D>();
        // initialize state
        golemStateMachine.Initialize(golemIdleState);
    }

    // Helps preventing hurting more during Hurt state.
    public void DisableAllColliders()
    {
        Collider2D[] childColliders = this.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D child in childColliders)
        {
            if (child.tag == "HurtCollider" || child.tag == "Hitbox")
            {
                child.enabled = false;
            } 
        }
    }

    public void EnableAllColliders()
    {
        Collider2D[] childColliders = this.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D child in childColliders)
        {
            if (child.tag == "HurtCollider" || child.tag == "Hitbox")
            {
                child.enabled = true;
            }
        }
    }

    private void Update()
    {
        golemStateMachine.currentState.OnUpdate();
        debugString = $"Current State: {golemStateMachine.currentState.GetStateName()}" +
            "";
        LookAtPlayer();
    }

   

    private void FixedUpdate()
    {
        golemStateMachine.currentState.OnFixedUpdate();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Damage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }

    public override void DisableAllAttacks()
    {
        throw new System.NotImplementedException();
    }

    protected override void WaitForPlayerToRecover(float amt)
    {
        throw new System.NotImplementedException();
    }

    public void SetInDetectableArea(bool param)
    {
        isInDetectableArea = param;
        if (golemStateMachine.currentState is GolemIdleState)
        {
            golemStateMachine.ChangeState(golemWalkState);
        }
    }

  
    public void SetToIdleIfWasWalking()
    {
        if (golemStateMachine.currentState is GolemWalkState)
        {
            golemStateMachine.ChangeState(golemIdleState);
        }
    }


    #region Combat
    public override void TakeDamage(float amount)
    {
        golemStateMachine.ChangeState(golemHurtState);
        Vector2 amt = Vectors.FindDirectionTowardsPlayer(playerRef, transform.position);
        rb.AddForce(amt * knockbackForce * -1,ForceMode2D.Impulse);
        base.TakeDamage(amount);
        if (health < 0.0f)
        {
            golemStateMachine.ChangeState(golemDeathState);
        }
    }

    #endregion
    #region Movement
    public void MoveGolem(Vector2 direction)
    {
        rb.velocity = speed * direction;
    }
    #endregion
}
