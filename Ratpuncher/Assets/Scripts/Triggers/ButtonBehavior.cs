using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour , ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = new Color32(50, 50, 50, 255);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = new Color32(50, 50, 50, 255);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }
    public void SelectButton(GameObject go)
    {
        go.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(50, 50, 50, 255);
        //go.transform.Translate(Vector3.right*0.5f);
    }

    public void DeselectButton(GameObject go)
    {
        go.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        //go.transform.Translate(Vector3.left*0.5f);
    }

}
