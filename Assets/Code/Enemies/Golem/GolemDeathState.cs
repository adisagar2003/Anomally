using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDeathState : GolemState
{
    private float timer = 0.0f;
    private AnimationClip deathAnimationClip;

    public GolemDeathState(Golem golemRef, GolemStateMachine golemStateMachine) : base(golemRef, golemStateMachine)
    {
        this.currentStateName = "GolemDeathState";
        SetDeathAnimationClip();
    }

    public override void EnterState()
    {
        timer = 0.0f;
        golemAnimator.SetTrigger("Death");
        golem.DisableAllColliders();
        base.EnterState();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void ExitState()
    {
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
        if (timer > deathAnimationClip.length +1)
        {
            golem.Death();
        }
        base.OnUpdate();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    private void SetDeathAnimationClip()
    {
        AnimationClip[] animationClips = golemAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip animClip in animationClips)
        {
            if (animClip.name == "Hurt")
            {
                deathAnimationClip = animClip;
            }
        }
    }


}
