using UnityEngine;

public class AmbulanceCamera : MonoBehaviour
{
    [Header("Ambulance Needs")]
    public Transform ambulance;
    public Rigidbody rb;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 4f, -8f);
    public float positionSmoothTime = 0.2f;
    public float rotationSmoothTime = 0.1f;
    public float lookAheadDistance = 5f;
    public float minSpeedForLookAhead = 2f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = ambulance.position + ambulance.rotation * offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, positionSmoothTime);

        Vector3 moveDirection = rb.linearVelocity;

        if (moveDirection.magnitude < minSpeedForLookAhead)
        {
            moveDirection = ambulance.forward;
        }
        else
        {
            moveDirection.Normalize();
        }

        Vector3 lookTarget = ambulance.position + moveDirection * lookAheadDistance;

        Quaternion targetRotation = Quaternion.LookRotation(lookTarget - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime * 10f * Time.deltaTime);
    }
}
/*
 * Esta script se utiliza para la camara de la ambulancia.
 * 
 * Este script se tiene que poner en un game object vacio que dentro tenga la camara que se utilizara.
 */