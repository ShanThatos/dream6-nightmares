using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum VerticalState
    {
        Grounded,
        Jumping,
        Falling,
        Pounding
    }

    public enum PlayerActions
    {
        None,
        Dashing,
        Pounding,
        Attacking,
        Blocking
    }
}
