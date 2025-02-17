using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RihnoState
{
    public bool isCompleted { get; protected set; }
    protected float startTime;
    protected Rihno rihno;
    protected RihnoStateMachine rihnoStateMachine;
    public float time => Time.time - startTime;

    public RihnoState(Rihno rihnoref, RihnoStateMachine rihnoStateMachine)
    {
        this.rihno = rihnoref;
        this.rihnoStateMachine = rihnoStateMachine;
    }

    
    public virtual void EnterState()
    {
        // Start state
    }

    public virtual void ExitState()
    {
        // End state
    }

    public string GetStateName()
    {
        
        return "";
    }
    public virtual void OnUpdateState()
    {
        // this part runs inside Update();
    }

    public virtual void OnFixedUpdateState()
    {
        // this part runs inside FixedUpdate();
    }
}
