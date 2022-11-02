using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RMMainAttackState : RMState {

    public float mainAttackCooldown = 1f;
    float remainingCooldown = 0f;


    public override void enter() {
        controller.animator.Play("RemEnemyMainAttack");
    }

    public override void exit() {
        remainingCooldown = mainAttackCooldown;
    }

    public override void run() {
        if (controller.isAnimationDone("RemEnemyMainAttack")) {
            controller.switchState("RMIdle");
            return;
        }
    }

    private void FixedUpdate() {
        if (remainingCooldown >= 0f)
            remainingCooldown -= Time.deltaTime;
    }

    public override bool canEnter() {
        if (remainingCooldown > 0f || !controller.isCurrentStateIn("RMIdle", "RMRun", "RMChase"))
            return false;
        Transform tr = controller.getPoint("MainAttackZone");
        return Vector2.Distance(tr.position, GameManager.GetPlayerTransform().position) < tr.localScale.x;
    }

    public override string getStateName() {
        return "RMMainAttack";
    }
}

