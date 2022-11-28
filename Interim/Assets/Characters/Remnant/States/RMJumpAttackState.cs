using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RMJumpAttackState : RMState {

    public float attackCooldown = 5f;
    float remainingCooldown = 0f;

    public Vector2 jumpForce = new Vector2(10f, 10f);
    bool jumpedForward = false;

    public float decelerateDistance = 4f;

    public GameObject shockSpawnerPrefab;
    bool spawnedShockwave = false;

    public override void enter() {
        controller.animator.Play("RemEnemyJumpAttack");
        jumpedForward = false;
        spawnedShockwave = false;
    }

    public override void exit() {
        remainingCooldown = attackCooldown;
    }

    public override void run() {

        if (controller.cues.shouldJumpForward) {
            if (!jumpedForward) {
                bool jumpRight = GameManager.GetPlayerTransform().position.x > controller.transform.position.x;
                controller.setDirection(jumpRight);
                controller.rb.AddForce(new Vector2(jumpRight ? jumpForce.x : -jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
                jumpedForward = true;
            }
        }

        if (jumpedForward) {
            if (Mathf.Abs(GameManager.GetPlayerTransform().position.x - controller.transform.position.x) < decelerateDistance) {
                Vector2 vel = controller.rb.velocity;
                vel.x *= Mathf.Pow(0.05f, Time.deltaTime);
                controller.rb.velocity = vel;
            }
        }

        if (controller.cues.shouldSpawnShockwave && !spawnedShockwave) {
            Instantiate(shockSpawnerPrefab, controller.getPoint("JumpShockSpawnPoint").position, Quaternion.identity);
            spawnedShockwave = true;
        }

        if (controller.isAnimationDone("RemEnemyJumpAttack")) {
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
        float minDistance = controller.getPoint("MinJumpAttackZone").localScale.x;
        float maxDistance = controller.getPoint("MaxJumpAttackZone").localScale.x;
        float distance = getDistanceToPlayer();
        return distance >= minDistance && distance <= maxDistance;
    }

    public override string getStateName() {
        return "RMJumpAttack";
    }
}

