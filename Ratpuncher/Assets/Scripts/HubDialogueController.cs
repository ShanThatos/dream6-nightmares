using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HubDialogueController : MonoBehaviour
{
    public HubDialogueSO hubDialogueSO;
    private Queue<string> sentences;
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    private bool oldInput;
    private bool input;

    private bool isRunning;
    public float textSpeed;

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
        StartDialogue(hubDialogueSO);
    }

    // Update is called once per frame
    private void Update()
    {
        oldInput = input;
        input = Input.GetAxisRaw("Submit") > 0;
        if (input && !oldInput)
        {
            DisplayNextSentence();
        }
        
    }

    public void StartDialogue(HubDialogueSO dialogueSO)
    {
        sentences.Clear();
        
        foreach (string sentence in dialogueSO.firstDialogues)
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
        int separatorIndex = sentence.IndexOf(":");
        string name = sentence.Substring(0, separatorIndex);
        string dialogue = sentence.Substring(separatorIndex + 1); dialogueText.text = sentence;
        nameText.text = name;
        for (int i = 0; i < hubDialogueSO.character.Length; i++)
        {
            if (name == hubDialogueSO.character[i].name)
            {
                characterImage.sprite = hubDialogueSO.character[i].sprite;
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
        Debug.Log("Dialogue Ended");
        dialoguePanel.SetActive(false);
    }
}