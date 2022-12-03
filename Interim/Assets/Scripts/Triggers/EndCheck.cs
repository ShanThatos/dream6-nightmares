using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheck : MonoBehaviour
{
    private bool isDone;
    // Start is called before the first frame update
    void Start()
    {
        isDone = false;
    }

    private void Update()
    {
        if (isDone)
        {
            if (Input.anyKeyDown)
            {
                ScenesTransition.instance.LoadScene("MainMenu");
            }
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
            //ScenesTransition.instance.LoadScene("MainMenu");
            isDone = true;

        }
    }
}
