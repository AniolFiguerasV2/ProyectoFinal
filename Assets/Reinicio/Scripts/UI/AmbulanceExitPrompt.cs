using UnityEngine;

public class AmbulanceExitPrompt : MonoBehaviour
{
    [Header("Referencias")]
    public AmbulanceController ambulanceController;
    public MoveObject stretcher;
    public GameObject exitPromptUI;

    [Header("Detección")]
    public string patientTag = "Patient";

    private int patientsInRange = 0;

    public GuidedTutorialManager guidedTutorialManager;
    private bool stretcherObjectiveShown = false;

    private void Start()
    {
        if (exitPromptUI != null)
            exitPromptUI.SetActive(false);
    }

    private void Update()
    {
        UpdatePrompt();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(patientTag))
        {
            patientsInRange++;
            UpdatePrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(patientTag))
        {
            patientsInRange--;

            if (patientsInRange < 0)
                patientsInRange = 0;

            UpdatePrompt();
        }
    }

    private void UpdatePrompt()
    {
        if (exitPromptUI == null || ambulanceController == null)
            return;

        bool playersInside = ambulanceController.Allplayersin;
        bool patientNearby = patientsInRange > 0;
        bool patientAlreadyLoaded = stretcher != null && stretcher.hasPatient;

        bool shouldShowExitPrompt = playersInside && patientNearby && !patientAlreadyLoaded;

        exitPromptUI.SetActive(shouldShowExitPrompt);

        // Cuando los jugadores ya están fuera y hay paciente cerca
        if (!playersInside && patientNearby && !patientAlreadyLoaded && !stretcherObjectiveShown)
        {
            stretcherObjectiveShown = true;

            if (guidedTutorialManager != null)
            {
                guidedTutorialManager.SetTemporaryObjective("Take the stretcher from the back of the ambulance",5f);
            }
        }

        // Resetear si se alejan del paciente
        if (!patientNearby)
        {
            stretcherObjectiveShown = false;
        }
    }
}
