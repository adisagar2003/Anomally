using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GolemAttackState : GolemState
{
    private float timer = 0.0f;
    private float attackAnimDuration = .35f;
    private AnimationClip attackAnimClip; 

    public GolemAttackState(Golem golemRef, GolemStateMachine golemStateMachine) : base(golemRef, golemStateMachine)
    {
        currentStateName = "Attack";
    }

    public override void EnterState()
    {
        golemAnimator.SetTrigger("Attack");
        SetAttackAnimationDuration();
    }

    private void SetAttackAnimationDuration()
    {
        AnimationClip[] animClips = golemAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip animClip in animClips)
        {
            if (animClip.name == "Attack")
            {
                attackAnimClip = animClip;
            }
        }
        attackAnimDuration = attackAnimClip.length;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void ExitState()
    {
        golemAnimator.ResetTrigger("Attack");

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
        timer += Time.deltaTime;
        if (timer > attackAnimDuration)
        {
            if (Vectors.DistanceBetweenTwoObjects(golem, playerRef) < 2.1f)
            {
                golemStateMachine.ChangeState(golem.golemAttackState);
            }
            else
            {
                golemStateMachine.ChangeState(golem.golemIdleState);
            }
        }
        base.OnUpdate();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
