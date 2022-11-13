using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheck : MonoBehaviour
{
    private Animator anim;
    private Animator sceneAnim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sceneAnim = ScenesTransition.instance.gameObject.GetComponent<Animator>();
        if (PlayerPrefs.GetInt("Failed", 0) == 1)
        {
            anim.speed = 10f;
            sceneAnim.speed = 10f;
            Debug.Log("Super scrolling!");
        }
    }

    public void MoveOn()
    {
        if (PlayerPrefs.GetInt("Failed", 0) == 1)
        {
            ScenesTransition.instance.LoadScene("Office");
        }
        else
        {
            ScenesTransition.instance.LoadScene("MainMenu");
        }
    }
}
