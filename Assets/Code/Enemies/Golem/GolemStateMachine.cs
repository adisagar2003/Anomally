using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStateMachine
{
    public GolemState currentState { get; private set; }

    public void Initialize(GolemState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(GolemState changedState)
    {
        currentState.ExitState();
        currentState = changedState;
        currentState.EnterState();
    }

}
