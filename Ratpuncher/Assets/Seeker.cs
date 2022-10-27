using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    [Tooltip("Distance after which the seeker gives up")]
    public float giveUpDistance = 11f;

    [Tooltip("Pause before final rush")]
    public float terminalDelay = 1.5f;

    [Tooltip("Move Speed")]
    public float moveSpeed = 3f;

    [Tooltip("Rotation Speed (Radians)")]
    public float rotationSpeed = 1f;


    Transform target;
    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (active)
        {
            Vector3 targetF = Vector3.Normalize(target.position - transform.position);
            targetF.z = 0;

            Vector3 f = Vector3.RotateTowards(transform.forward, targetF, rotationSpeed * Time.deltaTime, 0.0f);
            transform.forward = f;
            
            transform.Translate(f * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            active = true;
            target = GameManager.instance.player.transform;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);

        if (target != null && false)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
