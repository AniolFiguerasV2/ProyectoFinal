using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //public List<Transform> targets;
    public Transform p1, p2;
    public float smoothTime = 0.5f;
    private Vector3 velocity;

    private void LateUpdate()
    {
        Vector3 centerPoint = GetCenterPoint();
        centerPoint.y = 0;
        transform.position = Vector3.SmoothDamp(transform.position, centerPoint, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        /*if (targets.Count == 1) return targets[0].position;

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;*/
        //BoxCollider c;

        //c.closedpoint


        Vector3 distanceVector = p2.position - p1.position;
        distanceVector *= 0.5f;

        return p1.position + distanceVector;
    }
}
