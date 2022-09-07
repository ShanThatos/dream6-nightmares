using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WanderState : TimedState {

    public IdleState idleState;

    public Rigidbody2D rb;


    public float maxSpeed = 2f;
    Vector2 direction;

    public override void onEnter() {
        setTimedState(Random.Range(2f, 5f), idleState);
        direction = Random.Range(0, 2) == 0 ? Vector2.left : Vector2.right;
    }

    public override State run() {
        if (checkCanMove())
            rb.velocity = direction * maxSpeed;
        return base.run();
    }

    private bool checkCanMove() {
        float raycastDist = Mathf.Abs(1f * rb.transform.localScale.x);
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, raycastDist, LayerMask.GetMask("Platform"));
        if (groundCheck.collider == null)
            return false;
        
        var wallCheck = Physics2D.Raycast(transform.position, direction, raycastDist, LayerMask.GetMask("Platform"));
        if (wallCheck.collider != null) {
            direction *= -1;
            return false;
        }
        
        var forwardGroundCheck = Physics2D.Raycast(transform.position + (Vector3)direction * raycastDist, Vector2.down, raycastDist, LayerMask.GetMask("Platform"));
        if (forwardGroundCheck.collider == null) {
            direction *= -1;
            return false;
        }

        return true;
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.green;
        float raycastDist = Mathf.Abs(1f * rb.transform.localScale.x);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) Vector2.down * raycastDist);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) direction * raycastDist);
        Gizmos.DrawLine(transform.position + (Vector3) direction * raycastDist, transform.position + (Vector3) direction * raycastDist + (Vector3) Vector2.down * raycastDist);
    }

    public override string getStateName() { return "Wander"; }
}