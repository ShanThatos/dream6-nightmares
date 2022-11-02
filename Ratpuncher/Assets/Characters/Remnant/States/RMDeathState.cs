using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RMDeathState : RMState {

    bool calledOnDeath = false;

    public override void enter() {
        controller.animator.Play("RemEnemyDeath");
    }

    public override void run() {
        if (controller.isAnimationDone("RemEnemyDeath")) {
            if (!calledOnDeath) {
                calledOnDeath = true;
                controller.onDeathEvent.Invoke();
                Destroy(controller.gameObject);
            }
        }
    }

    public override string getStateName() {
        return "RMDeath";
    }
}

