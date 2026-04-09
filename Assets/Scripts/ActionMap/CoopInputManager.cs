using UnityEngine;
using UnityEngine.InputSystem;

public class CoopInputManager : MonoBehaviour
{
    public static CoopInputManager Instance;

    public enum ControlMode
    {
        Keyboard,
        TwoGamepads
    }

    public ControlMode CurrentMode { get; private set; }

    private PlayerActions controls;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        controls = new PlayerActions();
        controls.Enable();

        RefreshControlMode();
    }

    private void Update()
    {
        RefreshControlMode();
    }

    private void RefreshControlMode()
    {
        if (Gamepad.all.Count >= 2)
            CurrentMode = ControlMode.TwoGamepads;
        else
            CurrentMode = ControlMode.Keyboard;
    }

    public Vector2 GetPlayer1Move()
    {
        if (CurrentMode == ControlMode.TwoGamepads)
            return Gamepad.all[0].leftStick.ReadValue();

        return controls.Player1.Move.ReadValue<Vector2>();
    }

    public Vector2 GetPlayer2Move()
    {
        if (CurrentMode == ControlMode.TwoGamepads)
            return Gamepad.all[1].leftStick.ReadValue();

        return controls.Player2.Move.ReadValue<Vector2>();
    }

    public bool Player1InteractPressed()
    {
        if (CurrentMode == ControlMode.TwoGamepads)
            return Gamepad.all[0].leftShoulder.wasPressedThisFrame;

        return controls.Player1.Interact.WasPressedThisFrame();
    }

    public bool Player2InteractPressed()
    {
        if (CurrentMode == ControlMode.TwoGamepads)
            return Gamepad.all[1].rightShoulder.wasPressedThisFrame;

        return controls.Player2.Interact.WasPressedThisFrame();
    }

    public bool Player1GrabPressed()
    {
        if (CurrentMode == ControlMode.TwoGamepads)
            return Gamepad.all[0].leftTrigger.wasPressedThisFrame;

        return controls.Player1.Grab.WasPressedThisFrame();
    }

    public bool Player2GrabPressed()
    {
        if (CurrentMode == ControlMode.TwoGamepads)
            return Gamepad.all[1].rightTrigger.wasPressedThisFrame;

        return controls.Player2.Grab.WasPressedThisFrame();
    }

    public bool PausePressed()
    {
        if (CurrentMode == ControlMode.TwoGamepads)
            return Gamepad.all[0].startButton.wasPressedThisFrame ||
                   Gamepad.all[1].startButton.wasPressedThisFrame;

        return controls.Player1.Pause.WasPressedThisFrame() ||
               controls.Player2.Pause.WasPressedThisFrame();
    }
}