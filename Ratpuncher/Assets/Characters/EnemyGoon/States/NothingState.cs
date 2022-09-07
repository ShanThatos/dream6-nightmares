using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NothingState : TimedState {

    public IdleState idleState;

    public override void onEnter() {
        setTimedState(1f, idleState);
    }

    public override string getStateName() { return "Nothing"; }
}