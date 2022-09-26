using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFDeathState : BFState {

    bool calledOnDeath = false;


    public override void enter() {
        controller.getDamagable().setInvincibility(true);
        controller.animator.Play("BFDeath");
        controller.rb.velocity = Vector2.zero;
    }

    public override void run() {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName("BFDeath") && controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) {
            if (!calledOnDeath) {
                calledOnDeath = true;
                controller.onDeathEvent.Invoke();
            }
        }
    }

    public override string getStateName() {
        return "BFDeath";
    }
}

