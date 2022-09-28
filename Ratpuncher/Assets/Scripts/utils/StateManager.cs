using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public State currentState;

    protected State[] states;
    
    public void Start() {
        states = transform.Find("States").GetComponentsInChildren<State>();
        init();
        foreach (State state in states) {
            state.setManager(this);
            state.init();
        }
        currentState?.enter();
    }
    public virtual void init() {}


    public void FixedUpdate() {
        if (GameManager.IsMovementLocked()) return;
        State nextState = Array.Find<State>(states, state => state.canEnter());
        if (nextState != null)
            switchState(nextState.getStateName());
        currentState.run();
        run();
    }

    public virtual void run() {}


    public void switchState(State state) {
        currentState?.exit();
        currentState = state;
        currentState?.enter();
    }
    public void switchState(string stateName) {
        switchState(Array.Find<State>(states, state => state.getStateName() == stateName));
    }
}