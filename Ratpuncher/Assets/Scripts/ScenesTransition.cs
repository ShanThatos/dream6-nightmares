using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
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
        if (SceneManager.GetActiveScene().name != "MainMenu" || SceneManager.GetActiveScene().name != "Office")
        {
            transitionAnim.Play("OpenEyes");
            LockPlayer();
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
            if (SceneManager.GetActiveScene().name != "MainMenu" || SceneManager.GetActiveScene().name != "Office")
            {
                LockPlayer();
            }
            LeanTween.delayedCall(gameObject, 2.3f, () =>
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

    public void ResetLevels()
    {
        PlayerPrefs.SetInt("LadybirdSolved", 0);
        PlayerPrefs.SetInt("IntroStart", 0);
        PlayerPrefs.SetInt("HubStart", 0);
        PlayerPrefs.SetInt("LadybirdStart", 0);
        PlayerPrefs.SetInt("LadybirdClosure", 0);
        PlayerPrefs.SetInt("FactoryStart", 0);
    }

    private bool isAnimationStopped()
    {
        return transitionAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !transitionAnim.IsInTransition(0);
    }

    public void LockPlayer()
    {
        GameManager.SetMovementLock(true);
    }

    public void UnlockPlayer()
    {
        GameManager.SetMovementLock(false);
    }
}
