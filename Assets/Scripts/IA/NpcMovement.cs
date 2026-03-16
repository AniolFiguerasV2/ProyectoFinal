using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public List<GameObject> wayPoints;
    public float speed = 2;

    void Update()
    {
        int index = 0;
        Vector3 destination = wayPoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, wayPoints[index].transform.position, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.05f)
        {
            index++;
        }
    }   
}
