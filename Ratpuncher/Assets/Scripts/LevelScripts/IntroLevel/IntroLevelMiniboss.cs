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
        Damagable playerDamagable = GameManager.instance.player.GetComponent<Damagable>();
        playerDamagable.OnRespawn += ResetFight;

        setBarriersActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || active)
        {
            return;
        }
        bossReveal.SetActive(false);
        bossHPBar.SetActive(true);

        setBarriersActive(true);

        active = true;
    }

    void OnBossDeath()
    {
        foreach(GameObject obj in barriers)
        {
            obj.GetComponent<Animator>().SetTrigger("stop");
        }

        afterBossDoor.SetActive(false);
    }

    void ResetFight()
    {
        setBarriersActive(false);
        active = false;
        bossDamagable.Respawn();
    }

    void setBarriersActive(bool active)
    {
        foreach (GameObject obj in barriers)
        {
            obj.SetActive(active);
        }
    }
}
