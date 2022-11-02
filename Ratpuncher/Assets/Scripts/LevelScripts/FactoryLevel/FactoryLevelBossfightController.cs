using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryLevelBossfightController : MonoBehaviour
{

    public GameObject bossHPBar; 
    public GameObject boss;
    Damagable bossDamagable;
    public GameObject[] barriers;

    bool active = false;

    void Start()
    {
        boss.SetActive(false);
        bossDamagable = boss.GetComponent<Damagable>();
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
        boss.SetActive(true);
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

        // dashTutorial.Open();
        GameManager.instance.player.GetComponent<PlayerCapabilities>().canDash = true;
        // afterBossDoor.SetActive(false);
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
