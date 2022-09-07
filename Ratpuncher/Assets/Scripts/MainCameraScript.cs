using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{

    public float minX;
    public float maxX;

    public Vector2 playerCameraOffset;

    Camera cam;

    public static MainCameraScript instance;

    void Start()
    {
        instance = this;
        cam = GetComponent<Camera>();
    }

    // fixed update
    void FixedUpdate()
    {
        float oldZ = transform.position.z;
        Vector2 targetPos = (Vector2) GameManager.getPlayerTransform().position + playerCameraOffset;
        Vector2 newPos = Vector2.Lerp(transform.position, targetPos, Mathf.Clamp(Time.deltaTime * 4, 0, 1));
        float halfCamWidth = cam.orthographicSize * cam.aspect;
        newPos.x = Mathf.Clamp(newPos.x, minX + halfCamWidth, maxX - halfCamWidth);
        transform.position = new Vector3(newPos.x, newPos.y, oldZ);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minX, 10), new Vector3(minX, -10));
        Gizmos.DrawLine(new Vector3(maxX, 10), new Vector3(maxX, -10));
    }
}
