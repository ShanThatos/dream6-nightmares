using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFSwoopState : BFState {

    const float SWOOP_COOLDOWN = 5f;
    float swoopStateTimer = 0f;

    const float TIME_UNTIL_SWOOP = 1f;
    float timeUntilSwoop = 0f;

    bool startedSwoop = false;

    float swoopTime = 0f;

    float timeBetweenSonicBooms = .1f;
    float sonicBoomTimer = 0f;

    public override void enter() {
        controller.animator.Play("BFSwoop");
        swoopStateTimer = SWOOP_COOLDOWN + UnityEngine.Random.Range(0f, 3f);

        controller.rb.AddForce(new Vector2(0, controller.flyUpForce * 2), ForceMode2D.Impulse);
        timeUntilSwoop = TIME_UNTIL_SWOOP;
        startedSwoop = false;

        px = GameManager.GetPlayerTransform().position.x;
        py = GameManager.GetPlayerTransform().position.y;
    }

    public override void exit() {
        swoopStateTimer = SWOOP_COOLDOWN + UnityEngine.Random.Range(0f, 3f);
    }

    public override void reset() {
        swoopStateTimer = SWOOP_COOLDOWN + UnityEngine.Random.Range(0f, 3f);
    }

    public void FixedUpdate() {
        if (swoopStateTimer >= 0)
            swoopStateTimer -= Time.deltaTime;
    }


    // swoop data
    float tx, ty, px, py;

    public override void run() {
        if (timeUntilSwoop >= 0) {
            timeUntilSwoop -= Time.deltaTime;
            return;
        }

        if (!startedSwoop) {
            startedSwoop = true;
            swoopTime = 0;
            tx = controller.transform.position.x;
            ty = controller.transform.position.y;
            // px = GameManager.GetPlayerTransform().position.x;
            // py = GameManager.GetPlayerTransform().position.y;
            controller.rb.velocity = Vector2.zero;
            controller.rb.constraints = RigidbodyConstraints2D.FreezeAll;
            controller.setDirection(px > tx);

            Instantiate(controller.swoopBigSonicBoomPrefab, controller.getPoint("SonicBoomSpawnPoint").position, Quaternion.identity);
            sonicBoomTimer = timeBetweenSonicBooms;
        }

        sonicBoomTimer -= Time.deltaTime;
        if (sonicBoomTimer <= 0) {
            sonicBoomTimer = timeBetweenSonicBooms;
            Instantiate(controller.swoopSmallSonicBoomPrefab, controller.getPoint("SonicBoomSpawnPoint").position, Quaternion.identity);
        }


        swoopTime += Time.deltaTime;
        float t = swoopTime / controller.totalSwoopAttackTime;
        controller.transform.position = new Vector2(tx + 2 * (px - tx) * t, py + 4 * (t - .5f) * (t - .5f) * (ty - py));

        if (swoopTime >= controller.totalSwoopAttackTime) {
            controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            controller.switchState("BFFly");
        }
    }

    public override bool canEnter() {
        return controller.currentState.getStateName() == "BFFly" && swoopStateTimer <= 0 && getDistanceToPlayer() > controller.getPoint("SwoopTriggerZone").localScale.x;
    }

    public override string getStateName() {
        return "BFSwoop";
    }
}

