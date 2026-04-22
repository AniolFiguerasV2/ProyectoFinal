using TMPro;
using UnityEngine;

public class GuidedTutorialManager : MonoBehaviour
{
    [Header("UI Objetivo")]
    public GameObject objectivePanel;
    public TextMeshProUGUI objectiveText;

    [Header("UI Controles")]
    public ControlHintsManager controlHintsManager;

    [Header("UI a ocultar al inicio")]
    public GameObject patientUI;
    public GameObject scoreUI;
    public GameObject timerUI;

    private void Start()
    {
        StartFirstStep();
    }

    public void StartFirstStep()
    {
        if (patientUI != null)
            patientUI.SetActive(false);

        if (scoreUI != null)
            scoreUI.SetActive(false);

        if (timerUI != null)
            timerUI.SetActive(false);

        if (objectivePanel != null)
            objectivePanel.SetActive(true);

        if (objectiveText != null)
            objectiveText.text = "Go to the ambulance";

        if (controlHintsManager != null)
            controlHintsManager.ShowOnFootHints(); 
    }

    public void ShowGameplayUI()
    {
        if (patientUI != null)
            patientUI.SetActive(true);

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
