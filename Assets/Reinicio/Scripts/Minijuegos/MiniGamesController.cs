using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGamesController : MonoBehaviour
{
    [Header("References")]
    public MoveObject moveObject;
    public AmbulanceController ambulanceController;

    [Header("MiniGames")]
    public List<MiniGameBase> miniGamesList;

    [Header("Countdown")]
    [SerializeField] private GameObject countdownCanvas;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private int countdownSeconds = 5;

    [Header("Private Var")]
    private MiniGameBase currentMiniGame;

    public void StartMinigame(PatientDeathTime patient)
    {
        if (patient == null)
            return;

        if (currentMiniGame != null)
            return;

        StartCoroutine(StartMinigameRoutine(patient));
    }

    private IEnumerator StartMinigameRoutine(PatientDeathTime patient)
    {
        countdownCanvas.SetActive(true);

        int timer = countdownSeconds;

        while (timer > 0)
        {
            countdownText.text = timer.ToString();

            yield return new WaitForSeconds(1f);

            timer--;
        }

        countdownText.text = "0";

        yield return new WaitForSeconds(0.5f);

        countdownCanvas.SetActive(false);

        int index = Random.Range(0, miniGamesList.Count);

        currentMiniGame = Instantiate(miniGamesList[index]);

        currentMiniGame.OnMinigameFinished += HandleMiniGameFinished;

        currentMiniGame.StartMinigame(patient);
    }

    private void HandleMiniGameFinished(MiniGameBase game)
    {
        if (currentMiniGame == game)
        {
            currentMiniGame.OnMinigameFinished -= HandleMiniGameFinished;
            currentMiniGame = null;
        }
    }
}