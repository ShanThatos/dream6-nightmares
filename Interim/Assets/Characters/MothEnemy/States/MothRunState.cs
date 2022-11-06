using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MothRunState : MothState {
    
    public float runAwayForce = 5f;
    public float runAwayDelay = 1f;
    float delay;

    public float runAwayTime = 5f;
    float time;

    bool flappedAway;

    public override void enter() {
        controller.animator.Play("MothIdle");
        delay = runAwayDelay;
        time = runAwayTime;
        flappedAway = false;
    }

    public override void run() {
        if (delay >= 0) {
            delay -= Time.deltaTime;
            return;
        }


        if (!flappedAway) {
            flappedAway = true;
            float playerX = GameManager.GetPlayerTransform().position.x;
            bool runningRight = playerX < transform.position.x;
            controller.setDirection(runningRight);
            controller.rb.AddForce(new Vector2(runningRight ? 1 : -1, 1) * runAwayForce, ForceMode2D.Impulse);
        }

        time -= Time.deltaTime;
        if (time <= 0)
            controller.switchState("MothIdle");
    }

    public override string getStateName() {
        return "MothRun";
    }
}

