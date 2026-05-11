using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    float minimumDistance = 0.2f;

    public float speed = 2.5f;
    public float rotationSpeed = 6f;

    [Header("Pathfinding Parameters")]
    [SerializeField]
    float findWaypointRadius = 10f;

    [SerializeField]
    [Range(0f, 1f)]
    float alignmentPriority = 0.7f;

    [SerializeField]
    [Range(0f, 1f)]
    float distancePriority = 0.3f;

    Transform destination;

    Rigidbody rb;

    bool launched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // NPC estable mientras camina
        rb.isKinematic = true;

        FindNextDestination();
    }

    void Update()
    {
        if (destination == null || launched)
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

    private void FindNextDestination()
    {
        destination = null;

        Collider[] waypoints = Physics.OverlapSphere(
            transform.position,
            findWaypointRadius,
            LayerMask.GetMask("Waypoint")
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

            float distanceScore =
                (alignmentPriority + distancePriority)
                - (dist / findWaypointRadius)
                * (alignmentPriority + distancePriority);

            float score =
                alignment * alignmentPriority
                + distanceScore * distancePriority;

            if (score > bestScore)
            {
                bestScore = score;
                destination = item.transform;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (launched)
            return;

        if (collision.gameObject.CompareTag("Ambulance"))
        {
            launched = true;

            // ACTIVAR físicas
            rb.isKinematic = false;

            Vector3 forceDir =
                (transform.position - collision.transform.position).normalized;

            float crashForce = collision.relativeVelocity.magnitude;

            rb.AddForce(
                forceDir * crashForce * 6f + Vector3.up * 5f,
                ForceMode.Impulse
            );

            rb.AddTorque(
                Random.insideUnitSphere * 15f,
                ForceMode.Impulse
            );
        }
    }
}