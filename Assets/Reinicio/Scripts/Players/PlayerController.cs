using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public float speed = 5f;
    public float gravityMultiplier = 0.5f;
    public float rotationSpeed = 10f;
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

        if (movementVector == Vector2.zero)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0f, movementY);

        if (movement.magnitude > 0.01f)
        {
            movement = movement.normalized;

            Vector3 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        Vector3 gravityForce = Physics.gravity * gravityMultiplier;
        rb.AddForce(gravityForce, ForceMode.Acceleration);
    }
}