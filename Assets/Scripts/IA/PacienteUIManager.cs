using UnityEngine;

public class PacienteUIManager : MonoBehaviour
{
    [SerializeField] private PatientSpawner spawner;
    [SerializeField] private GameObject pacienteWidgerPrefab;
    [SerializeField] private Transform widgetParent;

    private int lastPatientCount = 0;

    void Update()
    {
        if (spawner == null) return;

        if (spawner.patients.Count > lastPatientCount)
        {
            for (int i = lastPatientCount; i < spawner.patients.Count; i++)
            {
                CreateWidget(spawner.patients[i]);
            }

            lastPatientCount = spawner.patients.Count;
        }
    }
    void CreateWidget(PatientDeathTime patient)
    {
        if (patient == null) return;

        GameObject widget = Instantiate(pacienteWidgerPrefab, widgetParent);

        PacienteInfo info = widget.GetComponent<PacienteInfo>();

        if (info != null)
        {
            info.Init(patient);
        }
    }
}
