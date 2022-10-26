using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedTransform : MonoBehaviour {

    public bool showBounds = true;
    public bool relativeToStart = true;

    public float MIN_X;
    public float MAX_X;
    public float MIN_Y;
    public float MAX_Y;

    float actualMinX;
    float actualMaxX;
    float actualMinY;
    float actualMaxY;


    private void refreshBounds() {
        float xOffset = relativeToStart ? transform.position.x : 0;
        float yOffset = relativeToStart ? transform.position.y : 0;
        actualMinX = MIN_X + xOffset;
        actualMaxX = MAX_X + xOffset;
        actualMinY = MIN_Y + yOffset;
        actualMaxY = MAX_Y + yOffset;
    }

    private void Start() {
        refreshBounds();
    }

    private void OnValidate() {
        refreshBounds();
    }

    private void Update() {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, actualMinX, actualMaxX);
        pos.y = Mathf.Clamp(pos.y, actualMinY, actualMaxY);
        transform.position = pos;
    }

    private void OnDrawGizmosSelected() {
        if (!showBounds) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(actualMinX, actualMinY), new Vector3(actualMaxX, actualMinY));
        Gizmos.DrawLine(new Vector3(actualMaxX, actualMinY), new Vector3(actualMaxX, actualMaxY));
        Gizmos.DrawLine(new Vector3(actualMaxX, actualMaxY), new Vector3(actualMinX, actualMaxY));
        Gizmos.DrawLine(new Vector3(actualMinX, actualMaxY), new Vector3(actualMinX, actualMinY));
    }

    float EPSILON = 0.25f;
    public bool isNearBounds() {
        return Mathf.Abs(transform.position.x - actualMinX) < EPSILON
            || Mathf.Abs(transform.position.x - actualMaxX) < EPSILON
            || Mathf.Abs(transform.position.y - actualMinY) < EPSILON
            || Mathf.Abs(transform.position.y - actualMaxY) < EPSILON;
    }

    public bool isNearLeftBound() {
        return Mathf.Abs(transform.position.x - actualMinX) < EPSILON;
    }

    public bool isNearRightBound() {
        return Mathf.Abs(transform.position.x - actualMaxX) < EPSILON;
    }
}
