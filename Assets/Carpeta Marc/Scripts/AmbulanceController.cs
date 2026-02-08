using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class AmbulanceController : MonoBehaviour
{
    [Header("Ambulance Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steerinRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    private WheelControl[] wheels;
    private Rigidbody rb;

    private PlayerController carControls;

    void Awake()
    {
        carControls = new PlayerController();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 centerOfMass = rb.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rb.centerOfMass = centerOfMass;

        wheels = GetComponentsInChildren<WheelControl>();
    }

    void FixedUpdate()
    {
        //Vector2 inputVector = carControls.

        //float vInput = inputVector.y;
        //float hInput = inputVector.x;

        float forwardSpeed = Vector3.Dot(transform.forward, rb.linearVelocity);
        float speedFactor =Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steerinRange, steeringRangeAtMaxSpeed, speedFactor);

        //bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            if(wheel.steerable)
            {
                //wheel.wheelCollider.steerAngle = hInput * currentSteerRange;
            }
            /*
            if(isAccelerating)
            {
                if(wheel.motorized)
                {
                    wheel.WheelCollider.notorTorque = vInput * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
            */
        }
    }
    public void EnterVehicle(InteractPlayers player)
    {
        player.transform.SetParent(transform);
        //player.transform.position = seat.position;
        player.DrivenMode();
        //player.playervisual.SetActive(false);
        //player.movementscript.enabled = false;
    }

    public void ExitVehicle(InteractPlayers player)
    {
        player.transform.SetParent(null);
        //player.transform.position = exitPoint.position;
        player.WalkMode();
        //player.playervisual.SetActive(true);
        //player.movementscript.enabled = true;
    }


    void OnEnable()
    {
        carControls.enabled = true;
    }

    void OnDisable()
    {
        carControls.enabled = false;
    }
}
