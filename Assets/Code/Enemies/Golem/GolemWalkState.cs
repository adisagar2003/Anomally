using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemWalkState : GolemState
{
    
    public GolemWalkState(Golem golemRef, GolemStateMachine golemStateMachine) : base(golemRef, golemStateMachine)
    {
        currentStateName = "Walk";
    }
    public override void EnterState()
    {
        golemAnimator.SetBool("isWalking", true);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void ExitState()
    {
        golemAnimator.SetBool("isWalking", false);
        base.ExitState();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string GetStateName()
    {
        return base.GetStateName();
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
}
