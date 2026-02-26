using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPlayers : MonoBehaviour
{
    private bool isInsideVehicle = false;
    public bool Isleft = false;
    public bool Isright = false;
    public bool Isback = false;
    public AmbulanceEntry currentEntry;
    private AmbulanceController currentAmbulance;

    public Transform spawncamilla;

    bool IsOutC = false;
    public GameObject camilla;
    public Transform BackDoor;
    public MoveObject cargacamilla;

    public GameObject playervisual;
    public PlayerController movementscript;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (isInsideVehicle)
            ExitVehicle();
        else
        {
            if (Isleft || Isright)
            {
                TryEnterVehicle();
            }
            if (Isback)
            {
                Strecher();
            }
        }
            
    }

    void Strecher()
    {
        if (!IsOutC)
        {
            Debug.Log(BackDoor.transform.position);
            camilla.transform.position = BackDoor.transform.position;
            Debug.Log(camilla.transform.position);
            IsOutC = true;
        }
        else
        {
            if (cargacamilla.IsInside)
            {
                camilla.transform.position = spawncamilla.transform.position;
                IsOutC =false;
            }
        }
        //camilla.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        //camilla.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
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
        if(other.gameObject.layer == 9)
        {
            Debug.Log("Estoy en puerta");
            Isleft = true;
            if (other.TryGetComponent(out AmbulanceEntry entry))
            {
                if (entry.Available)
                {
                    currentEntry = entry;
                }
            }
        }
        if(other.gameObject.layer == 8)
        {
            Isright=true;
            if (other.TryGetComponent(out AmbulanceEntry entry))
            {
                if (entry.Available)
                {
                    currentEntry = entry;
                }
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

}
