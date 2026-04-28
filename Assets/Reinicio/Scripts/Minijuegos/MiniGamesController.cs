using TMPro;
using UnityEngine;

public class MiniGamesController : MonoBehaviour
{
    [Header("Referencias")]
    public MoveObject moveObject;

    [Header("MiniGamesReferences")]
    public MiniGame1 miniGame1;

    private PatientDeathTime patient;
    private PatientDeathTime cachedPatient;

    [Header("Canvases")]
    public GameObject normalCanvas;
    public GameObject patientAmbulanceCanvas;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    private bool activated = false;
    private bool lifetimeCapped = false;

    private bool miniGameActive = false;
    private bool miniGameFinished = false;

    void Update()
    {
        cachedPatient = moveObject.GetComponentInChildren<PatientDeathTime>();

        if (cachedPatient == null)
        {
            ResetToNormal();
            return;
        }

        patient = cachedPatient;

        bool inAmbulance = moveObject.IsInside && moveObject.hasPatient;

        UpdateTimerUI();

        if (!inAmbulance)
        {
            if (activated)
                ResetToNormal();

            return;
        }

        if (!activated)
        {
            ActivateAmbulanceCanvas();
            activated = true;

            ApplyLifetimeCap();
        }

        float remaining = patient.Lifetime - patient.Timer;

        if (remaining < 40f)
        {
            if (!miniGameActive && !miniGameFinished && miniGame1 != null)
            {
                StartMiniGame();
            }
        }
    }

    void StartMiniGame()
    {
        miniGameActive = true;

        miniGame1.gameObject.SetActive(true);

        miniGame1.OnMiniGameFinished += HandleMiniGameResult;
    }

    void HandleMiniGameResult(bool success)
    {
        miniGame1.OnMiniGameFinished -= HandleMiniGameResult;

        miniGameActive = false;
        miniGameFinished = true;

        if (patient == null) return;

        if (success)
        {
            patient.SetTimer(patient.Timer - 20f);
        }
        else
        {
            patient.SetTimer(patient.Timer + 10f);
        }
    }

    void ActivateAmbulanceCanvas()
    {
        normalCanvas.SetActive(false);
        patientAmbulanceCanvas.SetActive(true);
    }

    void ResetToNormal()
    {
        activated = false;
        lifetimeCapped = false;

        miniGameActive = false;
        miniGameFinished = false;

        patient = null;
        cachedPatient = null;

        patientAmbulanceCanvas.SetActive(false);
        normalCanvas.SetActive(true);
    }

    void UpdateTimerUI()
    {
        float remaining = patient.Lifetime - patient.Timer;

        int seconds = Mathf.CeilToInt(remaining);

        if (timerText != null)
        {
            timerText.text = $"{seconds:00}";
        }
    }

    void ApplyLifetimeCap()
    {
        if (patient == null || lifetimeCapped) return;

        if (patient.Lifetime > 100f)
        {
            float excess = patient.Lifetime - 100f;

            patient.SetLifetime(100f);
            patient.SetTimer(Mathf.Max(0f, patient.Timer - excess));
        }

        lifetimeCapped = true;
    }
}