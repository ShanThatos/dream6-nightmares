using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField]
    Damagable leverHitbox;

    [SerializeField]
    Animator doorAnim;

    [SerializeField]
    GameObject[] toBeToggled;

    // Start is called before the first frame update
    void Start()
    {
        leverHitbox.OnDeath += OnActivate;
    }

    void OnActivate()
    {
        doorAnim.SetTrigger("Active");

        foreach(GameObject obj in toBeToggled)
        {
            bool isActive = obj.activeInHierarchy;
            obj.SetActive(!isActive);
        }
    }
}
