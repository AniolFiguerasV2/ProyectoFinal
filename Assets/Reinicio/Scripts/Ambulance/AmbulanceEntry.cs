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

    private void Start()
    {
        if (player1UI != null)
            player1UI.SetActive(false);

        if (player2UI != null)
            player2UI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Available) return;

        if (other.gameObject == player1 || other.transform.IsChildOf(player1.transform))
        {
            if (player1UI != null)
                player1UI.SetActive(true);
        }

        if (other.gameObject == player2 || other.transform.IsChildOf(player2.transform))
        {
            if (player2UI != null)
                player2UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player1 || other.transform.IsChildOf(player1.transform))
        {
            if (player1UI != null)
                player1UI.SetActive(false);
        }

        if (other.gameObject == player2 || other.transform.IsChildOf(player2.transform))
        {
            if (player2UI != null)
                player2UI.SetActive(false);
        }
    }
}