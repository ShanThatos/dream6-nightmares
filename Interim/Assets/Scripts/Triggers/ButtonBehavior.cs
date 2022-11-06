using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour , ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void Start()
    {
        if (!this.gameObject.GetComponent<Button>().interactable)
        {
            try
            {
                GetComponentInChildren<TextMeshProUGUI>().color = new Color32(50, 50, 50, 255);
            }
            catch
            {
                GetComponentInChildren<Text>().color = new Color32(50, 50, 50, 255);
            }
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (this.gameObject.GetComponent<Button>().interactable)
        {
            try
            {
                GetComponentInChildren<TextMeshProUGUI>().color = new Color32(50, 50, 50, 255);
            }
            catch
            {
                GetComponentInChildren<Text>().color = new Color32(50, 50, 50, 255);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.GetComponent<Button>().interactable)
        {
            try
            {
                GetComponentInChildren<TextMeshProUGUI>().color = new Color32(50, 50, 50, 255);
            }
            catch
            {
                GetComponentInChildren<Text>().color = new Color32(50, 50, 50, 255);
            }
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (this.gameObject.GetComponent<Button>().interactable)
        {
            try
            {
                GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
            catch
            {
                GetComponentInChildren<Text>().color = Color.white;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject.GetComponent<Button>().interactable)
        {
            try
            {
                GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
            catch
            {
                GetComponentInChildren<Text>().color = Color.white;
            }
        }
    }
    public void SelectButton(GameObject go)
    {
        if (this.gameObject.GetComponent<Button>().interactable)
        {
            try
            {
                go.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(50, 50, 50, 255);
            }
            catch
            {
                go.GetComponentInChildren<Text>().color = new Color32(50, 50, 50, 255);
            }
        }
        //go.transform.Translate(Vector3.right*0.5f);
    }


    public void DeselectButton(GameObject go)
    {
        if (this.gameObject.GetComponent<Button>().interactable)
        {
            try
            {
                go.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
            catch
            {
                go.GetComponentInChildren<Text>().color = Color.white;
            }
        }
        //go.transform.Translate(Vector3.left*0.5f);
    }

}
