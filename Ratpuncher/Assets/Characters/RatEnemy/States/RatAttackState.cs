using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatAttackState : RatEnemyState {

    public override void enter() {
        controller.animator.Play("RatAttack");
    }

    public override void run() {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            controller.switchState("RatChase");
    }

    public override bool canEnter() {
        if (controller.currentState.getStateName() != "RatChase")
            return false;
        return getDistanceToPlayer() < controller.getPoint("AttackTriggerZone").localScale.x;;
    }

    public override string getStateName() {
        return "RatAttack";
    }
}

