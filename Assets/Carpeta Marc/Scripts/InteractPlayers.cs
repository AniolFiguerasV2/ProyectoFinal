using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPlayers : MonoBehaviour
{
    private bool isInsideVehicle = false;

    private AmbulanceEntry currentEntry;
    private AmbulanceEntry entryUsed;
    private AmbulanceController currentAmbulance;

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

        entryUsed = currentEntry;
        currentAmbulance = currentEntry.ambulance;

        currentAmbulance.EnterVehicle(this, currentEntry.seat);

        isInsideVehicle = true;
        gameObject.SetActive(false);
    }

    void ExitVehicle()
    {
        currentAmbulance.ExitVehicle(this, entryUsed.exitPoint);

        isInsideVehicle = false;
        currentAmbulance = null;
        entryUsed = null;
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
}
