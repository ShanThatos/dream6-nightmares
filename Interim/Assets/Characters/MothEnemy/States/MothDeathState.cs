using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MothDeathState : MothState {
    bool calledOnDeath = false;
    public override void enter() {
        controller.getDamagable().setInvincibility(true);
        controller.animator.Play("MothDeath");
        controller.rb.velocity = Vector2.zero;
        controller.rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void run() {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName("MothDeath") && controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) {
            if (!calledOnDeath) {
                calledOnDeath = true;
                Destroy(controller.gameObject, 3);
                controller.onDeathEvent.Invoke();
            }
        }
    }

    public override string getStateName() {
        return "MothDeath";
    }
}

