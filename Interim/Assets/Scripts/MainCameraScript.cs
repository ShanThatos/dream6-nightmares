using UnityEngine;
using System;

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

    public CameraSetting[] fixedSettings;
    CameraSetting defaultSetting;

    [Serializable]
    public class CameraSetting {
        public Transform targetOverride;
        public Vector2 cameraOffsetOverride;
        public float fovMultiplierOverride;
        public float camPosLerpOverride;
        public float camFOVLerpOverride;
    }


    public static MainCameraScript instance;

    void Start()
    {
        instance = this;
        cam = GetComponent<Camera>();
        defaultCamFOV = cam.fieldOfView;
        transform.position = new Vector3(target.position.x + cameraOffset.x, target.position.y + cameraOffset.y, transform.position.z);
        setCamPosLerp(defaultCamPosLerp);
        setCamFOVLerp(defaultCamFOVLerp);

        defaultSetting = new CameraSetting();
        defaultSetting.targetOverride = target;
        defaultSetting.cameraOffsetOverride = cameraOffset;
        defaultSetting.fovMultiplierOverride = camFOVMultiplier;
        defaultSetting.camPosLerpOverride = defaultCamPosLerp;
        defaultSetting.camFOVLerpOverride = defaultCamFOVLerp;
    }

    // fixed update
    void FixedUpdate()
    {
        float oldZ = transform.position.z;
        Vector2 targetPos = (Vector2) target.position + cameraOffset;
        Vector2 newPos = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * camPosLerp);


        float halfCamWidth = Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2) * (-transform.position.z);
        newPos.x = Mathf.Clamp(newPos.x, minX + halfCamWidth, maxX - halfCamWidth);
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


    public void useSetting(CameraSetting setting) {
        if (setting.targetOverride != null) target = setting.targetOverride;
        cameraOffset = setting.cameraOffsetOverride;
        if (setting.fovMultiplierOverride != 0) camFOVMultiplier = setting.fovMultiplierOverride;   
        if (setting.camPosLerpOverride != 0) setCamPosLerp(setting.camPosLerpOverride);
        if (setting.camFOVLerpOverride != 0) setCamFOVLerp(setting.camFOVLerpOverride);
    }
    public void useSetting(int index) {
        if (index < 0 || index >= fixedSettings.Length) return;
        useSetting(fixedSettings[index]);
    }

    public void useDefaultPositioning() {
        target = defaultSetting.targetOverride;
        cameraOffset = defaultSetting.cameraOffsetOverride;
    }

    public void useDefaultSetting() {
        useSetting(defaultSetting);
    }
}
