using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerImpact : MonoBehaviour
{
    public Seeker mainScript;
    public Damagable dmg;

    private void Start()
    {
        if (dmg)
        {
            dmg.OnDeath += Detonate;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Detonate();
    }

    private void Detonate()
    {
        mainScript.Detonate();
    }
}
