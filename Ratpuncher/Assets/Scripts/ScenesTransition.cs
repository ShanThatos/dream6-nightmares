using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ScenesTransition : MonoBehaviour
{
    private Animator transitionAnim;
    public bool HasChangedScenes { get; private set; } // true if a scene change has taken place
    public bool IsTransitioning { get; private set; } // true if we are in the process of changing scenes
    bool didTransitionOut;
    
    public static System.Action OnTransition;         // called whenever a scene transition takes place
    // Start is called before the first frame update
    void Start()
    {
        transitionAnim = gameObject.GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            transitionAnim.Play("OpenEyes");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string sceneName)
    {
        transitionAnim.Play("CloseEyes");
        LeanTween.delayedCall(gameObject, 4f, () =>
        {
            SceneManager.LoadSceneAsync(sceneName);
        });
    }

    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SetInactive(GameObject ob)
    {
        ob.SetActive(false);
    }

    public void SetActive(GameObject ob)
    {
        ob.SetActive(true);
    }
}
