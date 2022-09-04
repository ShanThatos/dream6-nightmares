using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Tooltip("Jump power")]
    public float jumpForce = 1;

    [Tooltip("Dash power")]
    public float dashForce = 2;
    [Tooltip("Cooldown time between dashes")]
    public float dashCooldown = 1.5f;
    float currDashCooldown = 0;
    [Tooltip("IFrame time after dashing")]
    public float dashDuration = .75f;
    float currDashDuration = 0;
    float gravity;

    [Tooltip("Normal movement acceleration")]
    public float moveAccel = 90;
    [Tooltip("Max speed of normal walking")]
    public float maxMoveSpeed = 5.5f;
    Vector2 moveVector;
    Vector2 forward = new Vector2(1, 0);

    public float groundPoundDelay = 0.1f;
    float groundPoundTimer = 0f;

    // Bottom of the sprite. Used to check ifGrounded
    public Transform bottom;

    bool isGrounded;
    bool canDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        gravity = rb.gravityScale;
        Debug.Log(gravity);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits;
        Vector2 start = bottom.position;
        hits = Physics2D.RaycastAll(start, new Vector2(0, -1), 0.075f);

        // The raycast tends to hit the player itself, so want to "ignore" it
        if (hits.Length > 1)
        {
            isGrounded = true;
            canDoubleJump = true;
        }
        else
        {
            isGrounded = false;
        }

        if (currDashCooldown >= 0)
        {
            currDashCooldown -= Time.deltaTime;
        }

        if(currDashDuration >= 0)
        {
            currDashDuration -= Time.deltaTime;
            if(currDashDuration < 0)
            {
                rb.gravityScale = gravity;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 currentVelocity = rb.velocity;

        // Limit maximum movement speed
        if(Mathf.Abs(currentVelocity.x) <= maxMoveSpeed && currDashDuration < 0)
        {
            rb.AddForce(moveVector);
        }

        // Slow player after a dash
        if(currDashDuration < 0 && Mathf.Abs(currentVelocity.x) > maxMoveSpeed)
        {
            currentVelocity.x = currentVelocity.x > 0 ? maxMoveSpeed : -maxMoveSpeed;
        }
        // If no movement input, add some "drag" to help slow the player
        if(Mathf.Abs(moveVector.x) <= 1)
        {
            currentVelocity.x = currentVelocity.x * .9f;
        }

        
        if(groundPoundTimer >= 0)
        { 
            groundPoundTimer -= Time.deltaTime;

            if (groundPoundTimer < 0)
            {
                // Execute GP when "animation" is done
                Debug.Log("GP");
                currentVelocity.y = -2 * jumpForce;
            }
            else
            {
                // While player is ground pound "animation", they are stuck midair
                currentVelocity = Vector2.zero;
            }

        }

        rb.velocity = currentVelocity;
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        if(inputVector.x > 0)
        {
            forward = new Vector2(1, 0);
        }
        else if (inputVector.x < 0)
        {
            forward = new Vector2(-1, 0);
        }


        moveVector = moveAccel * inputVector;
    }

    void OnJump()
    {
        if (isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump && currDashDuration < 0){

            canDoubleJump = false;
            Jump();
        }
        
    }

    void Jump()
    {
        Vector2 currVelocity = rb.velocity;
        currVelocity.y = jumpForce;
        rb.velocity = currVelocity;
    }

    void OnDash()
    {
        if(currDashCooldown <= 0)
        {
            currDashCooldown = dashCooldown;
            Vector2 currVelocity = rb.velocity;
            currVelocity.x = dashForce * forward.x;
            currVelocity.y = 0;
            // rb.gravityScale = 0;
            rb.velocity = currVelocity;

            currDashDuration = dashDuration;
        }
    }
     
    void OnPound()
    {
        if (!isGrounded && groundPoundTimer < 0)
        {
            rb.velocity = Vector2.zero;
            groundPoundTimer = groundPoundDelay;
        }
    }

}
