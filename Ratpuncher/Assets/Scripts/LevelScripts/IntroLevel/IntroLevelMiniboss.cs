using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLevelMiniboss : MonoBehaviour
{
    public GameObject BossReveal;
    public GameObject AfterBossDoor;
    public GameObject BossHPBar;
    public Damagable BossDamagable;

    bool active = false;

    void Start()
    {
        BossDamagable.OnDeath += OnBossDeath;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            return;
        }
        BossReveal.SetActive(false);
        BossHPBar.SetActive(true);
        active = true;
    }

    void OnBossDeath()
    {
        AfterBossDoor.SetActive(false);
    }
}
