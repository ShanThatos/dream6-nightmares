using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CrabSlamPhase
{
    Rising,
    Hangtime,
    Falling,
    Landed
}
public class FirecrabSlam : FirecrabState
{
    public float jumpForce;
    public float airTime;
    public float slamForce;

    public Rigidbody2D rb;
    public GameObject shockwaveSpawner;
    public Transform bottom;
    public AttackHitbox hitbox;

    private float timer;
    private CrabSlamPhase phase;
    private float grav;

    public override void enter()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        timer = airTime;
        grav = rb.gravityScale;

        phase = CrabSlamPhase.Rising;
    }

    public override void run()
    {
       timer -= Time.deltaTime;
        
        if(timer <= 0)
        {
            if(phase == CrabSlamPhase.Rising)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                timer = airTime / 2f;

                phase = CrabSlamPhase.Hangtime;
            }
            else if(phase == CrabSlamPhase.Hangtime)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = grav;
                rb.AddForce(Vector2.down * slamForce, ForceMode2D.Impulse);
                phase = CrabSlamPhase.Falling;
                hitbox.isActive = true;
            }
        }

        if(phase == CrabSlamPhase.Falling)
        {
            RaycastHit2D[] hits;
            Vector2 start = bottom.position;
            hits = Physics2D.RaycastAll(start, new Vector2(0, -1), 0.14f, LayerMask.GetMask("Platform"));

            if (hits.Length > 0)
            {
                rb.velocity = Vector2.zero;
                Vector3 pos = bottom.position;
                pos.y += .8f;

                Instantiate(shockwaveSpawner, pos, Quaternion.identity);
                phase = CrabSlamPhase.Landed;
                hitbox.isActive = false;
            }
        }

        if(phase == CrabSlamPhase.Landed)
        {
            
            controller.switchState("FCIdle");
        }
    }

    public override string getStateName()
    {
        return "FCSlam";
    }
}
