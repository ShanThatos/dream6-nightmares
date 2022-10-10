using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OfficeManager : MonoBehaviour
{
    public GameObject boardAlert;
    public GameObject ladybirdAlert;
    public GameObject elioAlert;
    public GameObject remAlert;

    [Header("It starts here...")]
    public GameObject boardButton;
    public GameObject phoneButton;
    public GameObject noteButton;
    public GameObject safeButton;
    public GameObject controlsButton;
    public GameObject boardFirstSelected;
    public GameObject phoneFirstSelected;
    public GameObject controlsFirstSelected;
    public GameObject XboxFirstSelected;
    //public GameObject noteFirstSelected;
    //public GameObject safeFirstSelected;

    public GameObject boardPanel;
    public GameObject phonePanel;
    public GameObject controlsPanel;
    public GameObject xboxControlsPanel;
    //public GameObject notePanel;
    //public GameObject safePanel;

    private bool backInput;
    private bool oldBackInput;
    private bool isXboxControls;
    private System.Action onBackInput;
    private bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 0)
        {
            boardAlert.SetActive(true);
            ladybirdAlert.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ElioSolved", 0) == 0 && PlayerPrefs.GetInt("ElioActivated", 0) == 1)
        {
            boardAlert.SetActive(true);
            elioAlert.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("RemSolved", 0) == 0 && PlayerPrefs.GetInt("RemActivated", 0) == 1)
        {
            boardAlert.SetActive(true);
            remAlert.SetActive(true);
        }
        EventSystem.current.SetSelectedGameObject(null);
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
        if (!DialogueManager.instance.isDialogueOn)
        {
            if (!isTriggered)
            {
                EventSystem.current.SetSelectedGameObject(boardButton);
                isTriggered = true;
            }
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            isTriggered = false;
        }
    }

    public void OpenSettings()
    {
        phonePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(phoneFirstSelected);
        onBackInput = CloseSettings;
    }

    public void CloseSettings()
    {
        phonePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(phoneButton);
        onBackInput = null;
    }

    public void OpenBoard()
    {
        boardPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(boardFirstSelected);
        onBackInput = CloseBoard;
    }

    public void CloseBoard()
    {
        boardPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(boardButton);
        onBackInput = null;
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
        phonePanel.SetActive(false);
        isXboxControls = false;
        EventSystem.current.SetSelectedGameObject(controlsFirstSelected);
        onBackInput = CloseControls;
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        xboxControlsPanel.SetActive(false);
        phonePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(controlsButton);
        onBackInput = null;
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
