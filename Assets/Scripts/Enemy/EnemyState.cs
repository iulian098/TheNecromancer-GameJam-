using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PluggableAI;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{
    public Enemy enemy;
    public State currentState;
    public State remainState;

    private void LateUpdate()
    {
        if (enemy.Agent.isActiveAndEnabled && currentState != null)
            currentState.UpdateState(enemy);
    }

    public void TransitionToState(State nextState)
    {
        //If next state changed, change current state
        if (nextState != remainState)
            currentState = nextState;
    }

    public void SetCurrentState(State newState) {
        currentState = newState;
    }
}
