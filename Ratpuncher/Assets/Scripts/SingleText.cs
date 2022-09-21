using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleText : MonoBehaviour
{
    public bool needsTrigger;
    [TextArea(5, 10)]
    public string textString;
    private GameObject textObject;
    private GameObject blackPanel;
    private bool isTriggered;
    private GameObject actionIndicator;
    private bool oldInput;
    private bool input;
    private bool isInside;
    private bool textShown;

    // Start is called before the first frame update
    void Start()
    {
        textObject = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        blackPanel = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        textObject.GetComponent<TextMeshProUGUI>().alpha = 0;
        textObject.GetComponent<TextMeshProUGUI>().text = textString;
    }

    // Update is called once per frame
    void Update()
    {
        oldInput = input;
        input = Input.GetAxisRaw("Interact") > 0;
        if (isInside && needsTrigger)
        {
            if (input && !oldInput)
            {
                if (!textShown)
                {
                    LeanTween.scaleY(actionIndicator, 0, 0.2f);
                    ShowText();
                }
                else
                {
                    RemoveText();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInside = true;
            if (!isTriggered)
            {
                if (!needsTrigger)
                {
                    ShowText();
                }
                else
                {
                    actionIndicator = Instantiate(Resources.Load("ActionIndicator")) as GameObject;
                    actionIndicator.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "F";
                    actionIndicator.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Interact";
                    actionIndicator.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 1, this.gameObject.transform.position.z);
                    LeanTween.moveY(actionIndicator, this.gameObject.transform.position.y, 0.2f);
                    LeanTween.scaleY(actionIndicator, 1, 0.2f);
                }
                isTriggered = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInside = false;
            if (!needsTrigger)
            {
                RemoveText();
            }
            else
            {
                LeanTween.scaleY(actionIndicator, 0, 0.2f).setDestroyOnComplete(true);
                if (textShown)
                {
                    RemoveText();
                }
            }
            isTriggered = false;
        }
    }

    private void ShowText()
    {
        LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.75f, 1);
        LeanTween.value(textObject, a => textObject.GetComponent<TextMeshProUGUI>().alpha = a, 0, 1, 1f);
        textShown = true;
    }

    private void RemoveText()
    {
        LeanTween.cancel(textObject);
        LeanTween.cancel(blackPanel);
        LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0f, 1);
        LeanTween.value(textObject, a => textObject.GetComponent<TextMeshProUGUI>().alpha = a, 1, 0, 1f);
        textShown = false;
    }
}
