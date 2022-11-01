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

    private float idleTime;

    public float attackDistance = 10f;

    private int volcanoCount = 0;
    private int digCount = 0;

    public override void enter()
    {
        controller.animator.Play("Idle");
        controller.damagable.damageReduction = DR;
        idleTime = Random.Range(minTime, maxTime);
    }

    public override void run()
    {
        idleTime -= Time.deltaTime;

        if (idleTime <= 0) {

            if (getDistanceToPlayer() < attackDistance)
            {
                // Choose an attack to use
                int rand = Random.Range(1, 11);
                if(rand % 2 == 0)
                {
                    if(volcanoCount <= 2)
                    {
                        switchVolcano();
                    }
                    else
                    {
                        switchDig();
                    }

                    
                }
                else
                {
                    if (digCount <= 2)
                    {
                        switchDig();
                    }
                    else
                    {
                        switchVolcano();
                    }
                }
            }
            else
            {
                controller.switchState("FCChase");
            }

        }
    }

    private void switchVolcano()
    {
        volcanoCount++;
        digCount = 0;
        controller.switchState("FCVolcano");
    }

    private void switchDig()
    {
        digCount++;
        volcanoCount = 0;
        controller.switchState("FCDig");
    }

    public override string getStateName()
    {
        return "FCIdle";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}

