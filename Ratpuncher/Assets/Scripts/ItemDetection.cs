using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDetection : MonoBehaviour
{
    public ItemSO itemSO;
    private GameObject itemIdentifier;
    private GameObject blackPanel;
    private GameObject notification;
    private Image itemImage;
    private TextMeshProUGUI itemDescription;
    private TextMeshProUGUI itemName;
    private bool isInside;
    // Start is called before the first frame update
    void Start()
    {
        blackPanel = GameObject.FindGameObjectWithTag("BlackPanel");
        notification = GameObject.FindGameObjectWithTag("Notification");
        notification.GetComponentInChildren<TextMeshProUGUI>().text = "Press Space to Collect";
        itemIdentifier = GameObject.FindGameObjectWithTag("ItemIdentifier");
        itemImage = itemIdentifier.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        itemName = itemIdentifier.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        itemDescription = itemIdentifier.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.Space))
        {
            blackPanel.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.0f, 0.1f);
            LeanTween.scale(itemIdentifier, new Vector3(0, 0, 0), 0.1f);
            Debug.Log("Collected " + name);
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            for (int i = 0; i < itemSO.items.Length; i++)
            {
                if (name == itemSO.items[i].itemName)
                {
                    itemName.text = itemSO.items[i].itemName;
                    itemDescription.text = itemSO.items[i].itemDescription;
                    itemImage.sprite = itemSO.items[i].itemSprite;
                    break;
                }
            }
            LeanTween.scaleY(notification, 1, 0.1f);
            blackPanel.transform.localScale = new Vector3(1, 1, 1);
            LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.75f, 0.1f);
            LeanTween.scale(itemIdentifier, new Vector3(1, 1, 1), 0.1f);
            isInside = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LeanTween.scaleY(notification, 0, 0.1f); 
            blackPanel.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.alpha(blackPanel.GetComponent<RectTransform>(), 0.0f, 0.1f);
            LeanTween.scale(itemIdentifier, new Vector3(0, 0, 0), 0.1f);
            isInside = false;
        }
    }
}
