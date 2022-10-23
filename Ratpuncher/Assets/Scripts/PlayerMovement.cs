using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Enums;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Info")]
    public Rigidbody2D rb;

    [Tooltip("Transform of the object player sprite is on")]
    public Transform sprite;

    [Tooltip("Transform placed at bottom of sprite")]
    public Transform bottom;
    [Space(6)]


    [Header("Horizontal Movement")]
    [Tooltip("Normal movement acceleration")]
    public float moveAccel = 90;

    [Tooltip("Max speed while grounded")]
    public float maxGroundMoveSpeed = 5.5f;

    [Tooltip("Max speed while airborne")]
    public float maxAirMoveSpeed = 3.3f;

    [Tooltip("Max speed after being launched (Note: cannot control movement after launch)")]
    public float maxLaunchSpeed = 12f;

    [Tooltip("Max speed while charging")]
    public float maxChargeMoveSpeed = .5f;

    private float currentMaxMoveSpeed;

    [Tooltip("Recoil after hitting something")]
    public Vector2 attackRecoil;

    Vector2 moveVector;
    Vector2 forward = new Vector2(1, 0);
    [Space(6)]


    [Header("Vertical movement")]
    [Tooltip("Jump power")]
    public float jumpForce = 1;
    bool canDoubleJump;

    [Tooltip("Time to remain midair after starting GP")]
    public float groundPoundDelay = 0.1f;
    float groundPoundTimer = 0f;
    public AudioSource groundPoundSound;

    [Space(6)]

    [Header("Dashing")]
    [Tooltip("Dash power")]
    public float dashForce = 2;
    [Tooltip("Cooldown time between dashes")]
    public float dashCooldown = 1.5f;
    float currDashCooldown = 1;
    [Tooltip("IFrame time after dashing")]
    public float dashDuration = .75f;
    public bool hasDashAbility = false;

    float currDashDuration = 0;


    public AudioSource hurtSound;
    public AudioSource deathSound;
    bool isRespawning = false;


    Vector3 spriteScale;
    int iframes = 0;
    Vector3 prevPos;
    Vector2 inputVector = Vector2.zero;
    private bool canRecoverFromLaunch = true;

    public VerticalState verticalState { get; private set; }
    public PlayerActions currentAction { get; private set; }

    PlayerAnimationManager animationManager;
    PlayerParticles particleManager;
    PlayerCapabilities playerCapabilities;
    PlayerAttackManager attackManager;
    Damagable damage;

    // Start is called before the first frame update
    void Start()
    {
        spriteScale = sprite.localScale;
        currentMaxMoveSpeed = maxGroundMoveSpeed;

        animationManager = GetComponent<PlayerAnimationManager>();
        particleManager = GetComponent<PlayerParticles>();
        playerCapabilities = GetComponent<PlayerCapabilities>();
        attackManager = GetComponent<PlayerAttackManager>();
        damage = GetComponent<Damagable>();

        Debug.Log("MaxHP: " + damage.GetMaxHealth());

        damage.OnHurt += OnHurt;
        damage.OnDeath += OnDeath;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

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
                damage.setInvincibility(false);
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


        // Apply movement, and limit max speed if nessecary
        if (verticalState != VerticalState.Launched && currentAction != PlayerActions.Dashing)
        {
            currentVelocity.x += moveVector.x;

            if (Mathf.Abs(currentVelocity.x) > currentMaxMoveSpeed)
            {
                currentVelocity.x = currentVelocity.x > 0 ? currentMaxMoveSpeed : -currentMaxMoveSpeed;
            }
        }

        // If no movement input, add some "drag" to help slow the player
        if (Mathf.Abs(moveVector.x) <= .33 && verticalState != VerticalState.Launched)
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
        else if (currentAction == PlayerActions.Pounding && Mathf.Approximately(transform.position.y, prevPos.y))
        {
            // NOTE: This CANNOT be the only check since it
            // takes two physics ticks to trigger
            // (It will cause a delay in most cases)
            EndGroundPound();
        }
        else if(verticalState == VerticalState.Launched && Vector3.Distance(prevPos, transform.position) < 0.005)
        {
            verticalState = VerticalState.Falling;
        }



        // Negate vertical movement when dashing
        if(currentAction == PlayerActions.Dashing)
        {
            currentVelocity.y = 0;
        }

        prevPos = transform.position;
        rb.velocity = currentVelocity;
    }

    private bool checkIsGrounded()
    {
        RaycastHit2D[] hits;
        Vector2 start = bottom.position;
        hits = Physics2D.RaycastAll(start, new Vector2(0, -1), 0.095f, LayerMask.GetMask("Platform"));

        if (hits.Length > 0 && canRecoverFromLaunch)
        {
            // Grounded
            canDoubleJump = true;

            if (currentAction == PlayerActions.Pounding)
            {
                EndGroundPound();
            }

            currentMaxMoveSpeed = maxGroundMoveSpeed;

            if(currentAction == PlayerActions.Charging)
            {
                // If charging and grounded, drastically slow player
                currentMaxMoveSpeed = maxChargeMoveSpeed;
            }

            verticalState = VerticalState.Grounded;
        }
        else if (verticalState == VerticalState.Launched)
        {
            ;
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

            currentMaxMoveSpeed = maxAirMoveSpeed;
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
        if (currentAction == PlayerActions.Attacking && verticalState == VerticalState.Grounded)
        {
            if (!attackManager.TryAnimationCancel())
            {

            }
        }

        if (verticalState == VerticalState.Pounding || verticalState == VerticalState.Launched)
        {
            moveVector = Vector2.zero;
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

    public bool MovementAnimationCancel()
    {
        if(inputVector.magnitude > 0.2)
        {
            CalculateMovement();
            return true;
        }

        return false;
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
        if(currentAction == PlayerActions.Pounding 
            || verticalState == VerticalState.Launched
            || currentAction == PlayerActions.Charging)
        {
            // Cannot dash when performing ground pound or launched
            return;
        }

        if(currentAction == PlayerActions.Attacking && !attackManager.TryAnimationCancel())
        {
            // Try to cancel attack anim
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
            damage.setInvincibility(true);
        }
    }

    void OnPound()
    {
        if (playerCapabilities.canGroundPound 
            && currentAction != PlayerActions.Attacking
            && currentAction != PlayerActions.Charging
            && currentAction != PlayerActions.Pounding)
        {
            if (verticalState == VerticalState.Falling || verticalState == VerticalState.Jumping)
            {
                currentAction = PlayerActions.Pounding;

                rb.velocity = Vector2.zero;
                groundPoundTimer = groundPoundDelay;
                animationManager.setVertical(VerticalState.Pounding);
                CalculateMovement();
            }
        }
        
    }

    void OnAttack(InputValue value)
    {
        
        Debug.Log(value);

        float floatVal = 0;
        if(value.Get() != null)
        {
            floatVal = (float)value.Get();
        }

        if (currentAction == PlayerActions.Pounding ||
            currentAction == PlayerActions.Dashing ||
            verticalState == VerticalState.Launched)
        {
            return;
        }

        if (floatVal > .05 || value.isPressed)
        {
            if(currentAction != PlayerActions.Charging)
            {
                attackManager.BeginCharge();
                currentAction = PlayerActions.Charging;
            }
            return;
        }
        else
        {
            // pass 
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

        // May need to stop player movement if attacking
        CalculateMovement();

    }

    public void AttackRecoil(float multiplier = 1.0f)
    {
        Vector2 recoil = attackRecoil;
        recoil.x = forward.x > 0 ? -recoil.x : -recoil.x;

        recoil *= multiplier;

        rb.AddForce(recoil, ForceMode2D.Impulse);
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

    public void OnLaunch(Vector2 force, bool launch)
    {
        if (isRespawning)
        {
            return;
        }

        if (verticalState == VerticalState.Pounding)
        {
            // Negate all knockback if ground pounding
            return;
        }

        rb.velocity = Vector2.zero;

        rb.AddForce(force, ForceMode2D.Impulse);

        if (launch)
        {
  
            canRecoverFromLaunch = false;
            StartCoroutine(AllowLaunchRecovery());
            verticalState = VerticalState.Launched;
            animationManager.setLaunched(true);
            attackManager.EndAttack();
        }

        CalculateMovement();
    }

    public bool isFacingRight()
    {
        return forward.x >= 0;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
       
    }

    IEnumerator AllowLaunchRecovery()
    {
        yield return new WaitForSecondsRealtime(.2f);
        canRecoverFromLaunch = true;
        yield return null;
    }

    public void OnHurt(float damage, bool isEnergyHit)
    {
        if (isRespawning)
        {
            return;
        }

        particleManager.spawnHitParticles(isEnergyHit);

        string type = isEnergyHit ? "energy" : "physical";
        Debug.Log("Player took " + damage + " " + type + " damage");
        if(hurtSound && !hurtSound.isPlaying)
        {
            hurtSound.Play();
        }
    }

    public void OnDeath()
    {
        if (isRespawning)
        {
            return;
        }

        if (deathSound)
        {
            deathSound.Play();
        }

        Debug.Log("You died D:");
        GetComponent<PlayerInput>().enabled = false;
        moveVector = Vector2.zero;
        damage.setInvincibility(true);
        animationManager.setDeath();

        isRespawning = true;
    }

    public void OnDeathAnimDone()
    {
        Debug.Log("Respawning");
        GameManager.RespawnPlayer();
        animationManager.setRespawn();
    }

    public void OnRespawnAnimDone()
    {
        Debug.Log("Respawned");
        damage.Respawn();
        damage.setInvincibility(false);
        GetComponent<PlayerInput>().enabled = true;
        isRespawning = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(bottom.position, (Vector2) bottom.position + Vector2.down * 0.095f);
    }
}
