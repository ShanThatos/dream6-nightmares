using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatEnemyController : StateManager {

    public Rigidbody2D rb;

    public Animator animator;

    public const float MAX_HEALTH = 30;
    public float currentHealth;

    public Vector2 ratBaseKnockback;

    Dictionary<string, Transform> points = new Dictionary<string, Transform>();


    public float speed = 3f;
    public bool canJumpAttack = true;

    public Transform getPoint(string pointName) {
        if (!points.ContainsKey(pointName))
            points.Add(pointName, transform.Find("Points").Find(pointName));
        return points[pointName];
    }

    public void setDirection(bool isFacingRight) {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (isFacingRight ? -1 : 1), transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerStay2D(Collider2D collision) {
        GameObject hit = collision.gameObject;
        if (hit.layer == LayerMask.NameToLayer("PlayerAttack")) {
            AttackCollider ac = hit.GetComponent<AttackCollider>();
            if (ac.canAttack())
            {
                ac.resetCooldown();
                Vector2 knockback = ratBaseKnockback * ac.attackKnockback;
                knockback.x *= (hit.transform.position.x <= transform.position.x ? 1 : 1);
                rb.AddForce(knockback, ForceMode2D.Impulse);
                currentHealth -= ac.attackDamage;
                switchState("RatHurt");
            }
        }
    }
}

