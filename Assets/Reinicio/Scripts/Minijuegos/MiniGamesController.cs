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

    private void Start()
    {
        ambulanceController.OnAllPlayersInChange.AddListener(OnAllPlayersInChange);
    }

    private void OnAllPlayersInChange(bool allIn)
    {
        if(allIn)
        {
            bool inAmbulance = moveObject.IsInside && moveObject.hasPatient;
            bool playersInside = ambulanceController.Allplayersin;

            if (inAmbulance && playersInside && currentMiniGame == null)
            {
                SelectRandomMiniGame();
            }
        }
        else
        {
            currentMiniGame.Fail();
        }
    }

    private void SelectRandomMiniGame()
    {
        int index = Random.Range(0, miniGamesList.Count);

        currentMiniGame = miniGamesList[index];

        //currentMiniGame.StartMinigame(Patient.GetComponent<PatientDeathTime>());
        //falta singletone de patient
    }
}