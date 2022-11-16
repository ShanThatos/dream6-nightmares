using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabDig : FirecrabState
{
    public GameObject drillPrefab;
    public Transform drillPoint;
    public int baseCount = 3;
    public float startLag = .8f;
    public float endLag = .7f;
    public Vector2 randRange;

    private float timer;

    private bool spawned;
    private bool done;
    private int drillCount;
    private int drillsLeft;


    public override void enter()
    {
        timer = startLag;
        spawned = false;
        done = false;
        controller.animator.Play("DigStart");

        drillCount = baseCount;
        float healthPercent = controller.damagable.GetHealthPercent();
        if(healthPercent < 0.4)
        {
            drillCount++;
        }
        if(healthPercent < 0.7)
        {
            drillCount++;
        }

        drillsLeft = 0;
    }

    public override void run()
    {
        if(!spawned)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                // Spawn drills
                spawned = true;
                GameObject drill = null;
                while(drillCount > 0)
                {
                    drill = Instantiate(drillPrefab, drillPoint.position, Quaternion.identity);

                    if(drillCount != 1)
                    {
                        // Set non-player targets of first X - 1 drills
                        float rand = Random.Range(randRange.x, randRange.y);
                        rand *= Random.Range(0, 2) * 2 - 1;

                        Vector3 tgt = drillPoint.position;
                        tgt.x += rand;
                        drill.GetComponent<TrackingDrill>().manualSetTarget(tgt);
                    }
                    drillsLeft++;
                    drill.GetComponent<TrackingDrill>().OnExecute += DrillDone;
                    drillCount--;
                }
                timer = endLag;
            }
        }
        else
        {
            if(done)
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    controller.switchState("FCWeak");
                }
            }
        }
    }

    public override string getStateName()
    {
        return "FCDig";
    }

    private void DrillDone()
    {
        drillsLeft--;

        if(drillsLeft <= 0)
        {
            done = true;
        }
    }
}
