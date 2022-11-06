using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabVolcano : FirecrabState
{
    public GameObject volcanoPrefab;
    public Transform launchPoint;
    public float interval;
    public int baseCount = 1;

    private int launchCount;
    private float timer;

    public override void enter()
    {
        float healthPercent = controller.damagable.GetHealthPercent();

        controller.animator.Play("Volcano");

        launchCount = baseCount;

        if(healthPercent < 0.4)
        {
            launchCount++;
        }

        if(healthPercent < 0.7)
        {
            launchCount++;
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
