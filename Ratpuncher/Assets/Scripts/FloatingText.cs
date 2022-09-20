using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public string[] floatingText;
    public Vector2[] positions;
    public int[] zRotations;
    private bool isTriggered;
    private List<GameObject> textObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < floatingText.Length; i++)
        {
            GameObject fText = Instantiate(Resources.Load("FloatingText") as GameObject);
            fText.transform.SetParent(gameObject.transform.GetChild(0));
            //int xPos = Random.Range(Mathf.RoundToInt(gameObject.transform.position.x - 2), Mathf.RoundToInt(gameObject.transform.position.x + 2));
            //int yPos = Random.Range(Mathf.RoundToInt(gameObject.transform.position.y), Mathf.RoundToInt(gameObject.transform.position.y + 4));
            fText.transform.position = new Vector3(positions[i].x,positions[i].y, 0);
            fText.transform.Rotate(0, 0, zRotations[i]);
            textObjects.Add(fText);
            fText.GetComponent<TextMeshProUGUI>().text = floatingText[i];
            fText.GetComponent<TextMeshProUGUI>().alpha = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isTriggered)
            {
                ShowText();
                isTriggered = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RemoveText();
            isTriggered = false;
        }
    }


    public void ShowText()
    {
        int delayTime = 0;
        var seq = LeanTween.sequence();
        foreach(GameObject go in textObjects)
        {
            seq.append(LeanTween.value(go, a => go.GetComponent<TextMeshProUGUI>().alpha = a, 0, 1, 1f).setLoopPingPong());
            //LeanTween.value(go, a => go.GetComponent<TextMeshProUGUI>().alpha = a, 0, 1, 1f).setDelay(delayTime).setLoopPingPong();
            delayTime += 1;
        }
    }

    public void RemoveText()
    {
        int delayTime = 0;
        foreach (GameObject go in textObjects)
        {
            LeanTween.cancel(go);
            LeanTween.value(go, a => go.GetComponent<TextMeshProUGUI>().alpha = a, 1, 0, 1f).setDelay(delayTime);
            delayTime += 1;
        }
    }

    private void ChangeState()
    {
        isTriggered = !isTriggered;
        Debug.Log(isTriggered);
    }
}
