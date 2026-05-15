using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BarMinigame", menuName = "MiniGames/BarMinigame")]
public class BarMinigame : MiniGameBase
{
    [Header("UI")]
    public MiniGameUIBar uiprefab;
    private MiniGameUIBar uiInstance;

    [Header("InputManager")]
    [NonSerialized] public int player1Id = 1;
    [NonSerialized] public int player2Id = 2;

    [NonSerialized] public int failCounter = 0;
    [NonSerialized] public int maxFails = 3;

    [NonSerialized] public bool player1Success = false;
    [NonSerialized] public bool player2Success = false;

    [NonSerialized] public bool isActive = false;
    protected override void PrepareUIMinigame()
    {
        uiInstance = Instantiate(uiprefab, MainUI.instance.baseCanvas.transform);
        uiInstance.Initialize(this);
        uiInstance.StartLoop();
    }

    public override void StartMinigame(PatientDeathTime patientDeathTime)
    {
        finished = false;
        isActive = false;

        failCounter = 0;

        player1Success = false;
        player2Success = false;

        base.StartMinigame(patientDeathTime);

        isActive = true;
    }

    public void Tick()
    {
        if (finished || !isActive) return;

        HandleInputs();
    }

    public void HandleInputs()
    {
        bool p1Pressed = InputManager.Instance.GetGrabDown(player1Id);
        bool p2Pressed = InputManager.Instance.GetGrabDown(player2Id);

        if (!p1Pressed && !p2Pressed)
            return;

        bool success = uiInstance != null && uiInstance.IsInCenter();

        if (p1Pressed)
        {
            if (success) player1Success = true;
            else failCounter++;
        }

        if (p2Pressed)
        {
            if (success) player2Success = true;
            else failCounter++;
        }

        if (player1Success && player2Success)
        {
            FinishGame(true);
            return;
        }

        if (failCounter >= maxFails)
        {
            FinishGame(false);
            return;
        }

        uiInstance.ResetPositions();
    }

    private void FinishGame(bool success)
    {
        if (finished) return;
        
        finished = true;
        isActive = false;

        if (success) 
        {
            Succes();
        }
        else
        {
            Fail();
        }
        if(uiInstance != null)
        {
            uiInstance.StopLoop();
            GameObject.Destroy(uiInstance.gameObject);
            uiInstance = null;
        }

        OnMinigameFinished?.Invoke(this);

    }
}
