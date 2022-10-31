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
                    controller.switchState("FCVolcano");
                }
                else
                {
                    controller.switchState("FCDig");
                }

                
            }
            else
            {
                controller.switchState("FCChase");
            }

        }
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

