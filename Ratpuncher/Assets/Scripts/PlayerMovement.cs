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

    [Tooltip("Time to remain midair after starting GP")]
    public float groundPoundDelay = 0.1f;
    float groundPoundTimer = 0f;
    bool isGroundPounding = false;

    [Tooltip("Transform of the object player sprite is on")]
    public Transform sprite;

    // Bottom of the sprite. Used to check ifGrounded
    public Transform bottom;
    Vector3 spriteScale;

    bool isGrounded;
    bool canDoubleJump;

    int iframes = 0;

    PlayerAnimationManager animationManager;
    PlayerParticles particleManager;

    // Start is called before the first frame update
    void Start()
    {
        gravity = rb.gravityScale;
        spriteScale = sprite.localScale;

        animationManager = GetComponent<PlayerAnimationManager>();
        particleManager = GetComponent<PlayerParticles>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currDashCooldown >= 0)
        {
            currDashCooldown -= Time.deltaTime;
        }

        if(currDashDuration >= 0)
        {
            currDashDuration -= Time.deltaTime;
            if(currDashDuration < 0)
            {
                animationManager.setDashing(false);
            }
        }

        if(iframes >= 0)
        {
            iframes--;
        }
    }

    void FixedUpdate()
    {
        checkIsGrounded();

        Vector2 currentVelocity = rb.velocity;

        // Limit maximum movement speed
        if(Mathf.Abs(moveVector.x) >= 0 && currDashDuration < 0)
        {
            currentVelocity.x += moveVector.x;
        }

        // Limit player speed if not dashing
        if(currDashDuration < 0 && Mathf.Abs(currentVelocity.x) > maxMoveSpeed)
        {
            currentVelocity.x = currentVelocity.x > 0 ? maxMoveSpeed : -maxMoveSpeed;
        }
        // If no movement input, add some "drag" to help slow the player
        if(Mathf.Abs(moveVector.x) <= 1)
        {
            animationManager.setRunning(false);
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

    public bool checkIsGrounded()
    {
        RaycastHit2D[] hits;
        Vector2 start = bottom.position;
        hits = Physics2D.RaycastAll(start, new Vector2(0, -1), 0.075f);

        // The raycast tends to hit the player itself, so want to "ignore" it
        if (hits.Length > 1)
        {
            isGrounded = true;
            canDoubleJump = true;
            animationManager.setVertical(Enums.VerticalState.Grounded);

            if (isGroundPounding)
            {
                // Landed after ground pound
                particleManager.spawnPoundParticles();
                isGroundPounding = false;
            }
        }
        else
        {
            isGrounded = false;

            if (rb.velocity.y > 0)
            {
                animationManager.setVertical(Enums.VerticalState.Jumping);
            }
            else
            {
                animationManager.setVertical(Enums.VerticalState.Falling);
            }
        }

        return isGrounded;
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        float x = inputVector.x;
        if(Mathf.Abs(x) > 0)
        {
            animationManager.setRunning(true);
            forward = new Vector2(Mathf.Sign(x), 0);
            Debug.Log(Mathf.Sign(x) * spriteScale.x);
            sprite.localScale = new Vector3(Mathf.Sign(x) * spriteScale.x, spriteScale.y, spriteScale.z);
            
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
        particleManager.spawnJumpParticles();
        Vector2 currVelocity = rb.velocity;
        currVelocity.y = jumpForce;
        rb.velocity = currVelocity;
    }

    void OnDash()
    {
        if(currDashCooldown <= 0 && isGroundPounding == false)
        {
            float direction = Mathf.Sign(forward.x);
            particleManager.spawnDashParticles(direction == -1);
            currDashCooldown = dashCooldown;
            Vector2 currVelocity = rb.velocity;
            currVelocity.x = dashForce * direction;
            currVelocity.y = 0;
            animationManager.setDashing(true);
            rb.velocity = currVelocity;

            currDashDuration = dashDuration;
        }
    }
     
    void OnPound()
    {
        if (!isGrounded && isGroundPounding == false)
        {
            rb.velocity = Vector2.zero;
            groundPoundTimer = groundPoundDelay;
            animationManager.setVertical(Enums.VerticalState.Pounding);
            isGroundPounding = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if(iframes <= 0 && hit.layer == 9)
        {
            iframes = 45;

            if(hit.transform.position.x <= transform.position.x)
            {
                rb.AddForce(new Vector2(25, 25), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-25, 25), ForceMode2D.Impulse);
            }
        }
    }

}
