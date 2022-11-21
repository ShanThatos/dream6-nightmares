using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightZone : MonoBehaviour
{
    public Damagable[] enemies;
    public UnityEvent onEndEvent;

    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Damagable d in enemies)
        {
            d.limitEvents = true;
            enemyCount++;
            d.OnDeath += OnEnemyDead;
        }

        Debug.Log("Fight Zone with " + enemyCount + " enemies");
    }


    void EndFight()
    {
        onEndEvent.Invoke();
    }

    void OnEnemyDead()
    {
        enemyCount--;

        Debug.Log("Enemies Left: " + enemyCount);

        if(enemyCount <= 0)
        {
            EndFight();
        }
    }
}
