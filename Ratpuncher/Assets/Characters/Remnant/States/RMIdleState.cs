using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RMIdleState : RMState {
    
    public Vector2 idleTimeRange = new Vector2(.5f, 3f);
    float idleRemainingTime = 0f;

    public override void enter() {
        controller.animator.Play("RemEnemyIdle");
        idleRemainingTime = UnityEngine.Random.Range(idleTimeRange.x, idleTimeRange.y);
    }

    public override void run() {
        if (idleRemainingTime >= 0f)
            idleRemainingTime -= Time.deltaTime;
        else
            controller.switchState("RMRun");
    }

    public override string getStateName() {
        return "RMIdle";
    }
}

