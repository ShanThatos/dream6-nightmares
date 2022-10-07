using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool isGameStart;
    public Animator canvasAnim;

    [Header("We all love UI programming")]
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject creditsButton;
    public GameObject settingsFirstSelected;
    public GameObject creditsFirstSelected;
    public GameObject controlsFirstSelected;

    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject controlsPanel;
    public GameObject xboxControlsPanel;

    private bool backInput;
    private bool oldBackInput;
    private bool isXboxControls;
    private System.Action onBackInput;

    void Start()
    {
        isGameStart = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Update()
    {
        if (Input.anyKeyDown && !isGameStart)
        {
            StartGame();
            isGameStart = true;
        }

        oldBackInput = backInput;
        backInput = Input.GetAxisRaw("Cancel") > 0;

        if (backInput && !oldBackInput)
        {
            onBackInput?.Invoke();
        }
    }

    // It all starts here...
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsFirstSelected);
        onBackInput = CloseSettings;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(settingsButton);
        onBackInput = null;
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(creditsFirstSelected);
        onBackInput = CloseCredits;
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(creditsButton);
        onBackInput = null;
    }

    public void OpenControls()
    {
        settingsButton.SetActive(false);
        controlsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(controlsFirstSelected);
        onBackInput = CloseControls;
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        xboxControlsPanel.SetActive(false);
        settingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsFirstSelected);
        onBackInput = null;
    }

    public void ToggleControlPanelType()
    {
        if (isXboxControls)
        {
            isXboxControls = false;
            controlsPanel.SetActive(true);
            xboxControlsPanel.SetActive(false);
        }
        else
        {
            isXboxControls = true;
            controlsPanel.SetActive(false);
            xboxControlsPanel.SetActive(true);
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
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void ButtonSelect(GameObject go)
    {
        LeanTween.scale(go, new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
    }

    public void ButtonDeselect(GameObject go)
    {
        LeanTween.scale(go, new Vector3(1f, 1f, 1f), 0.1f);
    }

    public void SliderSelect(GameObject go)
    {
        LeanTween.scale(go, new Vector3(4.2f, 3.2f, 4.2f), 0.1f);
    }

    public void SliderDeselect(GameObject go)
    {
        LeanTween.scale(go, new Vector3(4f, 3f, 4f), 0.1f);
    }
}
