using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseState : State {

    public IdleState idleState;
    public AttackState attackState;
    public float triggerDistance = 5f;
    public float stopDistance = 1.2f;
    public float chaseSpeed = 3f;

    public Rigidbody2D rb;


    private ArrayList preChaseStates = new ArrayList() { "Idle", "Wander" };
    public bool shouldChasePlayer(string currentStateName) {
        if (!preChaseStates.Contains(currentStateName))
            return false;
        Vector3 playerPosition = GameManager.getPlayerTransform().position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        return distanceToPlayer < triggerDistance;
    }

    public override State run() {
        Vector2 playerPosition = GameManager.getPlayerTransform().position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        Vector2 direction = playerPosition.x < transform.position.x ? Vector2.left : Vector2.right;

        if (distanceToPlayer > triggerDistance) 
            return idleState;

        if (distanceToPlayer < stopDistance) {
            rb.velocity = Vector2.zero;
            return attackState;
        }
        if (checkCanMove(direction)) 
            rb.velocity = direction * chaseSpeed;

        return this;
    }

    private bool checkCanMove(Vector2 direction) {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Platform"));
        if (groundCheck.collider == null)
            return false;
        
        var wallCheck = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Platform"));
        if (wallCheck.collider != null)
            return false;
        
        var forwardGroundCheck = Physics2D.Raycast(transform.position + (Vector3)direction, Vector2.down, 1f, LayerMask.GetMask("Platform"));
        if (forwardGroundCheck.collider == null) 
            return false;

        return true;
    }

    public override string getStateName() { return "Chase"; }
}