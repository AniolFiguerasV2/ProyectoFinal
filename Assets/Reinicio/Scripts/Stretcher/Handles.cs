using UnityEngine;

public class Handles : MonoBehaviour
{
    private Transform player1Transform;
    private Transform player2Transform;

    private bool player1InZone;
    private bool player2InZone;

    private int holder = 0;

    public bool IsBeingHeld => holder != 0;

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
        bool grab1 = InputManager.Instance.GetGrabHold(1);
        bool grab2 = InputManager.Instance.GetGrabHold(2);

        if (holder == 0)
        {
            if (player1InZone && grab1)
            {
                Grab(1);
            }
            else if (player2InZone && grab2)
            {
                Grab(2);
            }
        }
        else
        {
            if (holder == 1 && !grab1)
            {
                Release();
            }
            if (holder == 2 && !grab2)
            {
                Release();
            }
        }
    }

    private void Grab(int player)
    {
        holder = player;
    }

    private void Release()
    {
        holder = 0;
    }
}
