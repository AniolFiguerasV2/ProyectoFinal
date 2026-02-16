using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Handles : MonoBehaviour
{
    public Transform snapPoint;
    private static PlayerActions players; // Static para evitar conflictos entre objetos

    private Transform player1Transform;
    private Transform player2Transform;

    // Referencias a sus controladores
    private CharacterController cc1;
    private CharacterController cc2;

    public bool player1InZone;
    public bool player2InZone;
    public int holder = 0;

    private void Awake()
    {
        if (players == null)
        {
            players = new PlayerActions();
            players.Enable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InZone = true;
            player1Transform = other.transform;
            cc1 = other.GetComponent<CharacterController>();
        }
        if (other.CompareTag("Player2"))
        {
            player2InZone = true;
            player2Transform = other.transform;
            cc2 = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InZone = false;
            if (holder == 1) Release();
        }
        if (other.CompareTag("Player2"))
        {
            player2InZone = false;
            if (holder == 2) Release();
        }
    }

    private void Update()
    {
        float grab1 = players.Player1.Grab.ReadValue<float>();
        float grab2 = players.Player2.Grab.ReadValue<float>();

        // Lógica de AGARRE
        if (holder == 0)
        {
            if (player1InZone && grab1 > 0.5f) Grab(1);
            else if (player2InZone && grab2 > 0.5f) Grab(2);
        }
        else
        {
            // Lógica de SNAP FORZADO
            ApplySnap();

            // Lógica de SOLTAR
            if (holder == 1 && grab1 < 0.5f) Release();
            else if (holder == 2 && grab2 < 0.5f) Release();
        }
    }

    private void Grab(int playerNum)
    {
        holder = playerNum;
        // DESACTIVAMOS el CharacterController para que no bloquee el movimiento
        if (holder == 1 && cc1 != null) cc1.enabled = false;
        if (holder == 2 && cc2 != null) cc2.enabled = false;
    }

    private void Release()
    {
        // REACTIVAMOS el CharacterController para que vuelva a caminar
        if (holder == 1 && cc1 != null) cc1.enabled = true;
        if (holder == 2 && cc2 != null) cc2.enabled = true;

        holder = 0;
    }

    private void ApplySnap()
    {
        Transform target = (holder == 1) ? player1Transform : player2Transform;
        if (target != null)
        {
            target.position = snapPoint.position;
            target.rotation = snapPoint.rotation;
        }
    }
}
