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
    [Tooltip("Damage resistance (1 = invunerable)")]
    [Range(0f, 1f)]
    public float damageReduction = 0.0f;

    [SerializeField]
    [Tooltip("Damage resistance (1 = invunerable)")]
    [Range(0f, 1f)]
    public float energyDamageReduction = 0.0f;

    [SerializeField]
    [Tooltip("How long entity is invincible in seconds after damage")]
    private float iFrameTime = 0.4f;

    [SerializeField]
    [Tooltip("Is this the player or not?")]
    private bool isPlayer = false;

    [SerializeField]
    [Tooltip("Spawn particles on hit?")]
    private bool spawnHitParticles = true;

    public bool limitEvents = false;
    private bool alreadyFired = false;

    private float health;
    private LayerMask targetLayer;
    private bool iFrame = false;
    private Rigidbody2D rb;
    private static GameObject particles;
    private static GameObject deflectParticles;
    private PlayerMovement player;

    public delegate void DeathEvent();
    public event DeathEvent OnDeath;
    public void CallOnDeath() => OnDeath?.Invoke();

    public delegate void HurtEvent(float damage, bool isEnergy = false);
    public event HurtEvent OnHurt;
    public void CallOnHurt(float damage, bool isEnergy) => OnHurt?.Invoke(damage, isEnergy);

    public delegate void RespawnEvent();
    public event RespawnEvent OnRespawn;
    public void CallOnRespawn() => OnRespawn?.Invoke();

    // Start is called before the first frame update
    void Start()
    {
        particles = Resources.Load<GameObject>("HitParticles");
        deflectParticles = Resources.Load<GameObject>("DeflectParticles");

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
                    float dr = attack.energyAttack ? energyDamageReduction : damageReduction;

                    float effectiveDamage = (attack.damage * (1 - dr) );
                    health -= effectiveDamage;

                    GameObject hitFX = particles;
                    float recoilMult = 1.0f;

                    if (dr >= .8f)
                    {
                        hitFX = deflectParticles;
                        recoilMult = 2.0f;
                    }

                    Debug.Log("Health left: " + health);

                    // See CallOnDeath on how to subscribe to this event
                    // Useful if you need custom behavior on hit (health bars, etc.)
                    if (health > 0)
                        CallOnHurt(effectiveDamage, attack.energyAttack);

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
                        GameObject spawned = Instantiate(hitFX, hit.transform.position, Quaternion.identity);   
                        if(flip)
                        {
                            spawned.transform.rotation = new Quaternion(0, 0, 180, 0);
                            spawned.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
                        }
                    }

                    if (health <= 0)
                    {
                        if(!limitEvents || !alreadyFired)
                        {
                            CallOnDeath();
                        }
                        alreadyFired = true;

                        // Subscribe your controller class to OnDeath()
                        // Example:
                        // Damagable damage = GetComponent<Damagable>();
                        // damage.OnDeath += OnDead;
                    }

                    attack.TryApplyRecoil(recoilMult);

                    iFrame = true;
                    StartCoroutine(DisableIFrames());
                }
            }
            else
            {
                
            }
        }
    }

    IEnumerator DisableIFrames()
    {
        yield return new WaitForSecondsRealtime(iFrameTime);

        iFrame = false;

        yield return null;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }
    public float GetHealth() {
        return health;
    }

    public float GetHealthPercent()
    {
        return health / maxHealth;
    }
    public void Respawn()
    {
        health = maxHealth;
        CallOnRespawn();
    }

    public void setInvincibility(bool invincible) {
        if (invincible)
            StopAllCoroutines();
        iFrame = invincible;
    }
}
