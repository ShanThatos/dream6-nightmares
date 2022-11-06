using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextInteraction : MonoBehaviour
{
    [TextArea(5, 10)]
    public string textString;
    private GameObject textObject;
    private GameObject blackPanel;
    public bool textShown;
    // Start is called before the first frame update
    void Start()
    {
        textObject = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        blackPanel = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        textObject.GetComponent<TextMeshProUGUI>().alpha = 0;
        textObject.GetComponent<TextMeshProUGUI>().text = textString;
    }

    public void ShowText()
    {
        LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.75f, 1);
        LeanTween.value(textObject, a => textObject.GetComponent<TextMeshProUGUI>().alpha = a, 0, 1, 1f);
        textShown = true;
    }

    public void RemoveText()
    {
        LeanTween.cancel(textObject);
        LeanTween.cancel(blackPanel);
        LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0f, 1);
        LeanTween.value(textObject, a => textObject.GetComponent<TextMeshProUGUI>().alpha = a, 1, 0, 1f);
        textShown = false;
    }
}
