using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RMThrowAttackState : RMState {

    public float attackCooldown = 3.5f;
    float remainingCooldown = 0f;

    public GameObject javelinPrefab;
    bool spawnedJavelin = false;


    public override void enter() {
        controller.animator.Play("RemEnemyThrowAttack");
        spawnedJavelin = false;

        bool facingRight = GameManager.GetPlayerTransform().position.x > controller.transform.position.x;
        controller.setDirection(facingRight);
    }

    public override void exit() {
        remainingCooldown = attackCooldown;
    }

    public override void run() {
        if (controller.cues.shouldSpawnJavelin && !spawnedJavelin) {
            Transform tr = controller.getPoint("JavelinSpawnPoint");
            Instantiate(javelinPrefab, tr.position, Quaternion.identity);
            spawnedJavelin = true;
        }

        if (controller.isAnimationDone("RemEnemyThrowAttack")) {
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
        float minDistance = controller.getPoint("MinThrowAttackZone").localScale.x;
        float maxDistance = controller.getPoint("MaxThrowAttackZone").localScale.x;
        float distance = getDistanceToPlayer();
        return distance >= minDistance && distance <= maxDistance;
    }

    public override string getStateName() {
        return "RMThrowAttack";
    }
}

