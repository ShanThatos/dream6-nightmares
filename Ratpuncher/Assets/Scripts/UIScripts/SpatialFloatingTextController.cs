using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using TM = TMPro.TextMeshProUGUI;

public class SpatialFloatingTextController : MonoBehaviour {

    public Text[] yay;

    [Serializable]
    public class Text {
        [TextArea(4,10)]
        public string text;
        public float duration;
    }

    public float transitionTime;
    bool isRunning = false;

    Animator animator;


    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {}

    
    public void StartFloatingText() {
        if (isRunning) return;
        isRunning = true;
        StartCoroutine(StartTextCoroutine());
    }

    public IEnumerator StartTextCoroutine() {
        animator.speed = 1 / (transitionTime / 2);
        for (int i = 0; i < yay.Length; i++) {
            SetText(yay[i].text);
            animator.Play("FadeIn");
            yield return new WaitForSeconds(transitionTime / 2 + yay[i].duration);
            animator.Play("FadeOut");
            yield return new WaitForSeconds(transitionTime / 2);
        }
        isRunning = false;
        yield return null;
    }



    TM _textMesh;
    void SetText(string text) {
        if (_textMesh == null)
            _textMesh = GetComponentInChildren<TM>();
        _textMesh.SetText(text);
    }

    private void OnValidate() {
        if (yay != null && yay.Length > 0)
            SetText(yay[0].text);
    }
}
