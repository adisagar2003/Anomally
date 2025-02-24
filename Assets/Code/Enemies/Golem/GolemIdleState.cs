using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemIdleState : GolemState
{
    
    public GolemIdleState(Golem golemRef, GolemStateMachine golemStateMachine) : base( golemRef,  golemStateMachine)
    {
        currentStateName = "Idle";
    }
    public override void EnterState()
    {
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void ExitState()
    {

    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override string GetStateName()
    {
        return base.GetStateName();
    }
}
