using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPlayers : MonoBehaviour
{
    private bool isInsideVehicle = false;
    private bool inStretcherRange = false;
    public bool Isback = false;
    public bool IsOutC = false;

    public AmbulanceEntry currentEntry;
    private AmbulanceController currentAmbulance;

    public MoveObject chargeStrecher;

    public Transform spawnStrecher;

    //Objeto donde se spawneara la camilla
    public Transform backDoor;

    public GameObject playervisual;
    //Objeto de camilla 
    public GameObject strecher;
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
            if (Isback)
            {
                Strecher();
            }
    }

    void Strecher()
    {
        if (!IsOutC)
        {
            strecher.transform.position = backDoor.transform.position;
            IsOutC = true;
        }
        else
        {
            if (chargeStrecher.IsInside)
            {
                Debug.Log(strecher.transform.position);
                strecher.transform.position = spawnStrecher.transform.position;
                IsOutC = false;
            }
        }
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
        if (other.gameObject.layer == 10)
        {
            Isback = true;
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
        if (other.gameObject.layer == 10)
        {
            Isback = false;
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