using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorialManager : MonoBehaviour
{
    public GameObject[] panels;

    [Header("Menú de pausa")]
    public GameObject pausePanel;
    public GameObject optionsPanel;

    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.Instance != null && InputManager.Instance.GetPausePressed())
        {           
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
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
