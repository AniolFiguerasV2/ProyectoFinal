using System.Collections.Generic;
using UnityEngine;

public class VehicleMovements : MonoBehaviour
{
    List<Transform> wayPoints;

    public float speed = 6f;

    private int index = 0;

    public void SetWayPoints(List<Transform> points)
    {
        wayPoints = points;
    }

    void Update()
    {
        if (wayPoints == null || wayPoints.Count == 0) return;

        Transform target = wayPoints[index];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        transform.LookAt(target);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= 0.2f)
        {
            index++;

            if (index >= wayPoints.Count)
            {
                Destroy(gameObject);
            }
        }
    }
}
