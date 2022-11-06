using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteActive : MonoBehaviour
{
    public string Check;
    public string ItemName;
    [TextArea(5,10)]
    public string Description;
    private GameObject details;
    private bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        details = gameObject.transform.GetChild(0).gameObject;
        details.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ItemName;
        details.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = Description;

        if (PlayerPrefs.GetInt(Check, 0) == 1)
        {
            this.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.GetComponent<Button>().interactable = false;
        }
    }

    public void ToggleDetails()
    {
        if (!isOpened)
        {
            LeanTween.scaleX(details, 1, 0.1f);
            isOpened = true;
        }
        else
        {
            LeanTween.scaleX(details, 0, 0.1f);
            isOpened = false;
        }
    }

    public void CloseDetails()
    {
        LeanTween.scaleX(details, 0, 0.1f);
        isOpened = false;
    }
}
