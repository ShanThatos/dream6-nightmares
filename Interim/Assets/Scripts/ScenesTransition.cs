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
        if (SceneManager.GetActiveScene().name == "EndCredits")
        {
            if (PlayerPrefs.GetInt("Failed", 0) == 0)
            {
                transitionAnim.Play("UnfadeWhite");
                return;
            }
        }

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
        string rebinds = PlayerPrefs.GetString("rebinds");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    private bool isAnimationStopped()
    {
        return transitionAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !transitionAnim.IsInTransition(0);
    }

    public void LockPlayer()
    {
        GameManager.LockMovement();
    }

    public void UnlockPlayer()
    {
        GameManager.UnlockMovement();
    }

    public void Finish(string sceneName)
    {
        transitionAnim.Play("FadeWhite");
        PlayerPrefs.SetInt("RemSolved", 1);
        LeanTween.delayedCall(gameObject, 5f, () =>
        {
            ChangeScene(sceneName);
            Debug.Log("Change scene to: " + sceneName);
        });
    }
}
