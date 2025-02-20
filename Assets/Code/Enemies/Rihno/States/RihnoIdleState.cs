using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoIdleState : RihnoState
{
    [SerializeField] private float idleTime = 0.9f;
    [SerializeField] private float timer = 0.0f;
    private RihnoStateMachine rihnoStateMachine;
    private Rihno rihno;

    public RihnoIdleState(Rihno rihnoref, RihnoStateMachine rihnoStateMachine) : base(rihnoref, rihnoStateMachine)
    {
        this.rihnoStateMachine = rihnoStateMachine;
        this.rihno = rihnoref;

    }

    // Start is called before the first frame update
   
    public override void EnterState()
    {
        // start a coroutine to switch to chase
        timer = 0.0f;  
        base.EnterState();
    }

    public override void OnFixedUpdateState()
    {
        // future enemy animator 
        base.OnFixedUpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            // Exit State;

            rihnoStateMachine.ChangeState(rihno.rihnoChaseState);
            
        }   
    }
}
