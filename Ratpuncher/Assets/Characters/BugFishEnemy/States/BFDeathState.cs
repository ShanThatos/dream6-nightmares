using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFDeathState : BFState {

    public override void enter() {
        controller.getDamagable().setInvincibility(true);
        controller.animator.Play("BFDeath");
        controller.rb.velocity = Vector2.zero;
    }

    public override void run() {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            Destroy(controller.gameObject);
    }

    public override string getStateName() {
        return "BFDeath";
    }
}

