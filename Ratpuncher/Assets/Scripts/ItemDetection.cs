using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDetection : MonoBehaviour
{
    //public ItemSO itemSO;
    [TextArea(5, 10)]
    public string descriptionString;
    public Sprite itemSprite;

    private GameObject itemIdentifier;
    private GameObject blackPanel;
    private GameObject notification;
    private Image itemImage;
    private TextMeshProUGUI itemDescription;
    private TextMeshProUGUI itemName;
    private bool isInside;
    private bool isExist;
    private bool isOpened;
    private bool oldInput;
    private bool input;
    private GameObject actionIndicator;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        isInside = false;
        blackPanel = GameObject.FindGameObjectWithTag("BlackPanel");
        notification = GameObject.FindGameObjectWithTag("Notification");
        notification.GetComponentInChildren<TextMeshProUGUI>().text = "Press Space to Collect";
        itemIdentifier = GameObject.FindGameObjectWithTag("ItemIdentifier");
        itemImage = itemIdentifier.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        itemName = itemIdentifier.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        itemDescription = itemIdentifier.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        oldInput = input;
        input = Input.GetAxisRaw("Interact") > 0;
        if (isInside)
        {
            if (Input.GetAxisRaw("Submit") > 0)
            {
                Collect();
            }
            if (input && !oldInput)
            {
                if (!isOpened)
                {
                    OpenIdentifier();
                }
                else
                {
                    CloseIdentifier();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInside = true;
            if (!isExist)
            {
                actionIndicator = Instantiate(Resources.Load("ActionIndicator")) as GameObject;
                actionIndicator.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "F";
                actionIndicator.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Interact";
                actionIndicator.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                LeanTween.moveY(actionIndicator, this.gameObject.transform.position.y + 2, 0.2f);
                LeanTween.scaleY(actionIndicator, 1, 0.2f);
                isExist = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInside = false;
            //LeanTween.moveY(actionIndicator, this.gameObject.transform.position.y + 1, 0.2f);
            LeanTween.scaleY(actionIndicator, 0, 0.2f).setDestroyOnComplete(true);
            isExist = false;
            CloseIdentifier();
        }
    }

    public void OpenIdentifier()
    {
        /*
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            if (name == itemSO.items[i].itemName)
            {
                itemName.text = itemSO.items[i].itemName;
                itemDescription.text = itemSO.items[i].itemDescription;
                itemImage.sprite = itemSO.items[i].itemSprite;
                break;
            }
        }*/
        itemName.text = name;
        itemDescription.text = descriptionString;
        itemImage.sprite = itemSprite;
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
