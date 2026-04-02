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

    private void Start()
    {
        Time.timeScale = 0f;

        tutorialPanel1.SetActive(true);
        tutorialPanel2.SetActive(false);

        drivingTutorialPanel1.SetActive(false);

        stretcherTutorialPanel1.SetActive(false);
        stretcherTutorialPanel2.SetActive(false);
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
}
