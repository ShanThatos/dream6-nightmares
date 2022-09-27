using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool isGameStart;
    public Animator canvasAnim;
    void Start()
    {
        isGameStart = false;
    }

    void Update()
    {
        if (Input.anyKeyDown && !isGameStart)
        {
            StartGame();
            isGameStart = true;
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        canvasAnim.Play("OpenMenuAnim");
        Debug.Log("StartGame");
    }

    public void ButtonSelect(GameObject go)
    {
        LeanTween.scale(go, new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
    }

    public void ButtonDeselect(GameObject go)
    {
        LeanTween.scale(go, new Vector3(1f, 1f, 1f), 0.2f);
    }
}
