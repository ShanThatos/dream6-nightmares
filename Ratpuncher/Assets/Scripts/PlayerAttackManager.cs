using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [Tooltip("Animation for each attack in combo sequence")]
    public GameObject[] attacks;

    [Tooltip("Where the attack is spawned")]
    public Transform attackPoint;

    [Tooltip("Player sprite (used to figure out direction)")]
    public Transform sprite;

    [Tooltip("Allow queueing of attacks")]
    public bool allowAttackQueueing;

    // Index of next attack in the array to use
    int comboStage;

    bool attackQueued = false;
    bool isAnimLocked;
    PlayerMovement playerMovement;
    PlayerAnimationManager animationManager;

    // Start is called before the first frame update
    void Start()
    {
        comboStage = 0;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        animationManager = gameObject.GetComponent<PlayerAnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool executeAttack()
    {
        comboStage = comboStage % attacks.Length;
        animationManager.setAttacking(true);

        if(isAnimLocked)
        {
            if(comboStage < 3)
            {
                attackQueued = allowAttackQueueing;
                Debug.Log("Attack queued");
            }
            
            return false;
        }

         GameObject attack = Instantiate(attacks[comboStage], attackPoint.position, Quaternion.identity);
        if(Mathf.Sign(sprite.localScale.x) == -1)
        {
            attack.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        } 

        isAnimLocked = true;

        comboStage++;
        attackQueued = false;
        playerMovement.setAttackState(1);
        animationManager.setNextAttack();
        return true;
    }

    public void endAnimLock(int finishedStage)
    {
        isAnimLocked = false;
        if (attackQueued)
        { 
            executeAttack();
        }
    }

    public void EndAttack()
    {
        animationManager.setAttacking(false);
        playerMovement.setAttackState(0);
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
        animationManager.setAttacking(false);
        animationManager.setAnimationCancel();
        playerMovement.setAttackState(0);
        return true;
    }
}
