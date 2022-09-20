using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Smoothing {
    public static float EaseInOut(float t) {
        return Mathf.Clamp(t * t * (3 - 2 * t), 0, 1);
    }
}