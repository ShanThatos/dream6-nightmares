using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [TextArea(5, 10)]
    public string descriptionString;

    private GameObject itemIdentifier;
    private GameObject blackPanel;
    private GameObject notification;
    private Image itemImage;
    private TextMeshProUGUI itemDescription;
    private TextMeshProUGUI itemName;
    public bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        blackPanel = GameObject.FindGameObjectWithTag("BlackPanel");
        notification = GameObject.FindGameObjectWithTag("Notification");
        notification.GetComponentInChildren<TextMeshProUGUI>().text = "Press Attack Key to Collect";
        itemIdentifier = GameObject.FindGameObjectWithTag("ItemIdentifier");
        itemImage = itemIdentifier.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        itemName = itemIdentifier.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        itemDescription = itemIdentifier.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OpenIdentifier()
    {
        itemName.text = name;
        itemDescription.text = descriptionString;
        itemImage.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        LeanTween.scaleY(notification, 1, 0.1f);
        blackPanel.transform.localScale = new Vector3(1, 1, 1);
        LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.75f, 0.1f);
        LeanTween.scale(itemIdentifier, new Vector3(1, 1, 1), 0.1f);
        isOpened = true;
    }
    public void CloseIdentifier()
    {
        LeanTween.scaleY(notification, 0, 0.1f);
        blackPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.0f, 0.1f);
        LeanTween.scale(itemIdentifier, new Vector3(0, 0, 0), 0.1f);
        isOpened = false;
    }

    public void Collect()
    {
        if (isOpened)
        {
            blackPanel.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.0f, 0.1f);
            LeanTween.scale(itemIdentifier, new Vector3(0, 0, 0), 0.1f);
            Debug.Log("Collected " + name);
            Destroy(this.gameObject);
        }
    }
}
