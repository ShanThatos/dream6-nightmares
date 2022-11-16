using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabChase : FirecrabState
{
    public float chaseSpeed;
    public float targetDistance;

    int direction;
    Vector2 movement;
    public override void enter()
    {
        controller.animator.Play("Walk");
        direction = (transform.position.x - GameManager.instance.player.transform.position.x > 0) ? -1 : 1;
        movement = new Vector2(chaseSpeed, 0) * direction;
    }

    public override void run()
    {
        controller.gameObject.transform.Translate(movement * Time.deltaTime);
        if(getDistanceToPlayer() <= targetDistance)
        {
            controller.switchState("FCIdle");
        }
    }

    public override string getStateName()
    {
        return "FCChase";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetDistance);
    }
}
