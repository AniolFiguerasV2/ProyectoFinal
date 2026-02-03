using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 viewportPos = mainCam.WorldToViewportPoint(transform.position);

        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);

        transform.position = mainCam.ViewportToWorldPoint(viewportPos);
    }
}
