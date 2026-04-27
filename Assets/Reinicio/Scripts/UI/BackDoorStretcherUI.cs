using UnityEngine;

public class BackDoorStretcherUI : MonoBehaviour
{
    [Header("Jugadores")]
    public GameObject player1;
    public GameObject player2;

    [Header("UI sacar camilla")]
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
