using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialIndicator : MonoBehaviour
{
    public string key;
    public string action;
    private bool isTriggered;
    private GameObject actionIndicator;
    // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isTriggered)
            {
                actionIndicator = Instantiate(Resources.Load("ActionIndicator")) as GameObject;
                actionIndicator.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = key;
                actionIndicator.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = action;
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
