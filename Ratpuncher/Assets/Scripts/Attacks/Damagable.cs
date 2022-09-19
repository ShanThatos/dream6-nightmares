using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damagable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Maximum HP of entity")]
    private float maxHealth = 10;

    [SerializeField]
    [Tooltip("How resistant this entity is to knockback in the X and Y directions")]
    private Vector2 knockResistance;

    [SerializeField]
    [Tooltip("How long entity is invincible in seconds after damage")]
    private float iFrameTime = 0.4f;

    [SerializeField]
    [Tooltip("Is this the player or not?")]
    private bool isPlayer = false;

    [SerializeField]
    [Tooltip("Spawn particles on hit?")]
    private bool spawnHitParticles = true;

    private float health;
    private LayerMask targetLayer;
    private bool iFrame = false;
    private Rigidbody2D rb;
    private static GameObject particles;
    private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        particles = Resources.Load<GameObject>("HitParticles");

        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        if (isPlayer)
        {
            targetLayer = LayerMask.NameToLayer("EnemyAttack");
            player = GetComponent<PlayerMovement>();
        }
        else
        {
            targetLayer = LayerMask.NameToLayer("PlayerAttack");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if(!iFrame && hit.layer == targetLayer)
        {
            if(hit.TryGetComponent<AttackHitbox>(out AttackHitbox attack))
            {
                if (attack.canHit(this))
                {
                    health -= attack.damage;

                    Vector2 kb = attack.knockback - knockResistance;

                    kb.x = kb.x < 0 ? 0 : kb.x;
                    kb.y = kb.y < 0 ? 0 : kb.y;
                    bool flip = attack.shouldFlip(transform);

                    if (flip)
                    {
                        kb.x *= -1;
                    }

                    if (!isPlayer && rb)
                    {
                        rb.AddForce(kb, ForceMode2D.Impulse);
                    }
                    else if (isPlayer)
                    {
                        // If player, have player movement handle it
                        player.OnLaunch(kb, attack.canLaunchPlayer);
                    }

                    if (spawnHitParticles)
                    {
                        GameObject spawned = Instantiate(particles, hit.transform.position, Quaternion.identity);
                        if(flip)
                        {
                            spawned.transform.rotation = new Quaternion(0, 0, 180, 0);
                        }
                    }

                    if (health <= 0)
                    {
                        RIP();
                    }

                    attack.TryApplyRecoil();

                    iFrame = true;
                    StartCoroutine(DisableIFrames());
                }
            }
            else
            {
                Debug.LogWarning("Expected an AttackHitbox component");
            }
        }
    }

    private void RIP()
    {
        // Put a method called OnDead containing death behavior of
        // into your controller script
        BroadcastMessage("OnDead");
    }

     IEnumerator DisableIFrames()
    {
        yield return new WaitForSecondsRealtime(iFrameTime);

        iFrame = false;

        yield return null;
    }
}
