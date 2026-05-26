using TMPro;
using UnityEngine;

public class MiniGamesFailUI : MonoBehaviour
{
    [SerializeField] private GameObject root;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI currentFailsText;

    public void Show()
    {
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    public void UpdateFails(int currentFails, int maxFails)
    {
        currentFailsText.text = "Llevas: " + currentFails + " de " + maxFails + " Fallos";
    }
}
