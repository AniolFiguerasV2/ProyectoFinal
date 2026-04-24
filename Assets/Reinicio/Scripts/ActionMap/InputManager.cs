using UnityEngine;
using UnityEngine.InputSystem; // IMPORTANTE
using UnityEngine.TextCore.LowLevel;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Input Actions")]
    public InputActionAsset actionsAsset;

    private InputActionMap player1Map;
    private InputActionMap player2Map;
    private InputActionMap globalMap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (actionsAsset == null)
        {
            Debug.LogError("[InputManager] Falta ActionsAsset.");
            return;
        }

        // 1) Localizamos los mapas
        player1Map = actionsAsset.FindActionMap("Player1", true);
        player2Map = actionsAsset.FindActionMap("Player2", true);
        globalMap = actionsAsset.FindActionMap("Global", true);

        // 2) Asignamos DISPOSITIVOS a cada mapa
        SetupDevicesForPlayers();

        // 3) Activamos mapas
        player1Map.Enable();
        player2Map.Enable();
        globalMap.Enable();
    }

    private void SetupDevicesForPlayers()
    {
        // Lista de mandos conectados en este momento
        var gamepads = Gamepad.all;

        // Referencia opcional al teclado (puede ser null en plataformas raras)
        Keyboard keyboard = Keyboard.current;

        // ---- Player1: teclado + Gamepad[0] (si existe) ----
        if (player1Map != null)
        {
            if (gamepads.Count > 0 && keyboard != null)
                player1Map.devices = new InputDevice[] { keyboard, gamepads[0] };
            else if (gamepads.Count > 0)
                player1Map.devices = new InputDevice[] { gamepads[0] };
            else if (keyboard != null)
                player1Map.devices = new InputDevice[] { keyboard };
            else
                player1Map.devices = null; // ningún dispositivo (raro)
        }

        // ---- Player2: teclado + Gamepad[1] (si existe) ----
        if (player2Map != null)
        {
            if (gamepads.Count > 1 && keyboard != null)
                player2Map.devices = new InputDevice[] { keyboard, gamepads[1] };
            else if (gamepads.Count > 1)
                player2Map.devices = new InputDevice[] { gamepads[1] };
            else if (keyboard != null)
                player2Map.devices = new InputDevice[] { keyboard };
            else
                player2Map.devices = null;
        }

        // ---- Global (pausa, etc.): que escuche TODO ----
        if (globalMap != null)
        {
            globalMap.devices = null;  // null = todos los dispositivos
        }

        Debug.Log($"[InputManager] Devices asignados: gamepads={gamepads.Count}");
    }

    private InputActionMap GetMap(int playerId)
    {
        return (playerId == 2) ? player2Map : player1Map;
    }

    private InputAction GetAction(int playerId, string actionName)
    {
        var map = GetMap(playerId);
        return map != null ? map.FindAction(actionName, false) : null;
    }

    // --------- Movimiento a pie ----------
    public Vector2 GetMoveAxis(int playerId)
    {
        var act = GetAction(playerId, "Move");
        return act != null ? act.ReadValue<Vector2>() : Vector2.zero;
    }

    // --------- Interacción / usar ----------
    public bool GetInteractDown(int playerId)
    {
        var act = GetAction(playerId, "Interact");
        return act != null && act.WasPressedThisFrame();
    }

    // --------- Agarrar camilla ----------
    public bool GetGrabHold(int playerId)
    {
        var act = GetAction(playerId, "GrabStretcher");
        if (act == null) return false;

        float v = 0f;
        try
        {
            v = act.ReadValue<float>();
        }
        catch
        {
            return act.IsPressed();
        }
        return v > 0.5f;
    }

    // --------- Conducir ambulancia ----------
    public float GetAmbulanceDriveAxis(int playerId)
    {
        var act = GetAction(playerId, "AmbulanceDrive");
        return act != null ? act.ReadValue<float>() : 0f;
    }

    public float GetAmbulanceSteerAxis(int playerId)
    {
        var act = GetAction(playerId, "AmbulanceSteer");
        return act != null ? act.ReadValue<float>() : 0f;
    }

    // --------- Pausa ----------
    public bool GetPausePressed()
    {
        if (globalMap == null) return false;

        var act = globalMap.FindAction("Pause", false);
        return act != null && act.WasPressedThisFrame();
    }

    public bool GetSelectPatientDown(int playerId)
    {
        var act = GetAction(playerId, "SelectPatient");
        return act != null && act.WasPressedThisFrame();
    }
}
