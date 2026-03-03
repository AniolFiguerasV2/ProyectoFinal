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
        //Controla el derrape
        WheelFrictionCurve sideways = wheelCollider.sidewaysFriction;
        sideways.stiffness = sidewaysGrip;
        wheelCollider.sidewaysFriction = sideways;

        //Ajusta agarre hacia delante
        WheelFrictionCurve forward = wheelCollider.forwardFriction;
        forward.stiffness = forwardGrip;
        wheelCollider.forwardFriction = forward;
    }
}
