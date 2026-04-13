using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RutaCoches : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;

    public Vector3 GetWaypointPosition(int index)
    {
        if (index < 0 || index >= waypoints.Count)
            return Vector3.zero;

        return waypoints[index].position;
    }

    public int GetNumWaypoints() => waypoints.Count;
}
