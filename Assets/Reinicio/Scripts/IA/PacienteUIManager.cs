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

        if (spawner.patients.Count != lastPatientCount)
        {
            RebuildWidgets();
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

    void RebuildWidgets()
    {
        if (selectionFrame != null && widgetParent != null)
        {
            selectionFrame.SetParent(widgetParent, false);
            selectionFrame.gameObject.SetActive(false);
        }

        foreach (PacienteInfo widget in widgets)
        {
            if (widget != null)
                Destroy(widget.gameObject);
        }

        widgets.Clear();

        for (int i = 0; i < spawner.patients.Count; i++)
        {
            CreateWidget(spawner.patients[i]);
        }

        lastPatientCount = spawner.patients.Count;
        selectedIndex = 0;

        if (widgets.Count > 0)
        {
            MoveSelection();
            PointArrowToSelectedPatient();
        }
    }

    void MoveSelection()
    {
        if (widgets.Count == 0) return;

        if (selectionFrame == null)
        {
            Debug.LogWarning("SelectionFrame no está asignado en PacienteUIManager");
            return;
        }

        if (selectedIndex < 0)
            selectedIndex = 0;

        if (selectedIndex >= widgets.Count)
            selectedIndex = widgets.Count - 1;

        RectTransform widgetRect = widgets[selectedIndex].GetComponent<RectTransform>();

        selectionFrame.gameObject.SetActive(true);

        selectionFrame.SetParent(widgetRect, false);
        selectionFrame.SetAsLastSibling();

        selectionFrame.anchorMin = Vector2.zero;
        selectionFrame.anchorMax = Vector2.one;
        selectionFrame.offsetMin = Vector2.zero;
        selectionFrame.offsetMax = Vector2.zero;

        selectionFrame.localScale = Vector3.one;
        selectionFrame.localRotation = Quaternion.identity;

        Debug.Log("SelectionFrame movido a: " + selectionFrame.parent.name);
    }

    public void DisablePacienteUI()
    {
        uiEnabled = false;

        if (selectionFrame != null)
            selectionFrame.gameObject.SetActive(false);

        if (widgetParent != null)
            widgetParent.gameObject.SetActive(false);
    }

    public void EnablePacienteUI()
    {
        uiEnabled = true;

        if (widgetParent != null)
            widgetParent.gameObject.SetActive(true);

        if (selectionFrame != null)
            selectionFrame.gameObject.SetActive(true);

        if (widgets.Count > 0)
        {
            selectedIndex = 0;
            MoveSelection();
            PointArrowToSelectedPatient();
        }
        else
        {
            Debug.LogWarning("No hay widgets todavía para colocar el SelectionFrame");
        }
    }
}