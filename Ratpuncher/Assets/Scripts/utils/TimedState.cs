using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TimedState : State {

    protected float timeToWait;
    protected State nextState;

    public void setTimedState(float timeToWait, State nextState) {
        this.timeToWait = timeToWait;
        this.nextState = nextState;
    }


    public override State run() {
        timeToWait -= Time.deltaTime;
        if (timeToWait <= 0)
            return nextState;
        return this;
    }
}