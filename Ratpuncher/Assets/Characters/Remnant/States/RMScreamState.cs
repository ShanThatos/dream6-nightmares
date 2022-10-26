using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RMScreamState : RMState {

    public float screamAttackCooldown = 1f;
    float remainingCooldown = 0f;

    public GameObject shockwavePrefab;
    bool spawnedShockwave = false;


    public override void enter() {
        controller.animator.Play("RemEnemyScream");
        spawnedShockwave = false;
    }

    public override void exit() {
        remainingCooldown = screamAttackCooldown;
    }

    public override void run() {
        if (controller.cues.shouldSpawnScreamShockwave && !spawnedShockwave) {
            Instantiate(shockwavePrefab, controller.transform.position, Quaternion.identity);
            spawnedShockwave = true;
        }

        if (controller.isAnimationDone("RemEnemyScream")) {
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
        float minDistance = controller.getPoint("MinScreamAttackZone").localScale.x;
        float maxDistance = controller.getPoint("MaxScreamAttackZone").localScale.x;
        float distance = getDistanceToPlayer();
        return distance >= minDistance && distance <= maxDistance;
    }

    public override string getStateName() {
        return "RMScream";
    }
}

