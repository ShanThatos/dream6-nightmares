using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NWDeathState : NWState {

    bool calledOnDeath = false;

    public override void enter() {
        controller.animator.Play("NarwhaleDeath");
        controller.GetDamagable().setInvincibility(true);
        controller.rb.velocity = Vector2.zero;
    }

    public override void run() {
        if (controller.isAnimationDone("NarwhaleDeath")) {
            if (!calledOnDeath) {
                calledOnDeath = true;
                controller.onDeathEvent.Invoke();
                Destroy(controller.gameObject);
            }
        }
    }

    public override string getStateName() {
        return "NWDeath";
    }
}
