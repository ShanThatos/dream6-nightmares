using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
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

    private bool oldInput;
    private bool input;
    public bool isDialogueOn;
    public bool isDialogueFinished;

    private bool isRunning;
    public float textSpeed;

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
        Debug.Log("YAY I STARTED");
    }

    // Update is called once per frame
    private void Update()
    {
        oldInput = input;
        input = Input.GetAxisRaw("Submit") > 0;
        if (isDialogueOn)
        {
            if (input && !oldInput)
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(string[] dialogue)
    {
        if (!isDialogueOn)
        {
            isDialogueOn = true;
            sentences.Clear();
            blackPanel.transform.localScale = new Vector3(1, 1, 1);
            LeanTween.scaleY(dialoguePanel, 1, 0.2f);
            LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 1f, 0.2f).setDelay(0.1f);
            LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.5f, 0.2f);
            foreach (string sentence in dialogue)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }
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
                // consume input
                oldInput = input;
                dialogueText.maxVisibleCharacters = sentence.Length;
            }
            yield return null;
        }

        isRunning = false;

    }
    public void EndDialogue()
    {
        if (isDialogueOn)
        {
            isDialogueFinished = true;
            Debug.Log("Dialogue Ended");
            blackPanel.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scaleY(dialoguePanel, 0, 0.2f);
            LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 0f, 0.2f);
            LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0, 0.2f);
            isDialogueOn = false;
            if (soSceneName != "")
            {
                ScenesTransition.instance.LoadScene(soSceneName);
            }
        }
        //dialoguePanel.SetActive(false);

        GameManager.SetMovementLock(false);
    }

    public void check(string text)
    {
        Debug.Log(text);
    }
}
