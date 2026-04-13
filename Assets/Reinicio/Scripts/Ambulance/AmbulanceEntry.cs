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

    private int playerInside = 0; //  CAMBIO CLAVE

    private void Start()
    {
        if (player1UI != null)
            player1UI.SetActive(false);

        if (player2UI != null)
            player2UI.SetActive(false);
    }

    private void Update()
    {
        // PLAYER 1
        if (player1InRange && playerInside == 0 && Input.GetKeyDown(KeyCode.E))
        {
            if (player1UI != null)
                player1UI.SetActive(false);

            player1InRange = false;
            playerInside = 1; //  ahora sabemos quién ha entrado
        }

        // PLAYER 2
        if (player2InRange && playerInside == 0 && (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)))
        {
            if (player2UI != null)
                player2UI.SetActive(false);

            player2InRange = false;
            playerInside = 2; //  ahora sabemos quién ha entrado
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // PLAYER 1
        if (other.gameObject == player1)
        {
            if (playerInside == 0) //  solo si nadie ha entrado
            {
                player1InRange = true;

                if (player1UI != null)
                    player1UI.SetActive(true);
            }
        }

        // PLAYER 2
        if (other.gameObject == player2)
        {
            if (playerInside == 0) //  solo si nadie ha entrado
            {
                player2InRange = true;

                if (player2UI != null)
                    player2UI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // PLAYER 1
        if (other.gameObject == player1)
        {
            player1InRange = false;

            if (player1UI != null)
                player1UI.SetActive(false);

            // solo reset si ese jugador era el que estaba dentro
            if (playerInside == 1)
                playerInside = 0;
        }

        // PLAYER 2
        if (other.gameObject == player2)
        {
            player2InRange = false;

            if (player2UI != null)
                player2UI.SetActive(false);

            if (playerInside == 2)
                playerInside = 0;
        }
    }



}

