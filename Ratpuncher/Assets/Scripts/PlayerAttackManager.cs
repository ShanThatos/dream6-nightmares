using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [Tooltip("Animation for each attack in combo sequence")]
    public GameObject[] attacks;

    [Tooltip("Animation lock time for each attack")]
    public float[] animLockTimes;

    [Tooltip("Time between attacks")]
    public float attackDelay = .2f;

    [Tooltip("Time before combo resets")]
    public float comboTime = 0.65f;

    [Tooltip("Where the attack is spawned")]
    public Transform attackPoint;

    [Tooltip("Allow attack queueing (button mashing vs rhythm")]
    public bool queueAttacks;

    [Tooltip("Player sprite (used to figure out direction)")]
    public Transform sprite;

    // Index of next attack in the array to use
    int comboStage;

    float currComboTime;
    float currAnimLock;
    float currAttackDelay;
    bool attackQueued = false;
    bool isAnimLocked;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        comboStage = 0;
        currComboTime = 0;
        currAnimLock = 0;
        playerMovement = gameObject.GetComponent<PlayerMovement>(); 

        if(attacks.Length != animLockTimes.Length)
        {
            Debug.LogError("Ensure that each attack has an animation lock time!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* if(currComboTime >= 0)
        {
            currComboTime -= Time.deltaTime;

            if(currComboTime < 0)
            {
                comboStage = 0;
                playerMovement.setAttackState(0);
            }
        }

        if(currAnimLock >= 0)
        {
            currAnimLock -= Time.deltaTime;
            if(currAnimLock < 0)
            {
                
            }
        }

        if(currAttackDelay >= 0)
        {
            currAttackDelay -= Time.deltaTime;
            if (attackQueued && currAttackDelay < 0)
            {
                executeAttack();
            }
        } */
    }

    public bool executeAttack()
    {
        comboStage = comboStage % attacks.Length;

        if(isAnimLocked)
        {
            if(comboStage < 3)
            {
                attackQueued = true;
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
        playerMovement.setAttackState(comboStage);
        return true;
    }

    public void endAttackAnim(int finishedStage)
    {
        isAnimLocked = false;
        if (attackQueued && finishedStage < 3)
        {
            executeAttack();
        }
        else
        {
            comboStage = 0;
            playerMovement.setAttackState(0);
        }
    }
}
