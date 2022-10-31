using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabVolcano : FirecrabState
{
    public GameObject volcanoPrefab;
    public Transform launchPoint;
    public float interval;

    private int launchCount;
    private float timer;

    public override void enter()
    {
        float healthPercent = controller.damagable.GetHealthPercent();

        controller.animator.Play("Volcano");

        launchCount = 1;

        if(healthPercent < 0.4)
        {
            launchCount = 3;
        }
        else if (healthPercent < 0.7)
        {
            launchCount = 2;
        }

        timer = interval;
    }

    public override void run()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            if (launchCount > 0)
            {
                // Launch and attack
                Instantiate(volcanoPrefab, launchPoint.position, Quaternion.identity);
                launchCount--;
                timer = interval;
            }
            else if (launchCount <= 0)
            {
                // Enter weakened state
                controller.switchState("FCWeak");
            }
        }
        
    }

    public override string getStateName()
    {
        return "FCVolcano";
    }
}
