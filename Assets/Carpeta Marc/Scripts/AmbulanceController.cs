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
    private bool Allplayersin = false;
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

        float vInput = 0f;
        float hInput = 0f;

        //Movimeinto jugador 1 adelante y izquierda
        if(inputPlayer1.y > 0)
        {
            vInput += inputPlayer1.y;
        }
        if (inputPlayer1.x < 0)
        {
            hInput += inputPlayer1.x;
        }

        //Movimiento jugador 2 atras y derecha
        if(inputPlayer2.y < 0)
        {
            vInput += inputPlayer2.y;
        }
        if(inputPlayer2.x > 0)
        {
            hInput += inputPlayer2.x;
        }

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
        player.transform.SetParent(transform);
        player.DrivenMode();
        currentPlayerin++;
        if(currentPlayerin >= RequiredPlayerin)
        {
            Allplayersin = true;
        }
    }

    public void ExitVehicle(InteractPlayers player)
    {
        player.transform.SetParent(null);
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
