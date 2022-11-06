using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFHurtState : BFState {


    float time;

    public override void enter() {
        controller.animator.Play("BFIdle");
        time = controller.hurtTime;
    }

    public override void run() {
        if (time >= 0)
            time -= Time.deltaTime;
        if (time <= 0)
            controller.switchState("BFIdle");
    }

    public override string getStateName() {
        return "BFHurt";
    }
}

