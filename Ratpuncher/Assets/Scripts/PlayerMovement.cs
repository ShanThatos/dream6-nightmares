using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Enums;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Tooltip("Jump power")]
    public float jumpForce = 1;
    bool canDoubleJump;

    [Tooltip("Dash power")]
    public float dashForce = 2;
    [Tooltip("Cooldown time between dashes")]
    public float dashCooldown = 1.5f;
    float currDashCooldown = 1;
    [Tooltip("IFrame time after dashing")]
    public float dashDuration = .75f;
    public bool hasDashAbility = false;

    float currDashDuration = 0;

    [Tooltip("Normal movement acceleration")]
    public float moveAccel = 90;
    [Tooltip("Max speed of normal walking")]
    public float maxMoveSpeed = 5.5f;
    Vector2 moveVector;
    Vector2 forward = new Vector2(1, 0);

    [Tooltip("Time to remain midair after starting GP")]
    public float groundPoundDelay = 0.1f;
    float groundPoundTimer = 0f;
    public AudioSource groundPoundSound;

    [Tooltip("Transform of the object player sprite is on")]
    public Transform sprite;

    // Bottom of the sprite. Used to check ifGrounded
    public Transform bottom;
    Vector3 spriteScale;

    int iframes = 0;
    float prevY = float.MinValue;
    Vector2 inputVector = Vector2.zero;

    public VerticalState verticalState { get; private set; }
    public PlayerActions currentAction { get ; private set; }

    PlayerAnimationManager animationManager;
    PlayerParticles particleManager;
    PlayerCapabilities playerCapabilities;
    PlayerAttackManager attackManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteScale = sprite.localScale;

        animationManager = GetComponent<PlayerAnimationManager>();
        particleManager = GetComponent<PlayerParticles>();
        playerCapabilities = GetComponent<PlayerCapabilities>();
        attackManager = GetComponent<PlayerAttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            SceneManager.LoadScene("MainMenu");

        if (currDashCooldown >= 0)
        {
            currDashCooldown -= Time.deltaTime;
        }

        if (currDashDuration >= 0)
        {
            currDashDuration -= Time.deltaTime;
            if (currDashDuration < 0)
            {
                animationManager.setDashing(false);
                currentAction = PlayerActions.None;
            }
        }

        if (iframes >= 0)
        {
            iframes--;
        }

    }

    void FixedUpdate()
    {
        checkIsGrounded();

        Vector2 currentVelocity = rb.velocity;

        if (currentAction == PlayerActions.None)
        {
            if(Mathf.Abs(moveVector.x) >= 0)
            {
                currentVelocity.x += moveVector.x;
            }

            // Limit player max speed from normal move
            if(Mathf.Abs(currentVelocity.x) > maxMoveSpeed)
            {
                currentVelocity.x = currentVelocity.x > 0 ? maxMoveSpeed : -maxMoveSpeed;
            }
        }
        
        // If no movement input, add some "drag" to help slow the player
        if (Mathf.Abs(moveVector.x) <= .33)
        {
            animationManager.setRunning(false);
            currentVelocity.x = currentVelocity.x * .9f;
        }


        if (groundPoundTimer >= 0)
        {
            groundPoundTimer -= Time.deltaTime;

            if (groundPoundTimer < 0)
            {
                // Execute GP when "animation" is done
                currentVelocity.y = -2 * jumpForce;
            }
            else
            {
                // While player is ground pound "animation", they are stuck midair
                currentVelocity = Vector2.zero;
            }

        }
        else if(currentAction == PlayerActions.Pounding)
        {
            // Triggers in event the player gets stuck on
            // geometry while ground pounding
            if(Mathf.Approximately(transform.position.y, prevY))
            {
                // NOTE: This CANNOT be the only check since it
                // takes two physics ticks to trigger
                // (It will cause a delay in most cases)
                EndGroundPound();
            }

            prevY = transform.position.y;
        }

        


        rb.velocity = currentVelocity;
    }

    public bool checkIsGrounded()
    {
        RaycastHit2D[] hits;
        Vector2 start = bottom.position;
        hits = Physics2D.RaycastAll(start, new Vector2(0, -1), 0.075f, LayerMask.GetMask("Platform"));

        if (hits.Length > 0)
        {
            // Grounded
            verticalState = VerticalState.Grounded;
            canDoubleJump = true;

            if (currentAction == PlayerActions.Pounding)
            {
                EndGroundPound();
            }
            verticalState = VerticalState.Grounded;
            
        }
        else if (currentAction != PlayerActions.Pounding)
        {
            // Airborne
            if (rb.velocity.y > 0)
            {
                verticalState = VerticalState.Jumping;
            }
            else
            {
                verticalState = VerticalState.Falling;
            }
        }
        animationManager.setVertical(verticalState);

        return verticalState == VerticalState.Grounded;
    }

    private void EndGroundPound()
    {
        if(currentAction != PlayerActions.Pounding)
        {
            return;
        }

        if (groundPoundSound != null)
        {
            groundPoundSound.Play(0);
        }
        particleManager.spawnPoundParticles();
        currentAction = PlayerActions.None;
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        CalculateMovement();
    }

    void CalculateMovement()
    {
        if(currentAction == PlayerActions.Attacking && verticalState == VerticalState.Grounded)
        {
            moveVector = Vector2.zero;
            return;
        }

        float x = inputVector.x;
        if (Mathf.Abs(x) > 0)
        {
            animationManager.setRunning(true);
            forward = new Vector2(Mathf.Sign(x), 0);

            // May need to mirror sprite
            sprite.localScale = new Vector3(Mathf.Sign(x) * spriteScale.x, spriteScale.y, spriteScale.z);

        }

        moveVector = moveAccel * inputVector;
    }

    void OnJump()
    {
        if(currentAction != PlayerActions.None)
        {
            // Cannot jump while doing other things
            return;
        }

        if (verticalState == VerticalState.Grounded)
        {
            Jump();
        }
        else if (canDoubleJump && playerCapabilities.canDoubleJump)
        {

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
        if(currentAction == PlayerActions.Pounding)
        {
            // Cannot dash when performing ground pound
            return;
        }

        if (currDashCooldown <= 0 && playerCapabilities.canDash)
        {
            currentAction = PlayerActions.Dashing;

            float direction = Mathf.Sign(forward.x);
            Vector2 currVelocity = rb.velocity;
            currVelocity.x = dashForce * direction;
            currVelocity.y = 0;
            animationManager.setDashing(true);
            rb.velocity = currVelocity;

            // Spawn particles, which may need to be flipped
            particleManager.spawnDashParticles(direction == -1);

            currDashCooldown = dashCooldown;
            currDashDuration = dashDuration;
        }
    }

    void OnPound()
    {
        if (playerCapabilities.canGroundPound && currentAction != PlayerActions.Attacking)
        {
            if (verticalState == VerticalState.Falling || verticalState == VerticalState.Jumping)
            {
                currentAction = PlayerActions.Pounding;

                rb.velocity = Vector2.zero;
                groundPoundTimer = groundPoundDelay;
                animationManager.setVertical(VerticalState.Pounding);
            }
        }
        
    }

    void OnLightAttack(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        if(currentAction == PlayerActions.Pounding || currentAction == PlayerActions.Dashing)
        {
            return;
        }

        bool success = attackManager.executeAttack();
    }

    public void setAttackState(int comboStep)
    {
        if (comboStep > 0)
        {
            currentAction = PlayerActions.Attacking;

            if(verticalState == VerticalState.Grounded)
            {
                rb.AddForce(forward * 5, ForceMode2D.Impulse);
            }
        }
        else
        {
            currentAction = PlayerActions.None;
        }

        animationManager.setAttackCombo(comboStep);

        // May need to stop player movement if attacking
        CalculateMovement();

    }

    void OnBlock(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Block started");
        }
        else
        {
            Debug.Log("Block Ended");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if (iframes <= 0 && ((1 << hit.layer) & LayerMask.GetMask("EnemyAttack")) != 0)
        {
            iframes = 45;

            AttackCollider ac = hit.GetComponent<AttackCollider>();
            if (ac.canAttack())
            {
                ac.resetCooldown();
                if (hit.transform.position.x <= transform.position.x)
                {
                    rb.AddForce(new Vector2(40, 5) * ac.attackKnockback, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector2(-40, 5) * ac.attackKnockback, ForceMode2D.Impulse);
                }
            }
        }
    }
}
