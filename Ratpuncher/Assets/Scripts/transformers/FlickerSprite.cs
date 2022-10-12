using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerSprite : MonoBehaviour {

    private SpriteRenderer sr;
    private Image img;

    public int numFlickers = 5;
    public float flickerTime = 0.1f;
    public Color flickerColor = Color.white;

    bool flickering = false;

    void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();
        img = GetComponent<Image>();
    }

    public void Flicker() {
        if (flickering) return;
        flickering = true;
        if(sr) StartCoroutine(FlickerCoroutine());
        if(img) StartCoroutine(FlickerUICoroutine());

    }

    private IEnumerator FlickerCoroutine() {
        Color originalColor = sr.color;
        bool isFlickerColor = false;
        for (int i = 0; i < numFlickers; i++) {
            isFlickerColor = !isFlickerColor;
            Color color = isFlickerColor ? flickerColor : originalColor;
            color.a = sr.color.a;
            sr.color = color;
            Debug.Log(sr.color);
            yield return new WaitForSeconds(flickerTime);
        }
        sr.color = originalColor;
        flickering = false;
    }

    private IEnumerator FlickerUICoroutine()
    {
        Color originalColor = img.color;
        bool isFlickerColor = false;
        for (int i = 0; i < numFlickers; i++)
        {
            isFlickerColor = !isFlickerColor;
            Color color = isFlickerColor ? flickerColor : originalColor;
            color.a = img.color.a;
            img.color = color;
            yield return new WaitForSeconds(flickerTime);
        }
        img.color = originalColor;
        flickering = false;
    }
}
