using UnityEngine;

public class ShowPlayerInteractionUI : MonoBehaviour
{
    [Header("Jugadores")]
    public GameObject player1;
    public GameObject player2;

    [Header("UI de interacción")]
    public GameObject player1UI;
    public GameObject player2UI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1)
        {
            if (player1UI != null)
                player1UI.SetActive(true);
        }

        if (other.gameObject == player2)
        {
            if (player2UI != null)
                player2UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player1)
        {
            if (player1UI != null)
                player1UI.SetActive(false);
        }

        if (other.gameObject == player2)
        {
            if (player2UI != null)
                player2UI.SetActive(false);
        }
    }
}
