using UnityEngine;

public class AmbulanceEntry : MonoBehaviour
{
    public AmbulanceController ambulance;
    public bool Available = true;
    public bool ControlsStearing = false;
    public GameObject visuals;

    [Header("Jugadores")]
    public GameObject player1;
    public GameObject player2;

    [Header("UI de interacción")]
    public GameObject player1UI;
    public GameObject player2UI;

    private bool player1InRange = false;
    private bool player2InRange = false;

    private void Start()
    {
        if (player1UI != null)
            player1UI.SetActive(false);

        if (player2UI != null)
            player2UI.SetActive(false);
    }

    private void Update()
    {
        if (player1InRange && InputManager.Instance != null && InputManager.Instance.GetInteractDown(1))
        {
            InteractPlayers interactPlayer = player1.GetComponent<InteractPlayers>();
            if (interactPlayer != null)
            {
                interactPlayer.currentEntry = this;
                ambulance.EnterVehicle(interactPlayer, ControlsStearing);
            }
        }

        if (player2InRange && InputManager.Instance != null && InputManager.Instance.GetInteractDown(2))
        {
            InteractPlayers interactPlayer = player2.GetComponent<InteractPlayers>();
            if (interactPlayer != null)
            {
                interactPlayer.currentEntry = this;
                ambulance.EnterVehicle(interactPlayer, ControlsStearing);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ha entrado en trigger: " + other.name);

        if (!Available) return;

        if (other.gameObject == player1 || other.transform.IsChildOf(player1.transform))
        {
            Debug.Log("Player 1 en rango");
            player1InRange = true;

            if (player1UI != null)
            {
                player1UI.SetActive(true);
                Debug.Log("Activando UI Player 1");
            }
        }

        if (other.gameObject == player2 || other.transform.IsChildOf(player2.transform))
        {
            Debug.Log("Player 2 en rango");
            player2InRange = true;

            if (player2UI != null)
            {
                player2UI.SetActive(true);
                Debug.Log("Activando UI Player 2");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player1 || other.transform.IsChildOf(player1.transform))
        {
            player1InRange = false;

            if (player1UI != null)
                player1UI.SetActive(false);
        }

        if (other.gameObject == player2 || other.transform.IsChildOf(player2.transform))
        {
            player2InRange = false;

            if (player2UI != null)
                player2UI.SetActive(false);
        }
    }
}