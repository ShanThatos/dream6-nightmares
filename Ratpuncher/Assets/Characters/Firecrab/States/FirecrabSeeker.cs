using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabSeeker : FirecrabState
{
    public GameObject leftSeeker;
    public GameObject rightSeeker;
    public Transform launchPos;
    public float delay;
    public int baseCount = 2;

    private int count;
    private float offset;
    private float timer;

    private Transform player;

    public override void enter()
    {
        controller.animator.Play("Volcano");
        player = GameManager.GetPlayerTransform();
        count = baseCount;
        offset = 0;
        timer = delay * 2;
        if(controller.damagable.GetHealthPercent() <= .5)
        {
            count++;
        }
    }

    public override void run()
    { 
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if(count <= 0)
            {
                controller.switchState("FCWeak");
            }
            else
            {
                // GameObject seeker = player.position.x < launchPos.position.x ? leftSeeker : rightSeeker;
                Vector3 pos = launchPos.position;
                pos.y += offset;
                GameObject seeker = Instantiate(rightSeeker, pos, Quaternion.identity);

                seeker.transform.LookAt(player);

                offset += .5f;
                timer = delay;
                count--;
                if (count <= 0)
                {
                    timer *= 2;
                }
            }
        }
    }

    public override string getStateName()
    {
        return "FCSeeker";
    }
}
