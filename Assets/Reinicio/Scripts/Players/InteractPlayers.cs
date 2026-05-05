using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPlayers : MonoBehaviour
{
    [SerializeField] private int playerId = 1;

    private bool isInsideVehicle = false;
    private bool inStretcherRange = false;

    public AmbulanceEntry currentEntry;
    private AmbulanceController currentAmbulance;

    public GameObject playervisual;
    //Objeto de camilla 
    public GameObject strecher;
    public PlayerController movementscript;
    public int PlayerId => playerId;


    public void Start()
    {
        currentEntry = null;
    }

    private void Update()
    {
        if (InputManager.Instance.GetInteractDown(playerId))
        {
            HandleInteract();
        }
    }
    
    void HandleInteract()
    {
        if(isInsideVehicle)
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
        currentAmbulance.EnterVehicle(this, currentEntry.ControlsStearing);

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
        GetComponent<PlayerController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }

    public void WalkMode()
    {
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