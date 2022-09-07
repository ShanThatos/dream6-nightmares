using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoonController : StateManager {

    public bool isBoss = false;
    public float maxHealth = 25;
    float currentHealth;

    public ChaseState chaseState;
    public NothingState nothingState;

    protected override State checkForAnyState() {
        if (chaseState.shouldChasePlayer(currentState.getStateName()))
            return chaseState;
        return null;
    }


    private Rigidbody2D rb;
    
    public override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }


    public override void Update() {
        base.Update();
        if (currentState.getStateName() != "Nothing") {
            if (rb.velocity.x > 0) {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else if (rb.velocity.x < 0) {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        if (currentHealth <= 0) {
            if (isBoss)
                GameManager.instance.player.hasDashAbility = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        GameObject hit = other.gameObject;
        if (((1 << hit.layer) & LayerMask.GetMask("PlayerAttack")) != 0) {
            AttackCollider attackCollider = hit.GetComponent<AttackCollider>();
            if (attackCollider.canAttack()) {
                attackCollider.resetCooldown();
                switchState(nothingState);
                currentHealth -= attackCollider.attackDamage;
                float playerX = GameManager.getPlayerTransform().position.x;
                if (playerX < transform.position.x) {
                    rb.AddForce(new Vector2(10, 5), ForceMode2D.Impulse);
                } else {
                    rb.AddForce(new Vector2(-10, 5), ForceMode2D.Impulse);
                }
            }
        }
        // if (other.gameObject.tag == "Player") {
        //     other.gameObject.GetComponent<PlayerController>().takeDamage(1);
        // }
    }
}