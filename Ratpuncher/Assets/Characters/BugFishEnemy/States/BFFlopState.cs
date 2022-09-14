using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFFlopState : BFState {



    const float FLOP_COOLDOWN = 4f;
    float flopTimer = 0f;

    float idleTimerMax = 2f;
    float idleTimer;

    bool floppedUp = false;
    bool floppedDown = false;

    bool shockSpawned = false;

    public override void enter() {
        controller.animator.Play("BFFlop", 0, 0f);
        flopTimer = FLOP_COOLDOWN + UnityEngine.Random.Range(0f, 2f);
        idleTimer = idleTimerMax;
        floppedUp = false;
        floppedDown = false;
        shockSpawned = false;
    }

    public void FixedUpdate() {
        if (flopTimer >= 0)
            flopTimer -= Time.deltaTime;
    }

    public override void run() {
        if (idleTimer > 0)
            idleTimer -= Time.deltaTime;
        if (controller.cues.inFlopGoingUp && !floppedUp) {
            Vector2 impulse = Vector2.zero;
            impulse.x += (GameManager.getPlayerTransform().position.x < controller.transform.position.x ? -1 : 1) * controller.flopUpSideForce;
            impulse += Vector2.up * controller.flopUpForce;
            controller.rb.AddForce(impulse, ForceMode2D.Impulse);
            floppedUp = true;
        }
        if (controller.cues.inFlopGoingDown && !floppedDown) {
            controller.rb.AddForce(Vector2.down * controller.flopDownForce, ForceMode2D.Impulse);
            floppedDown = true;
        }

        if (idleTimer <= 0 && controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && isGrounded())
            controller.switchState("BFIdle");
        
        if (controller.cues.inFlopStartShock && !shockSpawned && isGrounded()) {
            GameObject shockSpawner = Instantiate(controller.shockSpawnerPrefab, Physics2D.Linecast(controller.transform.position, controller.getPoint("BottomPoint").position, LayerMask.GetMask("Platform")).point, Quaternion.identity);
            shockSpawned = true;
        }
    }

    public override bool canEnter() {
        if (controller.currentState.getStateName() != "BFIdle")
            return false;
        return flopTimer <= 0 && getDistanceToPlayer() < controller.getPoint("FlopTriggerZone").localScale.x;
    }

    public override string getStateName() {
        return "BFFlop";
    }
}

