using UnityEngine;

public class Handles : MonoBehaviour
{
    public Transform snapPoint;

    private PlayerActions players;

    private Transform player1Transform;
    private Transform player2Transform;

    private bool player1InZone;
    private bool player2InZone;

    private int holder = 0;

    private void Awake()
    {
        players = new PlayerActions();
        players.Enable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InZone = true;
            player1Transform = other.transform;
        }
        if (other.CompareTag("Player2"))
        {
            player2InZone = true;
            player2Transform = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InZone = false;
            if (holder == 1)
            {
                Release();
            }
        }

        if (other.CompareTag("Player2"))
        {
            player2InZone = false;
            if (holder == 2)
            {
                Release();
            }
        }
    }
    private void Update()
    {
        float grab1 = players.Player1.Grab.ReadValue<float>();
        float grab2 = players.Player2.Grab.ReadValue<float>();


        if (holder == 1)
        {
            player1Transform.position = snapPoint.position;
            player1Transform.rotation = snapPoint.rotation;
        }
        else if (holder == 2)
        {
            player2Transform.position = snapPoint.position;
            player2Transform.rotation = snapPoint.rotation;
        }

        if (holder == 0)
        {
            if (player1InZone && grab1 > 0.5f)
            {
                Grab(1);
            }
            else if (player2InZone && grab2 > 0.5f)
            {
                Grab(2);
            }
        }
        else
        {
            if (holder == 1 && grab1 < 0.5f)
            {
                Release();
            }
            if (holder == 2 && grab2 < 0.5f)
            {
                Release();
            }
        }
    }
    private void Grab(int player)
    {
        holder = player;

        Transform snap = (player == 1) ? player1Transform : player2Transform;

        snap.position = snapPoint.position;
        snap.rotation = snapPoint.rotation;
    }

    private void Release()
    {
        holder = 0;
    }
}
