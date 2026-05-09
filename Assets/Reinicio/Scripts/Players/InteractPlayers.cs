using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPlayers : MonoBehaviour
{
    [SerializeField] private int playerId = 1;

    [Header("Camilla")]
    public bool Isback = false;
    public MoveObject chargeStrecher;
    public Transform spawnStrecher;
    public Transform backDoor;

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

        if (chargeStrecher != null)
            chargeStrecher.IsInside = true;
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
        if (isInsideVehicle)
        {
            ExitVehicle();
            return;
        }

        if (Isback)
        {
            Strecher();
            return;
        }

        TryEnterVehicle();
    }

    void Strecher()
    {
        if (!Isback) return;
        if (chargeStrecher == null) return;
        if (strecher == null) return;
        if (spawnStrecher == null) return;
        if (backDoor == null) return;

        if (chargeStrecher.IsInside)
        {
            strecher.transform.SetPositionAndRotation(
                backDoor.position,
                backDoor.rotation
            );

            chargeStrecher.IsInside = false;
        }
        else
        {
            strecher.transform.SetPositionAndRotation(
                spawnStrecher.position,
                spawnStrecher.rotation
            );

            chargeStrecher.IsInside = true;

            if (chargeStrecher.IsInside &&
                chargeStrecher.hasPatient &&
                !chargeStrecher.alreadyScored)
            {
                ScoreManager.Instance.AddPoints(100);
                chargeStrecher.alreadyScored = true;
            }
        }

        chargeStrecher.body.linearVelocity = Vector3.zero;
        chargeStrecher.body.angularVelocity = Vector3.zero;
    }

    void TryEnterVehicle()
    {
        if (currentEntry == null) return;

        currentAmbulance = currentEntry.ambulance;
        currentEntry.Available = false;

        if (currentEntry.visuals != null)
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