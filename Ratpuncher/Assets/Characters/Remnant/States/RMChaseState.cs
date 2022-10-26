using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMChaseState : RMState {

    public float chaseSpeed = 2f;

    public override void enter() {
        controller.animator.Play("RemEnemyRun");
    }

    public override void exit() {
        controller.rb.velocity *= new Vector2(.5f, 1);
    }

    public override void run() {
        if (getDistanceToPlayer() > controller.getPoint("ChaseZone").localScale.x + 1f) {
            controller.switchState(Random.Range(0, 2) == 0 ? "RMIdle" : "RMRun");
            return;
        }

        bool runningRight = controller.transform.position.x < GameManager.GetPlayerTransform().position.x;
        controller.setDirection(runningRight);
        if (getDistanceToPlayer() < .5f) {
            controller.rb.velocity *= new Vector2(.5f, 1);
            return;
        }
        
        controller.rb.velocity = new Vector2(chaseSpeed * (runningRight ? 1 : -1), controller.rb.velocity.y);
    }

    public override bool canEnter() {
        return controller.isCurrentStateIn("RMIdle", "RMRun") && getDistanceToPlayer() <= controller.getPoint("ChaseZone").localScale.x;
    }

    public override string getStateName() {
        return "RMChase";
    }
}

