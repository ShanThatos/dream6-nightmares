using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabDig : FirecrabState
{
    public GameObject drillPrefab;
    public Transform drillPoint;
    public float startLag = .8f;
    public float endLag = .7f;

    private float timer;

    private bool spawned;
    private bool done;

    public override void enter()
    {
        timer = startLag;
        spawned = false;
        done = false;
        controller.animator.Play("Dig");
    }

    public override void run()
    {
        if (!spawned)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                // Spawn drill
                spawned = true;
                GameObject drill = Instantiate(drillPrefab, drillPoint.position, Quaternion.identity);
                drill.GetComponent<TrackingDrill>().OnExecute += DrillDone;
                timer = endLag;
            }
        }
        else
        {
            if (done)
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    controller.switchState("FCDigWeak");
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
        done = true;
    }
}
