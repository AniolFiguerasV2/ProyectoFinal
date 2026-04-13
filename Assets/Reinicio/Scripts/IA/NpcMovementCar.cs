using UnityEngine;

public class NpcMovementCar : MonoBehaviour
{
    float minimumDistance = 0.2f;

    public float speed = 2.5f;
    public float rotationSpeed = 6f;

    [Header("Pathfinding Parameters")]
    [SerializeField]
    float findWaypointRadius = 10f;
    [SerializeField]
    [UnityEngine.Range(0f, 1f)]
    float alignmentPriority = 0.7f;
    [SerializeField]
    [UnityEngine.Range(0f, 1f)]
    float distancePriority = 0.3f;

    Transform destination;

    void Start()
    {
        FindNextDestination();
    }

    private void FindNextDestination()
    {
        destination = null;

        Collider[] waypoints = Physics.OverlapSphere(
            transform.position,
            findWaypointRadius,
            LayerMask.GetMask("WaypointCars")
        );

        float bestScore = float.MinValue;

        foreach (var item in waypoints)
        {
            Vector3 dir = item.transform.position - transform.position;
            float dist = dir.magnitude;

            if (dist < minimumDistance)
                continue;

            dir.Normalize();

            float alignment = Vector3.Dot(transform.forward, dir);

            // Closer = better score
            float distanceScore = (alignmentPriority + distancePriority) - (dist / findWaypointRadius) * (alignmentPriority + distancePriority);

            float score = alignment * alignmentPriority + distanceScore * distancePriority;

            if (score > bestScore)
            {
                bestScore = score;
                destination = item.transform;
            }
        }

    }

    void Update()
    {
        if (destination == null)
            return;

        Vector3 dir = (destination.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, destination.position) <= minimumDistance)
        {
            FindNextDestination();
        }
    }
}
