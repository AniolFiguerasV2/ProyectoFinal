using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PacienteInfo : MonoBehaviour
{
    public Image spritePaciente;
    public Image barhealth;

    private PatientDeathTime patient;
    public void Init(PatientDeathTime p)
    {
        patient = p;
    }
    
    void Update()
    {
        if (patient == null) return;

        float remaining = patient.Lifetime - patient.Timer;
        remaining = Mathf.Max(0, remaining);

        barhealth.fillAmount = remaining / patient.Lifetime;
    }
}
