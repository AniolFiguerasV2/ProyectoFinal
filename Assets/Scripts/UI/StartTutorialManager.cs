using UnityEngine;

public class StartTutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel1;
    public GameObject tutorialPanel2;

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
}
