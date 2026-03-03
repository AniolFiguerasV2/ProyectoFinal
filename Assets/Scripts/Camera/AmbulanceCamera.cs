using UnityEngine;

public class AmbulanceCamera : MonoBehaviour
{
    public Transform ambulance;
    public Vector3 offset = new Vector3(0, 0, -1);
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        Vector3 desiredPosition = ambulance.TransformPoint(offset);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(ambulance);
    }
}
