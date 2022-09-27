using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ScenesTransition : MonoBehaviour
{
    public static ScenesTransition instance { get; private set; }
    private Animator transitionAnim;
    private bool isTransitioning;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        transitionAnim = gameObject.GetComponent<Animator>();
        //transitionAnim.Play("OpenEyes");
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            transitionAnim.Play("OpenEyes");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isAnimationStopped());
    }
    public void LoadScene(string sceneName)
    {
        if (isAnimationStopped())
        {
            transitionAnim.Play("CloseEyes");
            LeanTween.delayedCall(gameObject, 4f, () =>
            {
                SceneManager.LoadScene(sceneName);
                Debug.Log("Change scene to: " + sceneName);
            });
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetInactive(GameObject ob)
    {
        ob.SetActive(false);
    }

    public void SetActive(GameObject ob)
    {
        ob.SetActive(true);
    }

    private bool isAnimationStopped()
    {
        return transitionAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !transitionAnim.IsInTransition(0);
    }
}
