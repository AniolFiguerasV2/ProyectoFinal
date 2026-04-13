using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject losePanel;
    public GameObject winPanel;

    private int patientsDelivered = 0;
    public int totalPatientsToWin = 3;

    private void Awake()
    {
        Instance = this;
    }
    public void PatientDelivered()
    {
        patientsDelivered++;

        if (patientsDelivered >= totalPatientsToWin)
        {
            WinGame();
        }
    }
    public void PatientDied()
    {
        LoseGame();
    }

    void WinGame()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    void LoseGame()
    {
        Time.timeScale = 0f;
        losePanel.SetActive(true);
    }
}
