using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class DialogueController : MonoBehaviour
{
    //public HubDialogueSO hubDialogueSO;
    public DialogueSystem dialogueSystem;
    private GameObject blackPanel;
    private Queue<string> sentences;
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public GameObject optionsList;

    private bool oldInput;
    private bool input;
    public bool isDialogueOn;
    public bool isDialogueFinished;

    private bool isRunning;
    private bool intervalDone;
    public float textSpeed;

    public UnityEvent EndDialogueFunction;

    [HideInInspector] public string soSceneName;

    // Start is called before the first frame update
    void Start()
    {
        /* Testing substrings
        for (int i = 0; i < hubDialogueSO.firstDialogues.Length; i ++)
        {
            int separatorIndex = hubDialogueSO.firstDialogues[i].IndexOf(":");
            string name = hubDialogueSO.firstDialogues[i].Substring(0, separatorIndex);
            string dialogue = hubDialogueSO.firstDialogues[i].Substring(separatorIndex + 1);
            Debug.Log(name + " says" + dialogue);
        }
        */
        sentences = new Queue<string>();
        blackPanel = this.gameObject.transform.GetChild(0).gameObject;
        blackPanel.transform.localScale = new Vector3(0, 0, 0);
        //StartDialogue(hubDialogueSO.firstDialogues);
        // Debug.Log("YAY I STARTED");
    }

    // Update is called once per frame
    private void Update()
    {
        oldInput = input;
        //input = Input.anyKeyDown;
        input = Input.GetAxisRaw("Submit") > 0 || Input.GetMouseButtonDown(0);
        if (isDialogueOn && intervalDone)
        {
            if (input && !oldInput)
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(string[] dialogue)
    {
        intervalDone = false;
        Invoke("DoneWait", 2f);
        optionsList.SetActive(false);
        sentences.Clear();
        if (!isDialogueOn)
        {
            isDialogueOn = true;
            blackPanel.transform.localScale = new Vector3(1, 1, 1);
            LeanTween.scaleY(dialoguePanel, 1, 0.2f);
            LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 1f, 0.2f).setDelay(0.1f);
            LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.5f, 0.2f);
        }
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (!isRunning)
        {
            oldInput = input;

            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isRunning = true;
        string nameChar;
        string spriteName;
        string dialogue;
        try
        {
            int startIndex = sentence.IndexOf("(");
            int endIndex = sentence.IndexOf(")");
            //Debug.Log("Start: " + startIndex + " End: " + endIndex);
            nameChar = sentence.Substring(0, startIndex);
            spriteName = sentence.Substring(startIndex + 1, endIndex - (startIndex + 1));
            dialogue = sentence.Substring(endIndex + 2);
        }
        catch (ArgumentOutOfRangeException)
        {
            spriteName = "";
            nameChar = "";
            dialogue = sentence;
        }
        dialogueText.text = sentence;
        nameText.text = nameChar;
        //Debug.Log("Start: " + startIndex + " End: " + endIndex);
        //Debug.Log(name);
        //Debug.Log(spriteName);
        for (int i = 0; i < dialogueSystem.character.Length; i++)
        {
            if (spriteName == dialogueSystem.character[i].spriteName)
            {
                characterImage.sprite = dialogueSystem.character[i].sprite;
                break;
            }
        }
        dialogueText.text = dialogue;
        dialogueText.maxVisibleCharacters = 0;

        for (float t = 0; dialogueText.maxVisibleCharacters < sentence.Length; t += Time.deltaTime)
        {
            dialogueText.maxVisibleCharacters = (int)(t * textSpeed);
            if (input && !oldInput)
            {
                if (sentence.Length - dialogueText.maxVisibleCharacters < 20)
                {
                    isRunning = false;
                    DisplayNextSentence();
                }
                else
                {
                    // consume input
                    oldInput = input;
                    dialogueText.maxVisibleCharacters = sentence.Length;
                }
            }
            yield return null;
        }
        isRunning = false;
    }
    public void EndDialogue()
    {
        int currentIndex = DialogueManager.instance.CurrentDialogueIndex();
        Debug.Log("Dialogue " + currentIndex + " : " + DialogueManager.instance.CurrentDialogueName());
        
        if (dialogueSystem.dialogues[currentIndex].options.Length == 0)
        {
            if (isDialogueOn)
            {
                isDialogueFinished = true;
                Invoke("ResetDialogue", 5f);
                Debug.Log("Dialogue Ended");
                blackPanel.transform.localScale = new Vector3(0, 0, 0);
                LeanTween.scaleY(dialoguePanel, 0, 0.2f);
                LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 0f, 0.2f);
                LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0, 0.2f);
                isDialogueOn = false;
                EndDialogueFunction.Invoke();
                if (soSceneName != "")
                {
                    ScenesTransition.instance.LoadScene(soSceneName);
                }
            }
            //dialoguePanel.SetActive(false);

            GameManager.UnlockMovement();
        }
        else
        {
            for (int idx = 0; idx < optionsList.transform.childCount; idx++ )
            {
                string currentOption = dialogueSystem.dialogues[currentIndex].options[idx];
                int startIndex = currentOption.IndexOf("(");
                int endIndex = currentOption.IndexOf(")");
                string optionName = currentOption.Substring(startIndex + 1, endIndex - (startIndex + 1));
                string optionText = currentOption.Substring(endIndex + 2);
                optionsList.transform.GetChild(idx).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = optionText;
                optionsList.transform.GetChild(idx).name = optionName;
            }
            optionsList.SetActive(true);
            EventSystem.current.SetSelectedGameObject(optionsList.transform.GetChild(0).gameObject);
        }
    }

    private void DoneWait()
    {
        intervalDone = true;
    }

    public void check(string text)
    {
        Debug.Log(text);
    }

    private void ResetDialogue()
    {
        isDialogueFinished = false;
    }
}
