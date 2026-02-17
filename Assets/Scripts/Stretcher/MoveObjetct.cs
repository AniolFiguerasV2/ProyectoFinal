using UnityEditor;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Handles handle1;
    public Handles handle2;

    public Transform middleObject;
    public Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (handle1.IsBeingHeld && handle2.IsBeingHeld)
        {
            transform.position = middleObject.position;
            transform.rotation = middleObject.rotation;
            body.linearVelocity = Vector3.zero;
        }
    }
}
