using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Slider bar;

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
    }

    void OnPlayerDamaged(float damage)
    {
        currPlayerHp -= damage;
        Debug.Log("PlayerHP Bar: " + currPlayerHp);
        bar.value = currPlayerHp / maxPlayerHP;
    }
}
