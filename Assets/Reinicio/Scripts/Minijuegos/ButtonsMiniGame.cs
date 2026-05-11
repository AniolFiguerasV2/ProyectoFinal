using UnityEngine;

[CreateAssetMenu(fileName = "ButtonsMiniGame", menuName = "MiniGames/ButtonsMiniGame")]
public class ButtonsMiniGame : MiniGameBase
{
    [Header("ButtonsMiniGame")]
    public MiniGameUIBase uiprefab;
    private MiniGameUIBase uiInstance;


    protected override void PrepareUIMinigame()
    {
        uiInstance = Instantiate(uiprefab);
    }

    public override void StartMinigame(PatientDeathTime patientDeathTime)
    {
        base.StartMinigame(patientDeathTime);

    }

    public override void Fail()
    {
        base.Fail();
        //Destroy(uiInstance.gameObject);
    }
    public override void Succes()
    {
        base.Succes();
        //Destroy(uiInstance.gameObject);
    }
}
