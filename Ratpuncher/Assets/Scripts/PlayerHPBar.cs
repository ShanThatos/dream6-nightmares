using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Slider bar;
    public FlickerSprite flicker;

    float maxPlayerHP = 500;
    float currPlayerHp = 500;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        Damagable playerHP = player.GetComponent<Damagable>();
        maxPlayerHP = playerHP.GetMaxHealth();
        currPlayerHp = maxPlayerHP;

        playerHP.OnHurt += OnPlayerDamaged;
        playerHP.OnRespawn += OnRespawned;
        playerHP.OnDeath += OnPlayerDeath;
    }

    void OnPlayerDamaged(float damage, bool isEnergy = false)
    {
        currPlayerHp -= damage;
        bar.value = currPlayerHp / maxPlayerHP;

        if (flicker)
        {
            flicker.Flicker();
        }
        
    }

    void OnRespawned()
    {
        currPlayerHp = maxPlayerHP;
        bar.value = 1;
    }

    void OnPlayerDeath()
    {
        currPlayerHp = 0;
        bar.value = 0;
        flicker.Flicker();
    }
}
