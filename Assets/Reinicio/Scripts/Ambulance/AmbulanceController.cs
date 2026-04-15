using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmbulanceController : MonoBehaviour
{
    [Header("Ambulance Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 90f;
    public float steerinRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    [Header("Auto Frenado")]
    public float autoBrakeForce = 3000f; //Fuerza del frenado automatico
    public float stopThreshold = 0.5f; //Velocidad minima para ser considerado ser detenido

    public GameObject spawnpoint;

    private WheelControl[] wheels;
    private Rigidbody rb;

    private bool p1Controlstearing;

    public bool autoBraking = false;
    private int currentPlayerin = 0;
    public int RequiredPlayerin = 2;

    public StartTutorialManager tutorialManager;

    float vInput = 0;
    float hInput = 0;

    private bool _allplayersin;
    public bool Allplayersin
    {
        get => _allplayersin;
        set
        {
            if (value)
            {
                CamerasManager.ActiveAmbulanceCamera();
            }
            else
            {
                CamerasManager.ActivePlayersCamera();
            }
            _allplayersin = value;
        }
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
        vInput = 0f;
        hInput = 0f;

        if (Allplayersin)
        {
            Vector2 inputP1 = InputManager.Instance.GetMoveAxis(1);
            Vector2 inputP2 = InputManager.Instance.GetMoveAxis(2);

            if (p1Controlstearing)
            {
                hInput = inputP1.x;
                vInput = inputP2.y;
            }
            else
            {
                hInput = inputP2.x;
                vInput = inputP1.y;
            }
        }
        if (!Allplayersin)
        {
            if (autoBraking)
            {
                ApplyAutoBrake();
            }
            return;
        }

        float forwardSpeed = Vector3.Dot(transform.forward, rb.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steerinRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            if (wheel.steerable)
            {
                wheel.wheelCollider.steerAngle = hInput * currentSteerRange;
            }
            if (isAccelerating)
            {
                if (wheel.motorized)
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

    //Funcion para detener la ambulancia
    void ApplyAutoBrake()
    {
        float speed = rb.linearVelocity.magnitude;

        if (speed > stopThreshold)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = 0f;
                wheel.wheelCollider.brakeTorque = autoBrakeForce;
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0f;
            }

            autoBraking = false;
        }
    }


    public void EnterVehicle(InteractPlayers player, bool controlsstearing)
    {
        player.transform.position = rb.transform.position;
        player.DrivenMode();
        player.transform.parent = rb.transform;

        player.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        player.playervisual.SetActive(false);

        if(player.currentEntry != null)
        {
            if(player.CompareTag("Player1") && player.currentEntry.player1UI != null)
            {
                player.currentEntry.player1UI.SetActive(false);
            }
            if(player.CompareTag("Player2")  && player.currentEntry.player2UI != null)
            {
                player.currentEntry.player2UI.SetActive(false);
            }
        }

        currentPlayerin++;
        if (currentPlayerin >= RequiredPlayerin)
        {
            Allplayersin = true;
            autoBraking = false;
            if (player.CompareTag("Player1") && controlsstearing)
            {
                p1Controlstearing = true;
            }
            else
            {
                p1Controlstearing = false;
            }
            if (tutorialManager != null)
            {
                tutorialManager.ShowDrivingTutorial();
            }
        }
    }

    public void ExitVehicle(InteractPlayers player)
    {
        player.transform.position = player.currentEntry.transform.position;
        player.WalkMode();
        player.transform.parent = null;

        player.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        player.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        player.playervisual.SetActive(true);

        if (player.currentEntry != null && player.currentEntry.Available)
        {
            if (player.CompareTag("Player1") && player.currentEntry.player1UI != null)
            {
                player.currentEntry.player1UI.SetActive(true);
            }

            if (player.CompareTag("Player2") && player.currentEntry.player2UI != null)
            {
                player.currentEntry.player2UI.SetActive(true);
            }
                
        }

        currentPlayerin--;

        if (currentPlayerin < RequiredPlayerin)
        {
            Allplayersin = false;
            autoBraking = true;
        }

    }
}
