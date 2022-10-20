using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseUI : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject settingsButton;
    //public GameObject controlsButton;
    public GameObject exitbutton;
    public GameObject settingsFirstSelected;
    public GameObject controlsFirstSelected;
    public GameObject XboxFirstSelected;

    public GameObject settingsPanel;
    public GameObject pausePanel;
    public GameObject controlsPanel;
    public GameObject xboxControlsPanel;

    private bool backInput;
    private bool oldBackInput;
    private bool isXboxControls;
    private System.Action onBackInput;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(resumeButton);
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        oldBackInput = backInput;
        backInput = Input.GetAxisRaw("Cancel") > 0;

        if (backInput && !oldBackInput)
        {
            onBackInput?.Invoke();
        }
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);
        xboxControlsPanel.SetActive(false);
        settingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsFirstSelected);
        onBackInput = CloseSettings;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        xboxControlsPanel.SetActive(false);
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsButton);
        onBackInput = null;
    }
    public void OpenControls()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        xboxControlsPanel.SetActive(false);
        controlsPanel.SetActive(true);
        isXboxControls = false;
        EventSystem.current.SetSelectedGameObject(controlsFirstSelected);
        onBackInput = CloseSettings;
    }

    public void OpenXboxControls()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        xboxControlsPanel.SetActive(true);
        isXboxControls = true;
        EventSystem.current.SetSelectedGameObject(XboxFirstSelected);
        onBackInput = CloseSettings;
    }
    public void ToggleControlPanelType()
    {
        if (isXboxControls)
        {
            isXboxControls = false;
            controlsPanel.SetActive(true);
            xboxControlsPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(controlsFirstSelected);
        }
        else
        {
            isXboxControls = true;
            controlsPanel.SetActive(false);
            xboxControlsPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(XboxFirstSelected);
        }
    }

    public void ButtonSelect(GameObject go)
    {
        go.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void ButtonDeselect(GameObject go)
    {
        go.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void SliderSelect(GameObject go)
    {
        go.transform.localScale = new Vector3(4.2f, 3.2f, 4.2f);
    }

    public void SliderDeselect(GameObject go)
    {
        go.transform.localScale = new Vector3(4f, 3f, 4f);
    }
}
