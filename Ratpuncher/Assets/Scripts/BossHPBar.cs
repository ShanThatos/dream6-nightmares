using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    // Start is called before the first frame update

    public string bossName;
    public float subBarDelay = 1.0f;
    float currentDelay;

    public float maxHP = 500;
    float currentHP;
    float targetValue;

    [Space(5)]
    [Header("UI Elements")]
    public TextMeshProUGUI nameText;
    public Slider mainBar;
    public Slider subBar;

    public float subBarTarget;
    
    void Start()
    {
        mainBar.value = 1;
        subBar.value = 1;
        nameText.text = bossName;
        currentHP = maxHP;
        targetValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
        }
        
        if(currentDelay <= 0)
        {
            subBarTarget = targetValue;
        }

        float lerpVal = .9f;
        if(mainBar.value - targetValue <= .001f)
        {
            lerpVal = 0;
        }

        float subLerpVal = .98f;
        if (subBar.value - subBarTarget <= .001f)
        {
            subLerpVal = 0;
        }

        mainBar.value = Mathf.Lerp(targetValue, mainBar.value, lerpVal);
        subBar.value = Mathf.Lerp(subBarTarget, subBar.value, subLerpVal);
    }

    public void ShowBar()
    {

    }

    public void RecieveDamage(float damage)
    {
        currentHP -= damage;

        targetValue = currentHP / maxHP;

        currentDelay = subBarDelay;

        // Debug.Log(targetValue);
    }

    public void OnDeath()
    {
        targetValue = 0;

        currentDelay = .3f;

        TryGetComponent<Animator>(out Animator ani);
        if (ani)
        {
            ani.SetTrigger("Death");
        }
    }

    public void SetMaxHP(float hp)
    {
        maxHP = hp;
        currentHP = maxHP;
    }
}
