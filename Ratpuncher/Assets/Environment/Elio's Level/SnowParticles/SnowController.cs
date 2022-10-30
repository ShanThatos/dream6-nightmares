using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowController : MonoBehaviour
{

    Animator animator;
    public bool active;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (!active)
        {
            animator.SetTrigger("FadeOut");
        }
    }
    public void FadeIn()
    {
        if (active)
        {
            return;
        }

        active = true;
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        if (!active)
        {
            return;
        }
        active = false;
        animator.SetTrigger("FadeOut");
    }
}
