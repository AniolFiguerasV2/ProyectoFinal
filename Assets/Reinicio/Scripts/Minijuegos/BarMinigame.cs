using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BarMinigame", menuName = "MiniGames/BarMinigame")]
public class BarMinigame : MiniGameBase
{
    [Header("ButtonsMiniGame")]
    public MiniGameUIBase uiprefab;
    private MiniGameUIBase uiInstance;

    [Header("InputManager")]
    [NonSerialized] public int player1Id = 1;
    [NonSerialized] public int player2Id = 2;

    protected override void PrepareUIMinigame()
    {
        Vector3 position = Vector3.zero;
        uiInstance = Instantiate(uiprefab, MainUI.instance.baseCanvas.transform);
        //uiInstance.Initialize(this);
    }

    public override void StartMinigame(PatientDeathTime patientDeathTime)
    {
        finished = false;

        base.StartMinigame(patientDeathTime);
    }

    public override void Fail()
    {
        base.Fail();
        Destroy(uiInstance.gameObject);
    }

    public override void Succes()
    {
        base.Succes();
        Destroy(uiInstance.gameObject);
    }

    public void HandleInputs()
    {
        bool p1Pressed = InputManager.Instance.GetGrabHold(player1Id);
        bool p2Pressed = InputManager.Instance.GetGrabHold(player2Id);
    }
}
