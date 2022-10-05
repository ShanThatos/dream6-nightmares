using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimStart : MonoBehaviour
{

    public int minDelay = 20;
    public int maxDelay = 40;
    int delayFrames;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        delayFrames = Random.Range(minDelay, maxDelay);
        print(delayFrames);
    }

    private void Update()
    {
        delayFrames--;
        if(delayFrames <= 0)
        {
            anim.SetTrigger("start");
            this.enabled = false;
        }
    }
}
