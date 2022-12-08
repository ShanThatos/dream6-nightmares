using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingJavelin : MonoBehaviour
{
    public GameObject Javelin;
    public Vector2 delay;

    private LineRenderer line;
    private float rand;
    private float timer = 0;
    private bool alreadyFired;

    private void Start()
    {
        rand = Random.Range(delay.x, delay.y);

        line = GetComponent<LineRenderer>();

        line.SetPosition(0, transform.position);
        Vector3 endpoint = transform.position;
        endpoint.y -= 30;
        line.SetPosition(1, endpoint);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        Color c = line.endColor;
        c.a = Mathf.Lerp(1, 0, timer / rand);

        print(c.a);

        line.endColor = c;
        line.startColor = c;

        if(timer >= rand && !alreadyFired)
        {
            alreadyFired = true;
            GameObject j = Instantiate(Javelin, transform.position, Quaternion.identity);
            j.GetComponent<Rigidbody2D>().AddForce(Vector3.down * 25, ForceMode2D.Impulse);
            Destroy(j, 10f);
            Destroy(gameObject, .5f);
        }
    }
}
