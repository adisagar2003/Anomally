using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHurtState : GolemState
{
    private float timer = 0.0f;
    private AnimationClip hurtAnimationClip;
    public GolemHurtState(Golem golemRef, GolemStateMachine golemStateMachine) : base(golemRef, golemStateMachine)
    {
        currentStateName = "GolemHurtState";
        SetHurtAnimationClip();   
    }

    private void SetHurtAnimationClip()
    {
        AnimationClip[] animationClips = golemAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip animClip in animationClips)
        {
            if (animClip.name == "Hurt")
            {
                hurtAnimationClip = animClip;
            }
        }
    }

    public override void EnterState()
    {
        timer = 0.0f;
        golemAnimator.SetTrigger("Hurt");
        golem.DisableAllColliders();
        base.EnterState();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void ExitState()
    {
        golem.EnableAllColliders();
        golemAnimator.ResetTrigger("Hurt");
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
        if (timer > hurtAnimationClip.length)
        {
            golemStateMachine.ChangeState(golem.golemWalkState);
        }
        base.OnUpdate();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
