using UnityEngine;

public class Handles : MonoBehaviour
{
    private Transform player1Transform;
    private Transform player2Transform;

    private bool player1InZone;
    private bool player2InZone;

    private int holder = 0;

    public bool IsBeingHeld => holder != 0;
    public AudioSource sonidoAgarrar;

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
        bool grab1 = InputManager.Instance.GetGrabDown(1);
        bool grab2 = InputManager.Instance.GetGrabDown(2);

        if (grab1)
        {
            if (holder == 0 && player1InZone)
            {
                Grab(1);
            }
            else if (holder == 1)
            {
                Release();
            }
        }
        if(grab2)
        {
            if (holder == 0 && player2InZone)
            {
                Grab(2);
            }
            else if (holder == 2)
            { 
                Release();
            }
        }
    }

    private void Grab(int player)
    {
        holder = player;
        if (sonidoAgarrar != null)
        {
            sonidoAgarrar.PlayOneShot(sonidoAgarrar.clip);
        }
    }

    private void Release()
    {
        holder = 0;
    }
}
