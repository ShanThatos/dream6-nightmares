using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OfficeManager : MonoBehaviour
{
    public GameObject boardAlert;
    public GameObject ladybirdAlert;
    public GameObject elioSitAlert;
    public GameObject elioAlert;
    public GameObject remAlert;

    [Header("It starts here...")]
    public GameObject boardButton;
    public GameObject phoneButton;
    public GameObject noteButton;
    public GameObject safeButton;
    public GameObject menuButton;
    //public GameObject controlsButton;
    public GameObject boardFirstSelected;
    public GameObject phoneFirstSelected;
    public GameObject controlsFirstSelected;
    public GameObject XboxFirstSelected;
    public GameObject noteFirstSelected;
    public GameObject menuFirstSelected;
    //public GameObject safeFirstSelected;

    public GameObject boardPanel;
    public GameObject phonePanel;
    public GameObject controlsPanel;
    public GameObject xboxControlsPanel;
    public GameObject notePanel;
    public GameObject menuPanel;
    public GameObject pinCaseAlert;
    //public GameObject safePanel;

    private bool backInput;
    private bool oldBackInput;
    private bool isXboxControls;
    private System.Action onBackInput;
    private bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
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
            //EventSystem.current.SetSelectedGameObject(null);
            isTriggered = false;
        }
    }

    public void TriggerAlert()
    {
        if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 0 && PlayerPrefs.GetInt("HubStart", 0) == 1)
        {
            boardAlert.SetActive(true);
            ladybirdAlert.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ElioSolved", 0) == 0 && PlayerPrefs.GetInt("ElioIntro", 0) == 1 && PlayerPrefs.GetInt("ElioStart", 0) == 0)
        {
            elioSitAlert.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ElioSolved", 0) == 0 && PlayerPrefs.GetInt("ElioStart", 0) == 1)
        {
            elioSitAlert.SetActive(false);
            boardAlert.SetActive(true);
            elioAlert.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("RemSolved", 0) == 0 && PlayerPrefs.GetInt("RemActivated", 0) == 1)
        {
            boardAlert.SetActive(true);
            remAlert.SetActive(true);
        }
    }

    public void OpenSettings()
    {
        controlsPanel.SetActive(false);
        xboxControlsPanel.SetActive(false);
        phonePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(phoneFirstSelected);
        onBackInput = CloseSettings;
    }
    public void OpenControls()
    {
        xboxControlsPanel.SetActive(false);
        phonePanel.SetActive(false);
        controlsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(controlsFirstSelected);
        onBackInput = CloseSettings;
    }

    public void OpenXboxControls()
    {
        phonePanel.SetActive(false);
        controlsPanel.SetActive(false);
        xboxControlsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(XboxFirstSelected);
        onBackInput = CloseSettings;
    }

    public void CloseSettings()
    {
        phonePanel.SetActive(false);
        xboxControlsPanel.SetActive(false);
        controlsPanel.SetActive(false);
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

    public void OpenNote()
    {
        notePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(noteFirstSelected);
        onBackInput = CloseNote;
    }

    public void CloseNote()
    {
        notePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(noteButton);
        onBackInput = null;
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuFirstSelected);
        onBackInput = CloseMenu;
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(menuButton);
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

    public void TriggerPin(string pinText)
    {
        GameObject caseText = pinCaseAlert.transform.GetChild(0).gameObject;
        caseText.GetComponent<TextMeshProUGUI>().text = pinText;
        pinCaseAlert.SetActive(true);
        var seq = LeanTween.sequence();
        //seq.append(LeanTween.moveX(pinCaseAlert.GetComponent<RectTransform>(), 485, 1f));
        seq.append(LeanTween.scaleY(pinCaseAlert, 1, 0.15f));
        seq.append(3f);
        seq.append(() => {
            LeanTween.value(caseText, a => caseText.GetComponent<TextMeshProUGUI>().alpha = a, 1, 0, 1.5f);
            LeanTween.alpha(pinCaseAlert.GetComponent<RectTransform>(), 0f, 1.5f).setOnComplete(() =>
            {
                pinCaseAlert.transform.localScale = new Vector3(1, 0, 1);
                LeanTween.value(caseText, a => caseText.GetComponent<TextMeshProUGUI>().alpha = a, 0, 1, 0f);
                LeanTween.alpha(pinCaseAlert.GetComponent<RectTransform>(), 255f, 0f);
                pinCaseAlert.SetActive(false);
            });
        });

    }
}
