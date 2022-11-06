using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatIdleState : RatEnemyState {

    float idleTime = 0f;

    public override void enter() {
        controller.animator.Play("RatIdle");
        idleTime = UnityEngine.Random.Range(2f, 5f);
    }

    public override void run() {
        idleTime -= Time.deltaTime;
        if (idleTime <= 0f)
            controller.switchState("RatWalk");
    }

    public override string getStateName() {
        return "RatIdle";
    }
}

