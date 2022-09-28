using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFBiteState : BFState {

    const float BITE_COOLDOWN = 5f;
    float biteTimer = 0f;

    public override void enter() {
        controller.animator.Play("BFBite");
        biteTimer = BITE_COOLDOWN + UnityEngine.Random.Range(0f, 3f);

        controller.setDirection(GameManager.GetPlayerTransform().transform.position.x > controller.transform.position.x);
    }

    public override void reset() {
        biteTimer = BITE_COOLDOWN + UnityEngine.Random.Range(0f, 3f);
    }

    public void FixedUpdate() {
        if (biteTimer >= 0)
            biteTimer -= Time.deltaTime;
    }

    public override void run() {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName("BFBite") && controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            controller.switchState("BFIdle");
    }

    public override bool canEnter() {
        return controller.currentState.getStateName() == "BFIdle" && biteTimer <= 0 && getDistanceToPlayer() < controller.getPoint("FlopTriggerZone").localScale.x;
    }

    public override string getStateName() {
        return "BFBite";
    }
}

