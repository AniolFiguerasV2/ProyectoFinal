using UnityEngine;

public class AmbulanceManager : MonoBehaviour
{
    private bool isPlayersinside = false;
    private int RequiredPlayers = 2;
    private int CurrentplayersIn = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerIn(string tag)
    {
        CurrentplayersIn++;
        if (CurrentplayersIn >= RequiredPlayers)
        {
            Debug.Log("All players in");
            isPlayersinside = true;
        }
    }
    public void PlayerOut(string tag)
    {
        CurrentplayersIn--;
        if(CurrentplayersIn < RequiredPlayers)
        {
            isPlayersinside = false;
        }
    }
}
