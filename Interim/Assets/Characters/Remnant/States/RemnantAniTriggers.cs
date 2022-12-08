using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemnantAniTriggers : MonoBehaviour
{
    public GameObject drill;
    public Transform drillPoint;
    public void SpawnOverheadDrill()
    {
        Instantiate(drill, drillPoint.position, Quaternion.identity);
    }
}