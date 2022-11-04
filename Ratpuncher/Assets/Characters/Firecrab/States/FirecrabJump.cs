using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabJump : FirecrabState
{
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform bottom;
    [Range(30, 75)]
    public float angle;
    public Rigidbody2D rb;

    public bool landed;

    private float timer;


    public override void enter()
    {
        landed = false;
        timer = .5f;
        float disLeft = Vector3.Distance(transform.position, leftPoint.position);
        float disRight = Vector3.Distance(transform.position, rightPoint.position);

        Vector3 target = disLeft >= disRight ? leftPoint.position : rightPoint.position;
        Vector3 force = BallisticTrajectory(transform.position, target, angle);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public override void run()
    {
        if (!landed)
        {
            RaycastHit2D[] hits;
            Vector2 start = bottom.position;
            hits = Physics2D.RaycastAll(start, new Vector2(0, -1), 0.14f, LayerMask.GetMask("Platform"));

            if(hits.Length > 0)
            {
                rb.velocity = Vector2.zero;
                landed = true;
            }
        }
        if (landed)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                controller.switchState("FCIdle");
            }
        }
    }

    public override void exit()
    {
        controller.tempRangeMult = 3.0f;
    }

    public override string getStateName()
    {
        return "FCJump";
    }

    public Vector3 BallisticTrajectory(Vector3 start, Vector3 target, float angle)
    {
        float gravity = Physics.gravity.magnitude + rb.gravityScale;

        Vector3 dir = target - start;
        float height = dir.y;
        dir.y = 0;
        float dis = dir.magnitude;
        float radians = angle * Mathf.Deg2Rad;
        dir.y = dis * Mathf.Tan(radians);
        dis += height / Mathf.Tan(radians);

        float velocity = Mathf.Sqrt(dis * gravity / Mathf.Sin(2 * radians));
        
        Vector3 force = velocity * dir.normalized;
        
        return force;
    }
}
