using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public GameObject boardAlert;
    public GameObject ladybirdAlert;
    public GameObject elioAlert;
    public GameObject remAlert;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 0)
        {
            boardAlert.SetActive(true);
            ladybirdAlert.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ElioSolved", 0) == 0 && PlayerPrefs.GetInt("ElioActivated", 0) == 1)
        {
            boardAlert.SetActive(true);
            elioAlert.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("RemSolved", 0) == 0 && PlayerPrefs.GetInt("RemActivated", 0) == 1)
        {
            boardAlert.SetActive(true);
            remAlert.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
