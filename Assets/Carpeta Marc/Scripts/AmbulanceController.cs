using UnityEngine;

public class AmbulanceController : MonoBehaviour
{
    public void EnterVehicle(InteractPlayers player, Transform seat)
    {
        player.transform.SetParent(transform);
        player.transform.position = seat.position;
    }

    public void ExitVehicle(InteractPlayers player, Transform exitPoint)
    {
        player.transform.SetParent(null);
        player.transform.position = exitPoint.position;
        player.gameObject.SetActive(true);
    }
}
