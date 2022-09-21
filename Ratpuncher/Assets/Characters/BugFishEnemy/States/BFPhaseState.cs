using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFPhaseState : BFState {

    float time;

    public override void enter() {
        controller.animator.Play("BFPhase");
        time = 1f;
        controller.getDamagable().setInvinicibility(true);
    }
    public override void exit() {
        controller.getDamagable().setInvinicibility(false);
    }

    public override void run() {
        if (time >= 0)
            time -= Time.deltaTime;
        if (time <= 0 && controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            controller.switchState("BFFly");
    }

    public override bool canEnter() {
        if (controller.currentState.getStateName() != "BFIdle" && controller.currentState.getStateName() != "BFHurt")
            return false;
        return controller.getDamagable().GetHealth() <= controller.getDamagable().GetMaxHealth() / 2;
    }

    public override string getStateName() {
        return "BFPhase";
    }
}

