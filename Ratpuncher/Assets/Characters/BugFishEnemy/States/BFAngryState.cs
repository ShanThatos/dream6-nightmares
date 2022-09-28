using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFAngryState : BFState {

    const float ANGRY_COOLDOWN = 1f;
    float angryStateTimer = 0f;

    public override void enter() {
        controller.animator.Play("BFBite");

        bool isFacingRight = GameManager.GetPlayerTransform().transform.position.x > controller.transform.position.x;
        controller.setDirection(isFacingRight);
        controller.rb.AddForce(new Vector2(isFacingRight ? -30 : 30, 5), ForceMode2D.Impulse);
    }
    public override void exit() {
        angryStateTimer = ANGRY_COOLDOWN + UnityEngine.Random.Range(0f, 2f);
    }

    public override void reset() {
        angryStateTimer = ANGRY_COOLDOWN + UnityEngine.Random.Range(0f, 3f);
    }

    public void FixedUpdate() {
        if (angryStateTimer >= 0)
            angryStateTimer -= Time.deltaTime;
    }

    public override void run() {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).IsName("BFBite") && controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            controller.switchState("BFIdle");
    }

    public override bool canEnter() {
        return controller.currentState.getStateName() == "BFIdle" && angryStateTimer <= 0 && getDistanceToPlayer() < controller.getPoint("AngryTriggerZone").localScale.x;
    }

    public override string getStateName() {
        return "BFAngry";
    }
}

