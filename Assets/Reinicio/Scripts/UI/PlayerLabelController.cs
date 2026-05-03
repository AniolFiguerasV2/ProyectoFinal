using UnityEngine;

public class PlayerLabelController : MonoBehaviour
{
    public UIFollow labelJ1;
    public UIFollow labelJ2;

    public Transform player1;
    public Transform player2;

    public Transform driverPoint;
    public Transform copilotPoint;

    public Camera playersCamera;
    public Camera ambulanceCamera;

    private void Start()
    {
        SetOnFoot();
    }

    public void SetOnFoot()
    {
        labelJ1.target = player1;
        labelJ2.target = player2;

        labelJ1.cam = playersCamera;
        labelJ2.cam = playersCamera;
    }

    public void SetInVehicle(int steeringPlayerId)
    {
        labelJ1.cam = ambulanceCamera;
        labelJ2.cam = ambulanceCamera;

        if (steeringPlayerId == 1)
        {
            labelJ1.target = driverPoint;
            labelJ2.target = copilotPoint;
        }
        else
        {
            labelJ2.target = driverPoint;
            labelJ1.target = copilotPoint;
        }
    }
}