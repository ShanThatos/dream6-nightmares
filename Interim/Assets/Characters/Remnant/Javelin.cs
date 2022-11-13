using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : MonoBehaviour
{
    public float chargeTime;
    public float delay;
    public float force;

    public LineRenderer targetLine;
    public float startWidth;
    public float endWidth;

    public GameObject javelin;

    float timer;
    Transform player;
    bool charged;
    bool done;

    private void Start()
    {
        player = GameManager.GetPlayerTransform();
        targetLine.SetPosition(0, transform.position);
        timer = chargeTime;
        charged = false;
        done = false;
    }

    private void Update()
    {
        if (!charged)
        {
            timer -= Time.deltaTime;

            targetLine.widthMultiplier = Mathf.Lerp(startWidth, endWidth, (chargeTime - timer) / chargeTime);
            Vector3 dir = player.position - transform.position;
            dir = dir.normalized;
            dir *= 100;
            dir = transform.position + dir;

            targetLine.SetPosition(1, dir);

            if(timer < 0)
            {
                charged = true;
                timer = delay;
            }
        }

        if (charged && !done)
        {
            timer -= Time.deltaTime;

            float a = Mathf.Lerp(1, 0, (delay - timer) / delay);
            Color c = targetLine.startColor;
            c.a = a;

            targetLine.startColor = c;
            targetLine.endColor = c;


            if(timer < 0)
            {
                GameObject jv = Instantiate(javelin, transform.position, Quaternion.identity);
                jv.transform.forward = (targetLine.GetPosition(1) - transform.position).normalized;
                jv.GetComponent<Rigidbody2D>().AddForce(jv.transform.forward * force, ForceMode2D.Impulse);

                Destroy(this.gameObject, 1);
                done = true;
            }
        }
    }



}
