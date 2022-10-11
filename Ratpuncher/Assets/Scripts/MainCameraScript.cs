using UnityEngine;

public class MainCameraScript : MonoBehaviour
{

    public float minX;
    public float maxX;

    public Vector2 cameraOffset;

    public Transform target;

    private float defaultCamFOV;
    public float camFOVMultiplier = 1f;

    public float defaultCamPosLerp = 8f;
    public float defaultCamFOVLerp = 80f;
    float camPosLerp;
    float camFOVLerp;

    Camera cam;

    public static MainCameraScript instance;

    void Start()
    {
        instance = this;
        cam = GetComponent<Camera>();
        defaultCamFOV = cam.fieldOfView;
        transform.position = new Vector3(target.position.x + cameraOffset.x, target.position.y + cameraOffset.y, transform.position.z);
        setCamPosLerp(defaultCamPosLerp);
        setCamFOVLerp(defaultCamFOVLerp);
    }

    // fixed update
    void FixedUpdate()
    {
        float oldZ = transform.position.z;
        Vector2 targetPos = (Vector2) target.position + cameraOffset;
        Vector2 newPos = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * camPosLerp);
        float halfCamFOV = cam.fieldOfView / 2;
        newPos.x = Mathf.Clamp(newPos.x, minX + halfCamFOV, maxX - halfCamFOV);
        transform.position = new Vector3(newPos.x, newPos.y, oldZ);

        float targetCamFOV = defaultCamFOV * camFOVMultiplier;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetCamFOV, Time.deltaTime * camFOVLerp);
    }

    public void setCamPosLerp(float lerp) {
        camPosLerp = lerp;
    }
    public void setCamFOVLerp(float lerp) {
        camFOVLerp = lerp;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minX, 10), new Vector3(minX, -10));
        Gizmos.DrawLine(new Vector3(maxX, 10), new Vector3(maxX, -10));
    }
}
