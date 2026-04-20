using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    [SerializeField] private RutaCoches ruta;
    [SerializeField] int nextWayPoint = 0;

    [SerializeField] private NavMeshAgent carnavmesh;
    [SerializeField] float distanceToAchieveCheckpoint = 3.0f;

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
        carnavmesh.SetDestination(ruta.GetWaypointPosition(nextWayPoint));
        nextWayPoint = (nextWayPoint + 1) % ruta.GetNumWaypoints();
    }
}