using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rihno : BaseEnemy
{
    [SerializeField] private Player player;

    #region RihnoStates
    public RihnoStateMachine rihnoStateMachine;
    public RihnoIdleState rihnoIdleState;
    public RihnoAttackState rihnoAttackState;
    public RihnoChaseState rihnoChaseState;
    public RihnoHurtState rihnoHurtState;
    public RihnoDeathState rihnoDeathState;
    #endregion

    #region Movement 
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5.0f;
    #endregion

    #region Combat 
    [SerializeField] private float knockbackForce = 5.0f;
    [SerializeField] private float deathCooldown = 1.4f;
    [SerializeField] private float minimumDistanceToWaitForAttack = 5.0f;
    #endregion

    #region Debug
    [SerializeField] private string debugString = "";
    #endregion

    private void Awake()
    {
        rihnoStateMachine = new RihnoStateMachine();
        rb = GetComponent<Rigidbody2D>();
        rihnoIdleState = new RihnoIdleState(this, rihnoStateMachine);
        rihnoAttackState = new RihnoAttackState(this, rihnoStateMachine);
        rihnoChaseState = new RihnoChaseState(this, rihnoStateMachine);
        rihnoHurtState = new RihnoHurtState(this, rihnoStateMachine);
        rihnoDeathState = new RihnoDeathState(this, rihnoStateMachine,deathCooldown);
    }

    private void Start()
    {
        rihnoStateMachine.Initialize(rihnoIdleState);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SetRandomSpeed();
    }

    // Set Random speed on Spwan
    private void SetRandomSpeed()
    {
        speed = Random.Range(speed - 5, speed + 6);
    }

    private void Update()
    {
        if (rihnoStateMachine == null) return;
        debugString = $"Current State: {rihnoStateMachine.currentState}" +
            $"\t Combat \t" +
            $"\n Is Cooldown: ";
        rihnoStateMachine.currentState.OnUpdateState();
    }

    private void FixedUpdate()
    {
        rihnoStateMachine.currentState.OnFixedUpdateState();
    }
    public override void Attack()
    {
        // Attack player if not null.
        player.TakeDamage(damage);
    }

    // Attack the player
    public void Attack(Vector2 direction)
    {
        if (player == null) return;
        if (CheckForAttackingAgain())
        {
            if (direction.x > 0.0f) player.TakeDamage(direction, true, damage);
            if (direction.x < 0.0f) player.TakeDamage(direction, false, damage);
            // change state
            rihnoStateMachine.ChangeState(rihnoAttackState);
           
        }
    }

    private bool CheckForAttackingAgain()
    {
        return (rihnoStateMachine.currentState is RihnoChaseState
                || rihnoStateMachine.currentState is RihnoIdleState
                || rihnoStateMachine.currentState is RihnoAttackState
        );
    }

    public override void Damage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }

    public override void Death()
    {
        
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    protected override void WaitForPlayerToRecover(float amt)
    {
        if (player != null && Vector2.Distance(player.transform.position,gameObject.transform.position) < minimumDistanceToWaitForAttack)
        {
            Debug.Log("Everyone changes to idle");
            rihnoStateMachine.ChangeState(rihnoIdleState);
        }
       
    }

    public override void DisableAllAttacks()
    {
        // disable all colliders internally 
        Collider2D[] childrenColliders = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D childCollider in childrenColliders)
        {
            childCollider.enabled = false;
        }
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void TakeDamage(float amount)
        
    {
        if (rihnoStateMachine.currentState is RihnoHurtState) return;
        base.TakeDamage(amount);
        
        rihnoStateMachine.ChangeState(rihnoHurtState);
        KnockBack();
        DeathIfHealthTooLow();
        
    }

    [ContextMenu("RihnoKnockBack")]
    private void KnockBack()
    {
        // Add a knockback force
        if (player != null)
        {
            Vector2 directionOfForce = (transform.position - player.transform.position).normalized;
            directionOfForce.y = 0;
            rb.AddForce(directionOfForce * knockbackForce, ForceMode2D.Impulse);
        }
    }
    private void DeathIfHealthTooLow()
    {
        if (health < 0.1f)
        {
            rihnoStateMachine.ChangeState(rihnoDeathState);
        }
    }
    public override string ToString()
    {
        return base.ToString();
    }

    // future migration: RihnoMovement.cs
    public void MoveRihno(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

}
