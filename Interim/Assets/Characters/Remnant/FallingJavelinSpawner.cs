using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingJavelinSpawner : MonoBehaviour
{

    public Vector2 bounds;
    public Vector2 delay;
    public int waveSize;

    private float timer;

    public GameObject javelin;
    public Damagable remnantHP;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(delay.x, delay.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(delay.x, delay.y);

            for(int i = 0; i < waveSize; i++)
            {
                Debug.Log("Spawning javelin");
                Vector2 pos = new Vector2(Random.Range(bounds.x, bounds.y), transform.position.y);
                Instantiate(javelin, pos, Quaternion.identity);
            }

            if(remnantHP.GetHealth() <= .33)
            {
                Vector2 pos = new Vector2(GameManager.GetPlayerTransform().position.x, transform.position.y);
                Instantiate(javelin, pos, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(bounds.x, transform.position.y), new Vector2(bounds.y, transform.position.y));
    }
}
