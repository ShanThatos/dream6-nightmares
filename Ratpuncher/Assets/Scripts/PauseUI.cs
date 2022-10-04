using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseUI : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject settingsButton;
    public GameObject exitbutton;
    public GameObject settingsFirstSelected;

    public GameObject settingsPanel;
    public GameObject pausePanel;

    private bool backInput;
    private bool oldBackInput;
    private System.Action onBackInput;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(resumeButton);
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
        settingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsFirstSelected);
        onBackInput = CloseSettings;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsButton);
        onBackInput = null;
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
