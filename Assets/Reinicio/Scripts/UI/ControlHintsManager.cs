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
        ShowHints(
            "Move:                    <sprite name=\"Botons_5\">\n" +
            "Interact:                 <sprite name=\"Botons_4\">\n" +
            "Hold:                    <sprite name=\"Botons_0\">");
    }

    public void ShowAmbulanceEnterHints()
    {
        ShowHints("Get into the ambulance: Y");
    }

    public void ShowDrivingHints()
    {
        ShowHints(
            "Pilot: Forward / Back           <sprite name=\"Botons_5\">\n" +
            "Copilot: Left / Right           <sprite name=\"Botons_5\">\n" +
            "Pausa:           <sprite name=\"Botons_2\">");
    }

    public void ShowStretcherSpawnHints()
    {
        ShowHints("Take out the stretcher: Y");
    }

    public void ShowStretcherCarryHints()
    {
        ShowHints("Move the stretcher:\nHold               <sprite name=\"Botons_0\">  +                <sprite name=\"Botons_5\">");
    }
}

