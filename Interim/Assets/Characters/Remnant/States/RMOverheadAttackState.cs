using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RMOverheadAttackState : RMState {

    public float attackCooldown = 5f;
    float remainingCooldown = 0f;


    public override void enter() {
        controller.animator.Play("RemEnemyOverheadAttack");
    }

    public override void exit() {
        remainingCooldown = attackCooldown;
    }

    public override void run() {
        if (controller.isAnimationDone("RemEnemyOverheadAttack")) {
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
        Transform tr = controller.getPoint("OverheadAttackZone");
        return Vector2.Distance(tr.position, GameManager.GetPlayerTransform().position) < tr.localScale.x;
    }

    public override string getStateName() {
        return "RMOverheadAttack";
    }
}

