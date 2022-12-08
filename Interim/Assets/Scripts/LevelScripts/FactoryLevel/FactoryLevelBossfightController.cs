using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryLevelBossfightController : MonoBehaviour
{

    public GameObject bossHPBar; 
    public GameObject boss;
    Damagable bossDamagable;
    public GameObject[] barriers;

    bool triggeredBossMusic = false;
    public AudioSource levelMusic;
    public AudioSource bossfightMusic;
    public float bossfightMusicVolume = 1.0f;

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
        MainCameraScript.instance.useSetting(0);
        boss.SetActive(true);
        bossHPBar.SetActive(true);

        setBarriersActive(true);

        active = true;
        StartCoroutine(startBossMusic());
    }

    private IEnumerator startBossMusic() {
        if (triggeredBossMusic) yield return null;
        triggeredBossMusic = true;
        bossfightMusic.Play();
        for (float i = 0; i <= 1.0f; i += 0.1f) {
            levelMusic.volume = 1.0f - i;
            bossfightMusic.volume = i * bossfightMusicVolume;
            yield return new WaitForSeconds(0.5f);
        }
        levelMusic.volume = 0.0f;
        levelMusic.Stop();
        bossfightMusic.volume = bossfightMusicVolume;
    }

    void OnBossDeath()
    {
        // foreach(GameObject obj in barriers)
        // {
        //     obj.GetComponent<Animator>().SetTrigger("stop");
        // }

        // dashTutorial.Open();
        // GameManager.instance.player.GetComponent<PlayerCapabilities>().canDash = true;
        // afterBossDoor.SetActive(false);
    }

    void ResetFight()
    {
        MainCameraScript.instance.useDefaultSetting();
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
