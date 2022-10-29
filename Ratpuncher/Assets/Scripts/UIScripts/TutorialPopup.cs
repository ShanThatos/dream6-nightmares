using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialPopup : MonoBehaviour
{
    

    public string Header;

    public InputActionReference action;

    [Header("[KEY] will be replaced by the bound key for the above action")]
    [TextArea(5, 10)]
    public string description;

    public Sprite image;

    public string followUpDialogue;

    private GameObject itemIdentifier;
    private GameObject blackPanel;
    private Image itemImage;
    private TextMeshProUGUI itemDescription;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI promptText;
    public bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        blackPanel = GameObject.FindGameObjectWithTag("BlackPanel");
        itemIdentifier = GameObject.FindGameObjectWithTag("ItemIdentifier");
        itemImage = itemIdentifier.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        itemName = itemIdentifier.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemDescription = itemIdentifier.transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        promptText = itemIdentifier.transform.GetChild(0).transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public void Open()
    {
        itemName.text = Header;

        string keyName = action.action.GetBindingDisplayString();
        description = description.Replace("[KEY]", keyName);
        itemDescription.text = description;

        itemImage.sprite = image;

        promptText.text = "Jump to Close";

        blackPanel.transform.localScale = new Vector3(1, 1, 1);
        LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.75f, 0.1f);
        LeanTween.scale(itemIdentifier, new Vector3(1, 1, 1), 0.1f);
        isOpened = true;



        InteractionTrigger player = GameManager.instance.player.GetComponent<InteractionTrigger>();
        player.openTutorialPopup(this);
    }
    public void Close()
    {
        blackPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.0f, 0.1f);
        LeanTween.scale(itemIdentifier, new Vector3(0, 0, 0), 0.1f);
        isOpened = false;

        if (!string.IsNullOrEmpty(followUpDialogue))
        {
            StartCoroutine(DialogueDelay());
        }
    }

    IEnumerator DialogueDelay()
    {
        yield return new WaitForSecondsRealtime(.5f);
        DialogueManager.instance.PlayDialogue(followUpDialogue);
    }
}
