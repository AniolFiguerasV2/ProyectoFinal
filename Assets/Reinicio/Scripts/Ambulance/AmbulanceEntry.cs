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

    private void OnTriggerEnter(Collider other)
    {
        // PLAYER 1
        if (other.gameObject == player1)
        {
            if (Available) //  solo si nadie ha entrado
            {
                player1InRange = true;

                if (player1UI != null)
                    player1UI.SetActive(true);
            }
        }
        // PLAYER 2
        if (other.gameObject == player2)
        {
            if (Available) //  solo si nadie ha entrado
            {
                player2InRange = true;

                if (player2UI != null)
                    player2UI.SetActive(true); 
            }
        }
        ControlHintsManager.Instance.ShowAmbulanceEnterHints();

    }
    private void OnTriggerExit(Collider other)
    {
        // PLAYER 1
        if (other.gameObject == player1)
        {
            player1InRange = false;

            if (player1UI != null)
                player1UI.SetActive(false);
        }

        // PLAYER 2
        if (other.gameObject == player2)
        {
            player2InRange = false;

            if (player2UI != null)
                player2UI.SetActive(false);

        }
        ControlHintsManager.Instance.ShowOnFootHints();
    }
}

