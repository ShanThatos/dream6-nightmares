using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatChaseState : RatEnemyState {

    string[] preChaseStates = new string[] { "RatIdle", "RatWalk" };

    public override void enter() {
        controller.animator.Play("RatChase");
    }

    public override void run() {
        if (!isPlayerInTriggerDistance()) {
            controller.switchState("RatWalk");
            return;
        }

        Vector2 playerPos = GameManager.GetPlayerTransform().position;
        if (isGrounded()) {
            bool playerIsRight = playerPos.x > controller.transform.position.x;

            if (controller.isFacingRight() == playerIsRight) {
                if (canMoveForward())
                    controller.transform.Translate(Vector2.right * controller.speed * Time.deltaTime * (playerIsRight ? 1 : -1));
                else {
                    controller.transform.Translate(Vector2.left * controller.speed * Time.deltaTime * (playerIsRight ? 1 : -1));
                    controller.switchState("RatAttack");
                }
            } else
                controller.setDirection(playerIsRight);
        }
    }

    public bool isPlayerInTriggerDistance() {
        return getDistanceToPlayer() < controller.getPoint("ChaseTriggerZone").localScale.x;
    }
    public override bool canEnter() {
        string currentStateName = controller.currentState.getStateName();
        if (!Array.Exists(preChaseStates, stateName => stateName == currentStateName))
            return false;
        return isPlayerInTriggerDistance();
    }



    public override string getStateName() {
        return "RatChase";
    }
}

