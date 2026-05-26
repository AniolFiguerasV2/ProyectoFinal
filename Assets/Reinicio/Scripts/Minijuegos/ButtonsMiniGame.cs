using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonsMiniGame", menuName = "MiniGames/ButtonsMiniGame")]
public class ButtonsMiniGame : MiniGameBase
{
    [Header("ButtonsMiniGame")]
    public MiniGameUIButtons uiprefab;
    private MiniGameUIButtons uiInstance;

    [Header("InputManager")]
    [NonSerialized] public int player1Id = 1;
    [NonSerialized] public int player2Id = 2;

    [NonSerialized] public bool inGreen = false;
    [NonSerialized] public bool roundResolved = false;

    [NonSerialized] public int successCounter = 0;
    [NonSerialized] public int targetSuccess = 5;

    [NonSerialized] public int failCounter = 0;
    [NonSerialized] public int maxFails = 10;

    [NonSerialized] public bool p1PressedInGreen = false;
    [NonSerialized] public bool p2PressedInGreen = false;

    protected override void PrepareUIMinigame()
    {
        Vector3 position = Vector3.zero;
        uiInstance = Instantiate(uiprefab, MainUI.instance.baseCanvas.transform);
        uiInstance.Initialize(this);
    }

    public override void StartMinigame(PatientDeathTime patientDeathTime)
    {
        finished = false;

        successCounter = 0;
        failCounter = 0;

        roundResolved = false;
        inGreen = false;

        p1PressedInGreen = false;
        p2PressedInGreen = false;

        ShowFailsUI(failCounter, maxFails);

        base.StartMinigame(patientDeathTime);
    }

    public override void Fail()
    {
        HideFailsUI();
        base.Fail();
        Destroy(uiInstance.gameObject);
    }

    public override void Succes()
    {
        HideFailsUI();
        base.Succes();
        Destroy(uiInstance.gameObject);
    }

    public void HandleInputs()
    {
        if(roundResolved) return;

        bool p1Pressed = InputManager.Instance.GetInteractDown(player1Id);
        bool p2Pressed = InputManager.Instance.GetInteractDown(player2Id);

        if(inGreen)
        {
            if(p1Pressed) p1PressedInGreen = true;
            if(p2Pressed) p2PressedInGreen = true;

            if(p1PressedInGreen && p2PressedInGreen)
            {
                successCounter++;
                Debug.Log("LLevas " + successCounter + " Aciertos");
                roundResolved = true;

                if(successCounter >= targetSuccess)
                {
                    FinishGame(true);
                }
            }
        }
        else
        {
            if ((p1Pressed || p2Pressed))
            {
                RegisterFail();
                roundResolved = true;
            }
        }
    }

    public void StartGreenPhase()
    {
        inGreen = true;

        roundResolved = false;

        p1PressedInGreen = false;
        p2PressedInGreen = false;
    }

    public void EndGreenPhase()
    {
        inGreen = false;

        if (!roundResolved)
        {
            RegisterFail();
        }
    }

    private void RegisterFail()
    {
        successCounter = 0;
        failCounter++;

        UpdateFailsUI(failCounter, maxFails);

        Debug.Log("Llevas " + failCounter+ " Fallos");

        if(failCounter >= maxFails)
        {
            FinishGame(false);
        }
    }


    private void FinishGame(bool success)
    {
        if(finished) return;

        finished = true;

        if (success)
        {
            Succes();
        }
        else
        {
            Fail();
        }
        
        OnMinigameFinished?.Invoke(this);
    }
}
