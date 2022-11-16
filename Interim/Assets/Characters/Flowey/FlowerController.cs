using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damagable))]
public class FlowerController : MonoBehaviour
{
    public Vector2 interval;
    public GameObject projectile;
    public Transform launchPoint;
    public float range;

    Animator animator;
    Damagable damagable;
    float timer;
    bool isOpened;
    bool alreadyDead;

    private void Start()
    {
        damagable = GetComponent<Damagable>();
        animator = GetComponent<Animator>();
        damagable.OnDeath += Death;

        isOpened = false;
        timer = Random.Range(interval.x, interval.y);
    }

    private void Update()
    {
        if (!isOpened)
        {
            timer -= Time.deltaTime;
        }
        
        if(timer <= 0)
        {
            if(Vector3.Distance(GameManager.GetPlayerTransform().position, gameObject.transform.position) < range)
            {
                isOpened = true;
                animator.SetTrigger("Open");
                timer = Random.Range(interval.x, interval.y);
            }
            else
            {
                // Wait until player is in range
                timer = 1;
            }
        }
    }

    public void Shoot()
    {
        GameObject obj = Instantiate(projectile, launchPoint.transform.position, Quaternion.identity);

        Vector3 toPlayer = GameManager.GetPlayerTransform().position - gameObject.transform.position;
        toPlayer.Normalize();

        obj.transform.forward = toPlayer;
    }

    public void Close()
    {
        isOpened = false;
    }

    private void Death()
    {
        if (alreadyDead)
        {
            return;
        }   
        alreadyDead = true;
        animator.SetTrigger("Die");
    }

    public void DeathAniDone()
    {
        Destroy(gameObject, .1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
