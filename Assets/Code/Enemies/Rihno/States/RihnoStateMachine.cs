using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RihnoStateMachine
{
    public RihnoState currentState { get; private set; }
    
    public void Initialize(RihnoState startingState) {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(RihnoState stateToChange)
    {
        currentState.ExitState();
        currentState = stateToChange;
        currentState.EnterState();
    }
}
