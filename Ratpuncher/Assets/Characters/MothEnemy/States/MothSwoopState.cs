using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MothSwoopState : MothState {
    
    const float SWOOP_COOLDOWN = 3f;
    float swoopStateTimer = 0f;

    const float TIME_UNTIL_SWOOP = 1f;
    float timeUntilSwoop = 0f;



    public float totalSwoopAttackTime = 1.5f;
    public float initialSwoopUpForce = 8f;
    float randomSwoopAttackTime;


    bool startedSwoop = false;

    float swoopTime = 0f;

    float timeBetweenSonicBooms = .1f;
    float sonicBoomTimer = 0f;

    public GameObject sonicBoomPrefab;


    float tx, ty, px, py;
    public override void enter() {
        controller.animator.Play("MothSwoop");

        controller.rb.AddForce(new Vector2(0, initialSwoopUpForce), ForceMode2D.Impulse);
        timeUntilSwoop = TIME_UNTIL_SWOOP;
        startedSwoop = false;

        px = GameManager.GetPlayerTransform().position.x;
        py = GameManager.GetPlayerTransform().position.y;
    }

    public override void exit() {
        controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        swoopStateTimer = SWOOP_COOLDOWN + UnityEngine.Random.Range(0f, 3f);
    }

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
            controller.rb.velocity = Vector2.zero;
            controller.rb.constraints = RigidbodyConstraints2D.FreezeAll;
            controller.setDirection(px > tx);

            Instantiate(sonicBoomPrefab, controller.getPoint("SonicBoomSpawnPoint").position, Quaternion.identity);
            sonicBoomTimer = timeBetweenSonicBooms;

            randomSwoopAttackTime = UnityEngine.Random.Range(totalSwoopAttackTime / 2f, totalSwoopAttackTime);
        }

        sonicBoomTimer -= Time.deltaTime;
        if (sonicBoomTimer <= 0) {
            sonicBoomTimer = timeBetweenSonicBooms;
            Instantiate(sonicBoomPrefab, controller.getPoint("SonicBoomSpawnPoint").position, Quaternion.identity);
        }


        swoopTime += Time.deltaTime;
        float t = swoopTime / totalSwoopAttackTime;
        controller.transform.position = new Vector2(tx + 2 * (px - tx) * t, py + 4 * (t - .5f) * (t - .5f) * (ty - py));

        if (swoopTime >= randomSwoopAttackTime) {
            controller.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            controller.switchState("MothIdle");
        }
    }

    public override bool canEnter() {
        return controller.currentState.getStateName() == "MothIdle" && swoopStateTimer <= 0 && getDistanceToPlayer() > controller.getPoint("SwoopTriggerZone").localScale.x && getDistanceToPlayer() < controller.getPoint("SwoopTriggerZone").localScale.x * 1.5f;
    }

    private void FixedUpdate() {
        if (swoopStateTimer >= 0)
            swoopStateTimer -= Time.deltaTime;
    }

    public override string getStateName() {
        return "MothSwoop";
    }
}

