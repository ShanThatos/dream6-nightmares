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

    // Index of next attack in the array to use
    int comboStage;

    bool attackQueued = false;
    bool isAnimLocked;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        comboStage = 0;
        playerMovement = gameObject.GetComponent<PlayerMovement>(); 
    }

    // Update is called once per frame
    void Update()
    {

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
