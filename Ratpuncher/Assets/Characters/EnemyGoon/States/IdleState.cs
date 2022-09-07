using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleState : TimedState {

    public WanderState wanderState;

    public override void onEnter() {
        setTimedState(Random.Range(.4f, 5f), wanderState);
    }

    public override string getStateName() { return "Idle"; }
}