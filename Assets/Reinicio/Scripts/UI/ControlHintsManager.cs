using TMPro;
using UnityEngine;

public class ControlHintsManager : MonoBehaviour
{
    public static ControlHintsManager Instance;

    [Header("UI")]
    public GameObject hintsPanel;
    public TextMeshProUGUI hintsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void ShowHints(string message)
    {
        if (hintsPanel != null)
            hintsPanel.SetActive(true);

        if (hintsText != null)
            hintsText.text = message;
    }

    public void HideHints()
    {
        if (hintsPanel != null)
            hintsPanel.SetActive(false);
    }

    public void ShowOnFootHints()
    {
        ShowHints("Move: Left Stick\nInteract: Y\nHold: A");
    }

    public void ShowAmbulanceEnterHints()
    {
        ShowHints("Get into the ambulance: Y");
    }

    public void ShowDrivingHints()
    {
        ShowHints("Pilot: Stick (L) up / down\nCopilot: Stick (L) left / right\nPausa: Start");
    }

    public void ShowStretcherSpawnHints()
    {
        ShowHints("Take out the stretcher: Y");
    }

    public void ShowStretcherCarryHints()
    {
        ShowHints("Move the stretcher: hold A + move");
    }
}

