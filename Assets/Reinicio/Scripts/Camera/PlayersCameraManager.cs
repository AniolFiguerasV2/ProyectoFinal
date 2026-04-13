using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCameraManager : MonoBehaviour
{
    public Transform p1, p2;
    public float smoothTime = 0.5f;
    private Vector3 velocity;

    private void FixedUpdate()
    {
        Vector3 centerPoint = GetCenterPoint();
        centerPoint.y = 0;
        transform.position = Vector3.SmoothDamp(transform.position, centerPoint, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        Vector3 distanceVector = p2.position - p1.position;
        distanceVector *= 0.5f;

        return p1.position + distanceVector;
    }
}
