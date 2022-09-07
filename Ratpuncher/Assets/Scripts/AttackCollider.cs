using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttackCollider : MonoBehaviour {

    public bool isAttacking = false;
    public float attackDamage;
    float attackCooldown = 0f;
    public float attackCooldownTime = 1f;

    public float attackKnockback = 1f;

    void Update() {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }

    public bool canAttack() {
        return isAttacking && attackCooldown <= 0;
    }

    public void resetCooldown() {
        attackCooldown = attackCooldownTime;
    }
}