using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel1;
    public GameObject tutorialPanel2;

    public GameObject drivingTutorialPanel1;

    public GameObject stretcherTutorialPanel1;
    public GameObject stretcherTutorialPanel2;

    public GameObject[] panels;
    private bool drivingTutorialShown = false;
    private bool stretcherTutorialShown = false;

    [Header("Menú de pausa")]
    public GameObject pausePanel;
    public GameObject optionsPanel;

    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 0f;

        tutorialPanel1.SetActive(true);
        tutorialPanel2.SetActive(false);

        drivingTutorialPanel1.SetActive(false);

        stretcherTutorialPanel1.SetActive(false);
        stretcherTutorialPanel2.SetActive(false);

        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    private void Update()
    {
        if (CoopInputManager.Instance != null && CoopInputManager.Instance.PausePressed())
        {
            // evitar conflictos con tutoriales
            if (tutorialPanel1.activeSelf || tutorialPanel2.activeSelf ||
                drivingTutorialPanel1.activeSelf ||
                stretcherTutorialPanel1.activeSelf || stretcherTutorialPanel2.activeSelf)
            {
                return;
            }

            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ShowSecondTutorial()
    {
        tutorialPanel1.SetActive(false);
        tutorialPanel2.SetActive(true);
    }

    public void EndTutorial()
    {
        tutorialPanel1.SetActive(false);
        tutorialPanel2.SetActive(false);
        Time.timeScale = 1f;
    }


    public void ShowPanel(GameObject panelToShow)
    {
        // Desactivar todos
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // Activar el seleccionado
        panelToShow.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();

        // Para que funcione dentro del editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void StartGame()
    {
        Time.timeScale = 1f; // por si estaba pausado
        SceneManager.LoadScene("Escena Final"); 
    }

    public void ShowDrivingTutorial()
    {
        if (drivingTutorialShown) return;

        drivingTutorialShown = true;

        Time.timeScale = 0f;
        drivingTutorialPanel1.SetActive(true);
    }

    public void ShowSecondDrivingTutorial()
    {
        drivingTutorialPanel1.SetActive(false);
    }

    public void EndDrivingTutorial()
    {
        drivingTutorialPanel1.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowStretcherTutorial()
    {
        if (stretcherTutorialShown) return;

        stretcherTutorialShown = true;

        Time.timeScale = 0f;
        stretcherTutorialPanel1.SetActive(true);
        stretcherTutorialPanel2.SetActive(false);
    }

    public void ShowSecondStretcherTutorial()
    {
        stretcherTutorialPanel1.SetActive(false);
        stretcherTutorialPanel2.SetActive(true);
    }

    public void EndStretcherTutorial()
    {
        stretcherTutorialPanel1.SetActive(false);
        stretcherTutorialPanel2.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenOptions()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Principal"); 
    }
}
