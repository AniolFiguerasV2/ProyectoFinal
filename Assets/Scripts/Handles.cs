using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Handles : MonoBehaviour
{
    public Transform player;
    public Transform snapPoint;

    private int requirePlayers = 2;
    private int curerntPlayers = 0;

    private bool isPlayerGrabing = false;
    private PlayerActions playerActions;


    private void Awake()
    {
        playerActions = new PlayerActions();
    }
    private void Update()
    {
        playerActions.Player1.Grab.ReadValue<Button>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {

        }
    }
}
