using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private float rotationSpeed = 10f;

    private Quaternion initialPos;

    private void Start()
    {
        initialPos = transform.rotation;
    }

    public void SetTarget(Transform newTarget)
    {
        arrow = newTarget;
    }

    void Update()   
    {
        if (arrow != null)
        {
            RotateTowardsTarget();
        }
        else
        {
            ReturnToInitialRotation();
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = arrow.position - transform.position;
        direction.y = 0f;

        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ReturnToInitialRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, initialPos, rotationSpeed * Time.deltaTime);
    }
}
