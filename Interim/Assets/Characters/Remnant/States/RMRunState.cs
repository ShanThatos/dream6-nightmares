using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RMRunState : RMState {

    public float runSpeed = 2f;
    public Vector2 runTimeRange = new Vector2(.5f, 3f);
    float totalRunTime = 0f;
    float runRemainingTime = 0f;
    bool isRunningRight = false;

    public override void enter() {
        controller.animator.Play("RemEnemyRun");
        runRemainingTime = totalRunTime = UnityEngine.Random.Range(runTimeRange.x, runTimeRange.y);
        isRunningRight = UnityEngine.Random.Range(0, 2) == 0;
    }

    public override void exit() {
        controller.rb.velocity *= new Vector2(.3f, 1);
    }

    public override void run() {
        if (runRemainingTime >= 0f)
            runRemainingTime -= Time.deltaTime;
        if (runRemainingTime <= 0f) {
            controller.switchState("RMIdle");
            return;
        }
        
        if (isRunningRight && controller.bounds.isNearRightBound() || !isRunningRight && controller.bounds.isNearLeftBound())
            isRunningRight = !isRunningRight;
        controller.setDirection(isRunningRight);

        controller.rb.velocity = new Vector2(runSpeed * (isRunningRight ? 1 : -1), controller.rb.velocity.y);
    }

    public override string getStateName() {
        return "RMRun";
    }
}

