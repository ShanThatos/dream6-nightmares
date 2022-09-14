using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatWalkState : RatEnemyState {

    float walkTime = 0f;
    Vector2 direction = Vector2.zero;

    public override void enter() {
        controller.animator.Play("RatWalk");
        walkTime = UnityEngine.Random.Range(2f, 5f);
        direction = UnityEngine.Random.Range(0, 2) == 0 ? Vector2.left : Vector2.right;
    }

    public override void run() {
        walkTime -= Time.deltaTime;
        if (walkTime <= 0f) {
            controller.switchState("RatIdle");
            return;
        }
        
        if (isGrounded()) {
            if (canMoveForward()) {
                controller.transform.Translate(direction * controller.speed * Time.deltaTime);
                controller.setDirection(direction.x > 0);
            } else {
                direction *= -1;
                controller.setDirection(direction.x > 0);
            }
        }
    }

    public override string getStateName() {
        return "RatWalk";
    }
}

