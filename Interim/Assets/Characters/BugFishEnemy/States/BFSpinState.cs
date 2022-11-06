using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFSpinState : BFState {

    const float SPIN_COOLDOWN = 1f;
    float spinTimer = 0f;

    public override void enter() {
        controller.animator.Play("BFSpin");
        spinTimer = SPIN_COOLDOWN + UnityEngine.Random.Range(0f, 3f);
    }

    public override void reset() {
        spinTimer = SPIN_COOLDOWN + UnityEngine.Random.Range(0f, 3f);
    }

    public void FixedUpdate() {
        if (spinTimer >= 0)
            spinTimer -= Time.deltaTime;
    }

    public override void run() {
        if (controller.isAnimationDone("BFSpin")) 
            controller.switchState("BFFly");
    }

    public override bool canEnter() {
        return controller.currentState.getStateName() == "BFFly" && spinTimer <= 0 && getDistanceToPlayer() < controller.getPoint("SpinTriggerZone").localScale.x;
    }

    public override string getStateName() {
        return "BFSpin";
    }
}

