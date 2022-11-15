using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabIdle : FirecrabState
{
    public float minTime = .5f;
    public float maxTime = 2f;

    [SerializeField]
    [Tooltip("Damage resistance (1 = invunerable)")]
    [Range(0f, 1f)]
    private float DR = 0.9f;

    [SerializeField]
    [Tooltip("Energy damage resistance (1 = invunerable)")]
    [Range(0f, 1f)]
    private float EDR = 0.9f;

    public PlayerDetector slamZone;

    private float idleTime;

    public float attackDistance = 10f;

    private float rangeMult = 1.0f;
    private float attackCount = 0;
    private bool canSlam = true;
    public override void enter()
    {
        controller.animator.Play("Idle");
        controller.damagable.damageReduction = DR;
        controller.damagable.energyDamageReduction = EDR;
        idleTime = Random.Range(minTime, maxTime);
        rangeMult = controller.tempRangeMult;
    }

    public override void run()
    {
        idleTime -= Time.deltaTime;

        if (idleTime <= 0) {

            if (getDistanceToPlayer() < attackDistance * rangeMult)
            {
                // Choose an attack to use
                string action = selectAction();
                controller.switchState(action);
            }
            else
            {
                controller.switchState("FCChase");
            }

        }
    }

    public override void exit()
    {
        controller.tempRangeMult = 1;
    }

    private string selectAction()
    {
        Debug.Log("Inside " + slamZone.playerInside);

        if (slamZone.playerInside && canSlam)
        {
            // Slam if player is too close (inside)
            canSlam = false;
            attackCount++;
            return "FCSlam";
        }

        string action = "FCJump";
        int rand = Random.Range(1, 6);

        if(attackCount >= 3)
        {
            // Force using jump after 3 attacks
            rand = 5;
        }

        switch (rand)
        {
            case 1:
            case 2:
                action = "FCVolcano";
                attackCount++;
                break;
            case 3:
            case 4:
                 action = "FCDig";
                 attackCount++;
                break;
            case 5:
                resetCounts();
                break;
        }

        return action;
    }

    public override string getStateName()
    {
        return "FCIdle";
    }

    public void resetCounts()
    {
        attackCount = 0;
        canSlam = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance * rangeMult);
    }
}

