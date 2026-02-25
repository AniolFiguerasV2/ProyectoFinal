using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPlayers : MonoBehaviour
{
    private bool isInsideVehicle = false;
    private bool inStretcherRange = false;

    public AmbulanceEntry currentEntry;
    private AmbulanceController currentAmbulance;

    public GameObject playervisual;
    public PlayerController movementscript;


    public void Start()
    {
        currentEntry = null;
    }
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
        currentEntry.Available = false;
        currentEntry.visuals.SetActive(false);
        currentAmbulance.EnterVehicle(this);

        isInsideVehicle = true;
    }

    void ExitVehicle()
    {
        currentAmbulance.ExitVehicle(this);
        currentEntry.Available = true;
        currentEntry.visuals.SetActive(true);
        isInsideVehicle = false;
        currentAmbulance = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AmbulanceEntry entry))
        {
            if (entry.Available)
            {
                currentEntry = entry;
            }
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AmbulanceEntry entry))
        {
            if (currentEntry == entry)
            {
                currentEntry = null;
            }
        }
    }

    public void DrivenMode()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }

    public void WalkMode()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void SetstretcherRange(bool newState)
    {
        inStretcherRange = newState;
    }

    public bool IsinStretcherRange()
    {
        return inStretcherRange;
    }
}
