using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerSprite : MonoBehaviour {

    private SpriteRenderer sr;

    public int numFlickers = 5;
    public float flickerTime = 0.1f;
    public Color flickerColor = Color.white;

    bool flickering = false;

    void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();
        Debug.Log(sr);
    }

    public void Flicker() {
        if (flickering) return;
        flickering = true;
        StartCoroutine(FlickerCoroutine());
    }

    private IEnumerator FlickerCoroutine() {
        Color originalColor = sr.color;
        bool isFlickerColor = false;
        for (int i = 0; i < numFlickers; i++) {
            isFlickerColor = !isFlickerColor;
            Color color = isFlickerColor ? flickerColor : originalColor;
            color.a = sr.color.a;
            sr.color = color;
            yield return new WaitForSeconds(flickerTime);
        }
        sr.color = originalColor;
        flickering = false;
    }
}
