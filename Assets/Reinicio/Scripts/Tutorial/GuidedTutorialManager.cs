using TMPro;
using UnityEngine;

public class GuidedTutorialManager : MonoBehaviour
{
    [Header("UI Objetivo")]
    public GameObject objectivePanel;
    public TextMeshProUGUI objectiveText;

    [Header("UI Controles")]
    public ControlHintsManager controlHintsManager;

    [Header("UI generales")]
    public GameObject scoreUI;
    public GameObject timerUI;

    [Header("UI pacientes")]
    public PacienteUIManager pacienteUIManager;

    [Header("Objetos del mundo")]
    public GameObject ambulanceArrow;

    private Coroutine objectiveRoutine;

    private void Start()
    {
        StartFirstStep();
    }

    public void StartFirstStep()
    {
        if (scoreUI != null)
            scoreUI.SetActive(false);

        if (timerUI != null)
            timerUI.SetActive(false);

        if (pacienteUIManager != null)
            pacienteUIManager.DisablePacienteUI();

        if (ambulanceArrow != null)
            ambulanceArrow.SetActive(false);

        if (objectivePanel != null)
            objectivePanel.SetActive(true);

        if (objectiveText != null)
            objectiveText.text = "Go to the ambulance";

        if (controlHintsManager != null)
            controlHintsManager.ShowOnFootHints();
    }

    public void ShowDrivingStep()
    {
        if (ambulanceArrow != null)
            ambulanceArrow.SetActive(true);

        if (pacienteUIManager != null)
            pacienteUIManager.EnablePacienteUI();

        if (objectivePanel != null)
            objectivePanel.SetActive(true);

        if (objectiveRoutine != null)
            StopCoroutine(objectiveRoutine);

        objectiveRoutine = StartCoroutine(DrivingObjectiveSequence());
    }

    private System.Collections.IEnumerator DrivingObjectiveSequence()
    {
        SetObjective("Press X to select a patient and go pick them up");

        yield return new WaitForSeconds(5f);

        SetObjective("Pilot: move forward and back. Copilot: turn left and right.");

        yield return new WaitForSeconds(5f);

        HideObjective();
    }

    public void ShowGameplayUI()
    {
        if (scoreUI != null)
            scoreUI.SetActive(true);

        if (timerUI != null)
            timerUI.SetActive(true);
    }

    public void SetObjective(string newObjective)
    {
        if (objectivePanel != null)
            objectivePanel.SetActive(true);

        if (objectiveText != null)
            objectiveText.text = newObjective;
    }

    public void HideObjective()
    {
        if (objectivePanel != null)
            objectivePanel.SetActive(false);
    }
}