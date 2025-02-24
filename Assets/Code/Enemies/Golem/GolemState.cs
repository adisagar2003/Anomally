using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GolemState 
{
    protected Golem golem;
    protected GolemStateMachine golemStateMachine;
    protected Animator golemAnimator;
    protected string currentStateName;
    protected Player playerRef;
    public GolemState(Golem golemRef, GolemStateMachine golemStateMachine)
    {
        this.golem = golemRef;
        this.golemStateMachine = golemStateMachine;
        golemAnimator = golem.GetComponentInChildren<Animator>();
        playerRef = GameObject.FindFirstObjectByType<Player>();
    }


    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual string GetStateName()
    {
        return currentStateName;
    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnUpdate()
    {

    }

    
}
