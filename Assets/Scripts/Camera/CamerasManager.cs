using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    public GameObject ambulanceCamera;
    public GameObject playersCamera;
    public AmbulanceController ambulance;
    private bool changedState = false;
    void Update()
    {
        if(ambulance.Allplayersin != changedState)
        {
            ambulanceCamera.SetActive(ambulance.Allplayersin);
            playersCamera.SetActive(!ambulance.Allplayersin);
            changedState = ambulance.Allplayersin;
        }
    }
}
