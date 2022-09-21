using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatJumpState : RatEnemyState {

    public float HORIZONTAL_JUMP_FORCE = 6f;
    public float VERTICAL_JUMP_FORCE = 5f;

    public float JUMP_COOLDOWN = 5f;

    float idleWaitTime;
    float jumpWaitTime;

    public override void init() {
        base.init();
        jumpWaitTime = 0f;
    }

    public override void enter() {
        controller.animator.Play("RatAttack");
        bool facingRight = GameManager.GetPlayerTransform().position.x > transform.position.x;
        controller.rb.AddForce(new Vector2(facingRight ? HORIZONTAL_JUMP_FORCE : -HORIZONTAL_JUMP_FORCE, VERTICAL_JUMP_FORCE), ForceMode2D.Impulse);
        controller.setDirection(facingRight);
        idleWaitTime = 0.5f;
        jumpWaitTime = JUMP_COOLDOWN;
    }

    public override void run() {
        if (idleWaitTime > 0)
            idleWaitTime -= Time.deltaTime;
        
        if (idleWaitTime <= 0 && isGrounded()) {
            controller.rb.velocity = Vector2.zero;
            controller.switchState("RatIdle");
        }
    }

    public void FixedUpdate() {
        if (jumpWaitTime > 0) 
            jumpWaitTime -= Time.deltaTime;
    }


    public override bool canEnter() {
        if (controller.currentState.getStateName() != "RatChase")
            return false;
        // Debug.Log("Hey there " + getDistanceToPlayer());
        return controller.canJumpAttack && jumpWaitTime <= 0 && getDistanceToPlayer() > controller.getPoint("AttackTriggerZone").localScale.x;
    }

    public override string getStateName() {
        return "RatJump";
    }
}

