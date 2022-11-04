using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingDrill : MonoBehaviour
{

    [Tooltip("How long before launching attack")]
    public float delay;

    [Tooltip("Tracking speed")]
    public float speed;

    [Tooltip("Should this attack target the player?")]
    public bool targetPlayer = true;

    [Tooltip("GameObject to spawn on attack")]
    public GameObject attack;

    [Tooltip("GameObject to telegraph attack")]
    public GameObject readyParticles;

    Transform player;
    Vector3 target;
    Rigidbody2D rb;
    bool shouldTrack = false;
    bool hasAttacked = false;
    bool alreadyReady = false;
    float lifetime = 0;

    public delegate void ExecuteEvent();
    public ExecuteEvent OnExecute;
    public void CallOnExecute() => OnExecute?.Invoke();
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    public void manualSetTarget(Vector3 tgt)
    {
        targetPlayer = false;
        target = tgt;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAttacked)
        {
            return;
        }

        if (targetPlayer)
        {
            target = player.position;
        }


        if (shouldTrack)
        {
            if(target.x > transform.position.x)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }
        }

        if (!alreadyReady)
        {
            if (Mathf.Abs(target.x - transform.position.x) < 0.001)
            {
                shouldTrack = false;
            }
            else
            {
                shouldTrack = true;
            }
        }
        

        lifetime += Time.deltaTime;
        if(!alreadyReady && lifetime >= delay - .5)
        {
            StartCoroutine(PreAttack());
        }
    }

    private void executeAttck()
    {
        StopAllCoroutines();
        attack.SetActive(true);
        ParticleSystem temp = GetComponent<ParticleSystem>();
#pragma warning disable CS0618 // Type or member is obsolete
        temp.enableEmission = false;
#pragma warning restore CS0618 // Type or member is obsolete
        hasAttacked = true;

        CallOnExecute();

        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PreAttack());
        }   
    }

    IEnumerator PreAttack()
    {
        if (alreadyReady)
        {
            yield break;
        }

        rb.velocity = Vector2.zero;
        alreadyReady = true;
        shouldTrack = false;
        readyParticles.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        executeAttck();
    }
}
