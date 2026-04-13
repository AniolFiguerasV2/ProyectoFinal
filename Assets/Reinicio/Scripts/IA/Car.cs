using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    [SerializeField] private RutaCoches ruta;
    [SerializeField] private NavMeshAgent carnavmesh;
    [SerializeField] float distanceToAchieveCheckpoint = 3.0f;

    int nextWayPoint = -1;

    void Start()
    {
        GoToNextWaypoint();
    }

    void Update()
    {
        if (HasReachWayPoint())
            GoToNextWaypoint();
    }

    bool HasReachWayPoint()
    {
        return carnavmesh.remainingDistance < distanceToAchieveCheckpoint;
    }

    void GoToNextWaypoint()
    {
        nextWayPoint = (nextWayPoint + 1) % ruta.GetNumWaypoints();
        carnavmesh.SetDestination(ruta.GetWaypointPosition(nextWayPoint));
    }
}