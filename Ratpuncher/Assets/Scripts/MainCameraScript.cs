using UnityEngine;

public class MainCameraScript : MonoBehaviour
{

    public float minX;
    public float maxX;

    public Vector2 cameraOffset;

    public Transform target;

    private float defaultCamSize;
    public float camSizeMultiplier = 1f;

    Camera cam;

    public static MainCameraScript instance;

    void Start()
    {
        instance = this;
        cam = GetComponent<Camera>();
        defaultCamSize = cam.orthographicSize;
    }

    // fixed update
    void FixedUpdate()
    {
        float oldZ = transform.position.z;
        Vector2 targetPos = (Vector2) target.position + cameraOffset;
        Vector2 newPos = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 4);
        float halfCamWidth = cam.orthographicSize * cam.aspect;
        newPos.x = Mathf.Clamp(newPos.x, minX + halfCamWidth, maxX - halfCamWidth);
        transform.position = new Vector3(newPos.x, newPos.y, oldZ);

        float targetCamSize = defaultCamSize * camSizeMultiplier;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetCamSize, Time.deltaTime * 4);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minX, 10), new Vector3(minX, -10));
        Gizmos.DrawLine(new Vector3(maxX, 10), new Vector3(maxX, -10));
    }
}
