using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : MonoBehaviour
{
    public float chargeTime;
    public float delay;
    public float force;

    public LineRenderer targetLine;
    public GameObject javelin;

    Transform player;
}
