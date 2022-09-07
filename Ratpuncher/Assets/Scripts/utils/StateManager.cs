using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public State currentState;

    public virtual void Start() {
        currentState?.onEnter();
    }

    public virtual void Update() {
        State nextState = checkForAnyState();
        if (nextState == null)
            nextState = currentState?.run();
        if (nextState != null && nextState != currentState) {
            switchState(nextState);
        }
    }

    // can be overriden for anystate-state transitions (such as when the player comes in viewing range of an enemy)
    protected virtual State checkForAnyState() {
        return null;
    }

    public virtual void switchState(State newState) {
        currentState?.onExit();
        currentState = newState;
        currentState?.onEnter();
    }
}