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

    public GameObject spawnpoint;

    private WheelControl[] wheels;
    private Rigidbody rb;
    [SerializeField] public bool Allplayersin = false;
    private int currentPlayerin = 0;
    public int RequiredPlayerin = 2;
    private PlayerActions carControls;

    void Awake()
    {
        carControls = new PlayerActions();
    }

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        Vector3 centerOfMass = rb.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rb.centerOfMass = centerOfMass;

        wheels = GetComponentsInChildren<WheelControl>();
    }

    void FixedUpdate()
    {
        if (!Allplayersin)
        {
            return;
        }

        Vector2 inputPlayer1 = carControls.Player1.Move.ReadValue<Vector2>();
        Vector2 inputPlayer2 = carControls.Player2.Move.ReadValue<Vector2>();


        float vInput = inputPlayer1.y;
        float hInput = inputPlayer2.x;

        float forwardSpeed = Vector3.Dot(transform.forward, rb.linearVelocity);
        float speedFactor =Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steerinRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            if(wheel.steerable)
            {
                wheel.wheelCollider.steerAngle = hInput * currentSteerRange;
            }
            if(isAccelerating)
            {
                if(wheel.motorized)
                {
                    wheel.wheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                wheel.wheelCollider.brakeTorque = 0f;
            }
            else
            {
                wheel.wheelCollider.motorTorque = 0f;
                wheel.wheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
        }
    }
    public void EnterVehicle(InteractPlayers player)
    {
        player.transform.position = spawnpoint.transform.position;
        player.DrivenMode();
        currentPlayerin++;
        if(currentPlayerin >= RequiredPlayerin)
        {
            Allplayersin = true;
        }
    }

    public void ExitVehicle(InteractPlayers player)
    {
        player.transform.position = player.currentEntry.transform.position;
        player.WalkMode();
        currentPlayerin--;
        Allplayersin = false;
    }


    void OnEnable()
    {
        carControls.Enable();
    }

    void OnDisable()
    {
        carControls.Disable();
    }
}
