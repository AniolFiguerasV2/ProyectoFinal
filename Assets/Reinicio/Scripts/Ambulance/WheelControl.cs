using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public Transform wheelModel;

    [HideInInspector] public WheelCollider wheelCollider;
    public bool steerable;
    public bool motorized;

    [Header("Configuracion Derape")]
    [Range(0.5f, 3f)] public float sidewaysGrip = 2f; //Mas Alto = menos derrape
    [Range(0.5f, 3f)] public float forwardGrip = 1.5f; //Traccion hacia delante

    Vector3 position;
    Quaternion rotation;
    private void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
        ApplyGripSettings();
    }

    void Update()
    {
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation;
    }

    void ApplyGripSettings()
    {
       WheelFrictionCurve sideways = wheelCollider.sidewaysFriction;
    sideways.stiffness = sidewaysGrip;
    sideways.extremumSlip = 0.4f;
    sideways.extremumValue = 1f;
    sideways.asymptoteSlip = 0.8f;
    sideways.asymptoteValue = 0.5f;
    wheelCollider.sidewaysFriction = sideways;

    WheelFrictionCurve forward = wheelCollider.forwardFriction;
    forward.stiffness = forwardGrip;
    forward.extremumSlip = 0.4f;
    forward.extremumValue = 1f;
    forward.asymptoteSlip = 0.8f;
    forward.asymptoteValue = 0.5f;
    wheelCollider.forwardFriction = forward;
    }
}
