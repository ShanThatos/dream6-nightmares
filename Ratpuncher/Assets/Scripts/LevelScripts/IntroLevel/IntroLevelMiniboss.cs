using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLevelMiniboss : MonoBehaviour
{
    public GameObject bossReveal;
    public GameObject afterBossDoor;
    public GameObject bossHPBar;
    public Damagable bossDamagable;
    public GameObject[] barriers;

    bool active = false;

    void Start()
    {
        bossDamagable.OnDeath += OnBossDeath;

        foreach (GameObject obj in barriers)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || active)
        {
            return;
        }
        bossReveal.SetActive(false);
        bossHPBar.SetActive(true);

        foreach (GameObject obj in barriers)
        {
            obj.SetActive(true);
        }

        active = true;
    }

    void OnBossDeath()
    {
        foreach (GameObject obj in barriers)
        {
            obj.SetActive(false);
        }

        afterBossDoor.SetActive(false);
    }
}
