using UnityEditor.Localization.Plugins.XLIFF.Common;
using UnityEngine;

public class Handles : MonoBehaviour
{
    private Transform player1Transform;
    private Transform player2Transform;

    private bool player1InZone;
    private bool player2InZone;

    private int holder = 0;

    private Transform currentHolderTransform;

    public bool IsBeingHeld => holder != 0;
    public Transform HolderPlayerTransform => holder == 1 ? player1Transform : holder == 2 ? player2Transform : null;

    public float maxDistanceFromHandle = 1.5f;

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
        }

        if (other.CompareTag("Player2"))
        {
            player2InZone = false;
        }
    }

    private void Update()
    {
        HandlePlayerInput(1, player1InZone);
        HandlePlayerInput(2, player2InZone);

        LimitPlayerDistance();
    }

    private void Grab(int player)
    {
        holder = player;

        currentHolderTransform = (player == 1) ? player1Transform : player2Transform;

        if (sonidoAgarrar != null)
        {
            sonidoAgarrar.PlayOneShot(sonidoAgarrar.clip);
        }
    }

    private void HandlePlayerInput(int player, bool inZone)
    {
        bool grab = InputManager.Instance.GetGrabDown(player);

        if (!grab) return;

        if (holder == 0 && inZone)
        {
            Grab(player);
        }
        else if (holder == player)
        {
            Release();
        }
    }

    private void LimitPlayerDistance()
    {
        if (currentHolderTransform == null) return;

        Vector3 handlePosition = transform.position;

        Vector3 direction =
            currentHolderTransform.position - handlePosition;

        float distance = direction.magnitude;

        if (distance > maxDistanceFromHandle)
        {
            currentHolderTransform.position =
                handlePosition +
                direction.normalized * maxDistanceFromHandle;
        }
    }

    private void Release()
    {
        holder = 0;
        currentHolderTransform = null;
    }
}
