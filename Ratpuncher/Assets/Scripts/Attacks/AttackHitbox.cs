using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHitbox : MonoBehaviour
{
    [Header("Ensure layer is set to PlayerAttack or EnemyAttack! \n")]
    [SerializeField]
    [Tooltip("Damage dealt by attack")]
    public float damage = 10;

    [SerializeField]
    [Tooltip("Knockback power of attack, in X and Y respectively")]
    public Vector2 knockback;

    [SerializeField]
    [Tooltip("If attack belongs to player")]
    public bool isPlayerAttack;

    [SerializeField]
    [Tooltip("If attack will yeet player or not")]
    public bool canLaunchPlayer;

    [HideInInspector]
    // Used to give the player some recoil
    public PlayerMovement player;

    [SerializeField]
    [Tooltip("Is this hitbox active. True for most cases")]
    public bool isActive = true;

    private List<Damagable> alreadyHit = new List<Damagable>();

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryApplyRecoil()
    {
        if (isPlayerAttack && player)
        {
            player.AttackRecoil();
        }
    }

    public bool canHit(Damagable target)
    {
        if (!isPlayerAttack)
        {
            return true;
        }

        if (!alreadyHit.Contains(target))
        {
            alreadyHit.Add(target);
            return true;
        }

        return false;
    }

    public bool shouldFlip(Transform target)
    {
        if (player)
        {
            return !player.isFacingRight();
        }

        return transform.position.x >= target.position.x;

    }
}
