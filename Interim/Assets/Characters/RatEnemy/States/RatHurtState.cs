using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatHurtState : RatEnemyState {

    float time = 0f;

    public override void enter() {
        controller.animator.Play("RatIdle");
        time = 1.5f;
    }

    public override void run() {
        time -= Time.deltaTime;
        if (time <= 0f)
            controller.switchState("RatIdle");
    }

    public override string getStateName() {
        return "RatHurt";
    }
}

