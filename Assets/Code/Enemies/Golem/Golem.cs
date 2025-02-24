using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : BaseEnemy
{


    private GolemStateMachine golemStateMachine;
    private GolemIdleState golemIdleState;
    private GolemWalkState golemWalkState;

    [SerializeField] private string debugString = "";

    #region Player Detection
    private bool isInDetectableArea = false;
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
}
