using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel1;
    public GameObject tutorialPanel2;

    public GameObject[] panels;

    private void Start()
    {
        Time.timeScale = 0f;

        tutorialPanel1.SetActive(true);
        tutorialPanel2.SetActive(false);
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
        SceneManager.LoadScene("Escena Final1"); 
    }
}
