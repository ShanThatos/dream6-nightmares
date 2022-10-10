using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialIndicator : MonoBehaviour
{

    public InputActionReference inputAction;
    public bool isDoublePress;

    public string keyOverride;
    public string actionOverride;
    private bool isTriggered;
    private GameObject actionIndicator;
    // Start is called before the first frame update
    private void Start()
    {
        KeyChange();
    }
    private void KeyChange()
    {
        if (inputAction != null)
        {
            if (string.IsNullOrWhiteSpace(actionOverride))
            {
                actionOverride = inputAction.action.name;

            }

            if (string.IsNullOrWhiteSpace(keyOverride))
            {
                if (actionOverride.ToUpper() == "MOVE")
                {
                    // Need to handle a special case for movement since its a composite
                    keyOverride = inputAction.action.GetBindingDisplayString(5);
                }
                else
                {
                    keyOverride = inputAction.action.GetBindingDisplayString();
                }

                if (isDoublePress)
                {
                    keyOverride += "x2";
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            KeyChange();
            if (!isTriggered)
            {
                actionIndicator = Instantiate(Resources.Load("ActionIndicator")) as GameObject;
                actionIndicator.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = keyOverride;
                actionIndicator.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = actionOverride;
                actionIndicator.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                LeanTween.moveY(actionIndicator, this.gameObject.transform.position.y + 1, 0.2f);
                LeanTween.scaleY(actionIndicator, 1, 0.2f);
                isTriggered = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTriggered = false;
            LeanTween.scaleY(actionIndicator, 0, 0.2f).setDestroyOnComplete(true);
        }
    }
}
