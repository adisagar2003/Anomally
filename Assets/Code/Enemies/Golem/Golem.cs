using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : BaseEnemy
{


    public GolemStateMachine golemStateMachine;
    public GolemIdleState golemIdleState;
    public GolemWalkState golemWalkState;
    public GolemAttackState golemAttackState;

    [SerializeField] private string debugString = "";

    #region Movement
    private float speed = 10.0f;
    private Rigidbody2D rb;
    #endregion

    #region Player Detection
    private bool isInDetectableArea = false;
    #endregion

    #region Combat

    #endregion
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        golemStateMachine = new GolemStateMachine();
        golemIdleState = new GolemIdleState(this, golemStateMachine);
        golemWalkState = new GolemWalkState(this, golemStateMachine);
        golemAttackState = new GolemAttackState(this, golemStateMachine);
        rb = GetComponent<Rigidbody2D>();
        // initialize state
        golemStateMachine.Initialize(golemIdleState);
    }

    private void Update()
    {
        golemStateMachine.currentState.OnUpdate();
        debugString = $"Current State: {golemStateMachine.currentState.GetStateName()}" +
            "";
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

    #region Movement
    public void MoveGolem(Vector2 direction)
    {
        rb.velocity = speed * direction;
    }
    #endregion
}
