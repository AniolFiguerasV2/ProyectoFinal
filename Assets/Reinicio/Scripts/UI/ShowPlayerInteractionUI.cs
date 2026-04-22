using UnityEngine;

public class ShowAmbulanceDoorUI : MonoBehaviour
{
    [Header("Tipo de puerta")]
    public bool controlsSteering = false;

    [Header("Jugadores")]
    public GameObject player1;
    public GameObject player2;

    [Header("UI Player 1")]
    public GameObject player1DriveUI;
    public GameObject player1SteerUI;

    [Header("UI Player 2")]
    public GameObject player2DriveUI;
    public GameObject player2SteerUI;

    private void Start()
    {
        HideAllUI();
    }

    private void HideAllUI()
    {
        if (player1DriveUI != null) player1DriveUI.SetActive(false);
        if (player1SteerUI != null) player1SteerUI.SetActive(false);
        if (player2DriveUI != null) player2DriveUI.SetActive(false);
        if (player2SteerUI != null) player2SteerUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject == player1)
        {
            if (controlsSteering)
            {
                if (player1SteerUI != null)
                    player1SteerUI.SetActive(true);
            }
            else
            {
                if (player1DriveUI != null)
                    player1DriveUI.SetActive(true);
            }
        }

        if (other.transform.root.gameObject == player2)
        {
            if (controlsSteering)
            {
                if (player2SteerUI != null)
                    player2SteerUI.SetActive(true);
            }
            else
            {
                if (player2DriveUI != null)
                    player2DriveUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.gameObject == player1)
        {
            if (player1DriveUI != null)
                player1DriveUI.SetActive(false);

            if (player1SteerUI != null)
                player1SteerUI.SetActive(false);
        }

        if (other.transform.root.gameObject == player2)
        {
            if (player2DriveUI != null)
                player2DriveUI.SetActive(false);

            if (player2SteerUI != null)
                player2SteerUI.SetActive(false);
        }
    }
}
