using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [Tooltip("Object for regular attack")]
    public GameObject normalAttack;

    [Tooltip("Object for charged attack")]
    public GameObject chargedAttack;

    [Tooltip("Object for charged attack ready indicator")]
    public GameObject chargedAttackIndicator;

    [Tooltip("Force to launch charged attack projectile")]
    public float chargedForce;

    [Tooltip("Where the attack is spawned")]
    public Transform attackPoint;

    [Tooltip("Where charged ready indicator is spawned")]
    public Transform readyPoint;

    [Tooltip("Player sprite (used to figure out direction)")]
    public Transform sprite;

    [Tooltip("Allow queueing of attacks")]
    public bool allowAttackQueueing;

    bool attackQueued = false;
    bool isAnimLocked;
    bool chargedAttackReady;
    PlayerMovement playerMovement;
    PlayerAnimationManager animationManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        animationManager = gameObject.GetComponent<PlayerAnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginCharge()
    {
        animationManager.setAttacking(true);
    }

    public bool executeAttack()
    {
        animationManager.setAttacking(true);

        if(isAnimLocked)
        {
            attackQueued = allowAttackQueueing;
            Debug.Log("Attack queued");
            return false;
        }

        isAnimLocked = true;

        attackQueued = false;
        playerMovement.setAttackState(1);
        animationManager.setNextAttack();
        return true;
    }

    public void endAnimLock()
    {
        isAnimLocked = false;

        if (playerMovement.MovementAnimationCancel())
        {
            return;
        }

        if (attackQueued)
        { 
            executeAttack();
        }

    }

    public void EndAttack()
    {
        endAnimLock();
        animationManager.setAttacking(false);
        playerMovement.setAttackState(0);
        chargedAttackReady = false;
    }

    // Executed via animation callback
    public void SpawnAttack()
    {
        if (chargedAttackReady)
        {
            SpawnChargedAttack();
        }
        else
        {
            SpawnNormalAttack();
        }

        chargedAttackReady = false;
    }

    public void SpawnNormalAttack()
    {
        GameObject attack = Instantiate(normalAttack, attackPoint.position, Quaternion.identity);
        attack.transform.parent = gameObject.transform;
        attack.GetComponent<AttackHitbox>().player = playerMovement;
        if (Mathf.Sign(sprite.localScale.x) == -1)
        {
            attack.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        }
    }

    public void SpawnChargedAttack()
    {
        GameObject attack = Instantiate(chargedAttack, attackPoint.position, Quaternion.identity);
        if (Mathf.Sign(sprite.localScale.x) == -1)
        {
            attack.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        }
        attack.GetComponent<Rigidbody2D>().
            AddForce(new Vector2(Mathf.Sign(sprite.localScale.x) * chargedForce, 0), ForceMode2D.Impulse);
    }

    // Attempt to end attacking early
    // Returns if successful or not
    public bool TryAnimationCancel()
    {
        if (isAnimLocked)
        {
            return false;
        }

        attackQueued = false;
        chargedAttackReady = false;
        animationManager.setAttacking(false);
        animationManager.setAnimationCancel();
        playerMovement.setAttackState(0);
        return true;
    }

    // Executed via animation callback
    public void ChargedAttackReady()
    {
        if(chargedAttackReady == false)
        {
            Instantiate(chargedAttackIndicator, readyPoint.transform.position, Quaternion.identity);
        }
        chargedAttackReady = true;
    }
}
