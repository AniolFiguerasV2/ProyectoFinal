using UnityEngine;
using System.Collections.Generic;

public class PacienteUIManager : MonoBehaviour
{
    [SerializeField] private PatientSpawner spawner;
    [SerializeField] private GameObject pacienteWidgerPrefab;
    [SerializeField] private Transform widgetParent;

    [SerializeField] private RectTransform selectionFrame;

    [SerializeField] private ArrowController arrowController;

    private int lastPatientCount = 0;

    private List<PacienteInfo> widgets = new List<PacienteInfo>();
    private int selectedIndex = 0;

    private bool uiEnabled = false;

    void Update()
    {
        if (!uiEnabled) return;
        if (spawner == null) return;

        if (spawner.patients.Count > lastPatientCount)
        {
            for (int i = lastPatientCount; i < spawner.patients.Count; i++)
            {
                CreateWidget(spawner.patients[i]);
            }

            lastPatientCount = spawner.patients.Count;
            MoveSelection();
            PointArrowToSelectedPatient();
        }

        bool selectPatientInput =
    Input.GetKeyDown(KeyCode.Tab) ||
    (InputManager.Instance != null && InputManager.Instance.GetSelectPatientDown(1)); 

        if (selectPatientInput)
        {
            if (widgets.Count == 0) return;

            selectedIndex++;

            if (selectedIndex >= widgets.Count)
                selectedIndex = 0;

            MoveSelection();
            PointArrowToSelectedPatient();
        }
    }

    void PointArrowToSelectedPatient()
    {
        if (widgets.Count == 0) return;
        if (arrowController == null) return;

        PatientDeathTime selectedPatient = widgets[selectedIndex].GetPatient();

        if (selectedPatient != null)
        {
            arrowController.SetTarget(selectedPatient.transform);
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
            widgets.Add(info);
        }
    }

    void MoveSelection()
    {
        if (widgets.Count == 0) return;

        RectTransform widgetRect = widgets[selectedIndex].GetComponent<RectTransform>();

        selectionFrame.SetParent(widgetRect);
        selectionFrame.SetAsLastSibling();

        selectionFrame.anchorMin = Vector2.zero;
        selectionFrame.anchorMax = Vector2.one;

        selectionFrame.offsetMin = Vector2.zero;
        selectionFrame.offsetMax = Vector2.zero;
    }

    public void EnablePacienteUI()
    {
        uiEnabled = true;
        gameObject.SetActive(true);

        if (widgets.Count > 0)
        {
            selectedIndex = 0;
            MoveSelection();
        }
    }

    public void DisablePacienteUI()
    {
        uiEnabled = false;
        gameObject.SetActive(false);
    }
}