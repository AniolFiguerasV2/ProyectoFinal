using UnityEngine;

public class AmbulanceController : MonoBehaviour
{
    public void EnterVehicle(InteractPlayers player)
    {
        player.transform.SetParent(transform);
        //player.transform.position = seat.position;
        player.DrivenMode();
        //player.playervisual.SetActive(false);
        //player.movementscript.enabled = false;
    }

    public void ExitVehicle(InteractPlayers player)
    {
        player.transform.SetParent(null);
        //player.transform.position = exitPoint.position;
        player.WalkMode();
        //player.playervisual.SetActive(true);
        //player.movementscript.enabled = true;
    }
}
