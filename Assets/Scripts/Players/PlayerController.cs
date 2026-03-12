using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public float speed = 5f;
    public float gravityMultiplier = 0.5f;
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
        animator.SetTrigger("Walking");
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        movement = movement.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + rb.transform.TransformDirection(movement));

        Vector3 gravityForce = Physics.gravity * gravityMultiplier;
        rb.AddForce(gravityForce, ForceMode.Acceleration);
    }
}
