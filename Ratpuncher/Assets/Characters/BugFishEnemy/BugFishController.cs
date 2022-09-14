using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BugFishController : StateManager {

    public Rigidbody2D rb;

    public Animator animator;

    public BugFishAnimationCues cues;

    public const float MAX_HEALTH = 100;
    public float currentHealth;

    public Vector2 fishBaseKnockback;

    Dictionary<string, Transform> points = new Dictionary<string, Transform>();


    public float idleSpeed = 4f;
    public float flopUpForce = 50f;
    public float flopUpSideForce = 10f;
    public float flopDownForce = 10f;

    public float MIN_X = -10f;
    public float MAX_X = 10f;

    public override void init()
    {
        base.init();
        currentHealth = MAX_HEALTH;
    }

    public Transform getPoint(string pointName) {
        if (!points.ContainsKey(pointName))
            points.Add(pointName, transform.Find("Points").Find(pointName));
        return points[pointName];
    }

    public void setDirection(bool isFacingRight) {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (isFacingRight ? -1 : 1), transform.localScale.y, transform.localScale.z);
    }

    public override void run() {
        base.run();
        // Debug.Log(currentState.getStateName());
        if (currentHealth < 0) {
            Destroy(gameObject);
            return;
        }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_X, MAX_X), transform.position.y, transform.position.z);
    }

    void OnTriggerStay2D(Collider2D collision) {
        GameObject hit = collision.gameObject;
        if (hit.layer == LayerMask.NameToLayer("PlayerAttack")) {
            AttackCollider ac = hit.GetComponent<AttackCollider>();
            if (ac.canAttack())
            {
                ac.resetCooldown();
                Vector2 knockback = fishBaseKnockback * ac.attackKnockback;
                knockback.x *= (hit.transform.position.x <= transform.position.x ? 1 : 1);
                rb.AddForce(knockback, ForceMode2D.Impulse);
                currentHealth -= ac.attackDamage;
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(MIN_X, -100, 0), new Vector3(MIN_X, 100, 0));
        Gizmos.DrawLine(new Vector3(MAX_X, -100, 0), new Vector3(MAX_X, 100, 0));
    }
}

