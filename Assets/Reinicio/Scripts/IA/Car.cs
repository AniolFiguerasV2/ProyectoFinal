using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    [SerializeField] private RutaCoches ruta;
    [SerializeField] int nextWayPoint = 0;

    [SerializeField] private NavMeshAgent carnavmesh;
    [SerializeField] float distanceToAchieveCheckpoint = 3.0f;

    [SerializeField] float maxSpeed = 10.0f;

    public float knockback = 1.5f;

    Rigidbody rb;
    Collider coll;

    void Start()
    {
        GoToNextWaypoint();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        if (HasReachWayPoint())
            GoToNextWaypoint();

        Vector3 directionVector = (carnavmesh.steeringTarget - carnavmesh.transform.position).normalized;
        float dotProduct = Vector3.Dot(carnavmesh.transform.forward, directionVector);
        float factor = Mathf.Max(Mathf.Abs(dotProduct), 0.5f);
        float maxSpeedPerDirection = maxSpeed * factor;

        float maxSpeedPerTargetDistance = carnavmesh.remainingDistance > 10.0f ? maxSpeed : maxSpeed * 0.5f;

        carnavmesh.speed = Mathf.Min(maxSpeedPerDirection, maxSpeedPerTargetDistance);
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ambulancia"))
        {
            carnavmesh.enabled = false;
            coll.isTrigger = false;
            rb.isKinematic = false;
            rb.linearVelocity = (transform.position - collision.transform.position).normalized * collision.collider.attachedRigidbody.linearVelocity.magnitude * knockback;
        }
    }

}