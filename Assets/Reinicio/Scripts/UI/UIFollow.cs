using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Camera cam;

    private void LateUpdate()
    {
        if (target == null || cam == null) return;

        Vector3 screenPos = cam.WorldToScreenPoint(target.position + offset);
        transform.position = screenPos;
    }
}
