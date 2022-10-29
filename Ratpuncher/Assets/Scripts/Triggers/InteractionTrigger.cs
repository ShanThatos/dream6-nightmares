using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionTrigger : MonoBehaviour
{
    private bool isInsideText;
    private GameObject interactingObject;
    private TextInteraction textScript;
    
    private Item itemScript;
    private bool isInsideItem;

    private TutorialPopup popup;
    private bool popupOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "InteractableText")
        {
            interactingObject = collision.gameObject;
            textScript = interactingObject.GetComponent<TextInteraction>();
            isInsideText = true;
        }
        if (collision.tag == "Item")
        {
            interactingObject = collision.gameObject;
            itemScript = interactingObject.GetComponent<Item>();
            isInsideItem = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "InteractableText")
        {
            isInsideText = false;
            if (textScript.textShown)
            {
                textScript.RemoveText();
            }
        }
        if (collision.tag == "Item")
        {
            isInsideItem = false;
            if (itemScript.isOpened)
            {
                itemScript.CloseIdentifier();
            }
        }
    }

    void OnInteract()
    {
        if (popupOpen)
        {
            //Block interaction if a tutorial popup is open
            return;
        }

        if (isInsideText)
        {
            if (!textScript.textShown)
            {
                textScript.ShowText();
            }
            else
            {
                textScript.RemoveText();
            }
        }
        if (isInsideItem)
        {
            if (!itemScript.isOpened)
            {
                itemScript.OpenIdentifier();
            }
            else
            {
                itemScript.CloseIdentifier();
            }
        }
    }


    public void openTutorialPopup(TutorialPopup pop)
    {
        popupOpen = true;
        popup = pop;
    }

    void OnJump()
    {
        if (isInsideItem)
        {
            itemScript.Collect();
        }
        if (popupOpen)
        {
            popup.Close();
            popup = null;
            popupOpen = false;
        }

    }

}
