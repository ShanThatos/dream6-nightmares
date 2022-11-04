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
    private float rangeMult = 1.0f;

    public override void enter()
    {
        controller.animator.Play("Idle");
        controller.damagable.damageReduction = DR;
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

    private string selectAction()
    {
        string action = "FCJump";
        int rand = Random.Range(1, 6);

        switch (rand)
        {
            case 1:
            case 2:
                if(volcanoCount <= 2)
                {
                    action = "FCVolcano";
                    volcanoCount++;
                    break;
                }
                goto case 3;
            case 3:
            case 4:
                if (digCount <= 2)
                {
                    action = "FCDig";
                    digCount++;
                }
                break;
            case 5:
                break;
        }

        if(action.Equals("FCJump")){
            digCount = 0;
            volcanoCount = 0;
        }

        return action;
    }

    public override string getStateName()
    {
        return "FCIdle";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance * rangeMult);
    }
}

