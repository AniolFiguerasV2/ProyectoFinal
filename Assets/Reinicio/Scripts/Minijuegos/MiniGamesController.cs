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

    [Header("Private Var")]
    private MiniGameBase currentMiniGame;

    
    public void StartMinigame(PatientDeathTime patient)
    {
        if (patient == null)
            return;
        if (currentMiniGame != null)
            return;

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