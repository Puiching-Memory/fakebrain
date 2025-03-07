using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;

    public void Init(State startState)
    {
        currentState = startState;
        startState.EnterState();
    }

    public void ChangeState(State newState)
    {
        currentState.ExitState();
        currentState = newState;
        Debug.Log(newState);
        currentState.EnterState();
    }
}
