using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTurretController : MonoBehaviour
{

    public float attackRange = 20.0f;
    public float attackInterval = 6.0f;
    float currentInterval;
    bool isCasting = false;

    public GameObject projectile;

    public GameObject attackPoint;

    Animator animator;
    GameObject player;
    Vector3 scale;
    ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentInterval = attackInterval;
        scale = transform.localScale;
        particles = attackPoint.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCasting)
        {
            currentInterval -= Time.deltaTime;
        }
        
        if(currentInterval <= 0)
        {
            BeginCast();
        }
    }

    public void BeginCast()
    {
        if(Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            return;
        }

        checkFlip();
        particles.Play();

        animator.SetTrigger("Casting");
        isCasting = true;
        currentInterval = attackInterval;
    }

    public void LaunchAttack()
    {
        isCasting=false;
        Debug.Log("Pew pew!");
        checkFlip();
        particles.Stop();

        GameObject proj = Instantiate(projectile, attackPoint.transform.position, Quaternion.identity);
        proj.GetComponent<BirdProjectile>().init(player.transform.position);
    }


    void checkFlip()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
        }
        else
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawSphere(attackPoint.transform.position, .05f);
    }

}