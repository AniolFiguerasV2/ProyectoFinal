using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPlayers : MonoBehaviour
{
    private bool isInsideVehicle = false;

    private AmbulanceEntry currentEntry;
    private AmbulanceController currentAmbulance;

    public GameObject playervisual;
    public PlayerController movementscript;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (isInsideVehicle)
            ExitVehicle();
        else
            TryEnterVehicle();
    }

    void TryEnterVehicle()
    {
        if (currentEntry == null) return;
        currentAmbulance = currentEntry.ambulance;

        currentAmbulance.EnterVehicle(this);

        isInsideVehicle = true;
    }

    void ExitVehicle()
    {
        currentAmbulance.ExitVehicle(this);

        isInsideVehicle = false;
        currentAmbulance = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AmbulanceEntry entry))
            currentEntry = entry;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AmbulanceEntry entry))
        {
            if (currentEntry == entry)
                currentEntry = null;
        }
    }

    public void DrivenMode()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public void WalkMode()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }

}
