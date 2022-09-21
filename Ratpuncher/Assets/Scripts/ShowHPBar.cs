using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHPBar : MonoBehaviour
{


    public GameObject HPBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.layer == LayerMask.NameToLayer("Player"))
        {
            HPBar.SetActive(true);
        }

        enabled = false;
    }
}
