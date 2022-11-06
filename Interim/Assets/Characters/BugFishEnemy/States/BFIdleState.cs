using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFIdleState : BFState {

    bool isGoingRight = true;

    public override void enter() {
        controller.animator.Play("BFIdle");
    }

    public override void run() {
        if (isGrounded()) {
            if (controller.cues.inIdleCanMove) {
                controller.transform.Translate((isGoingRight ? Vector2.right : Vector2.left) * controller.idleSpeed * Time.deltaTime);
                controller.setDirection(isGoingRight);
                if (!canMoveForward())
                    isGoingRight = !isGoingRight;
            } else
                isGoingRight = UnityEngine.Random.Range(0, 2) == 0;
        }
    }

    public override string getStateName() {
        return "BFIdle";
    }
}

