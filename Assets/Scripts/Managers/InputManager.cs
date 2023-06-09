using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private LayerMask groundLayerMask;

    private Controls controls;
    private const float MAX_MOUSE_DISTANCE = 1000f;

    public event EventHandler OnCannonShot;
    public event EventHandler OnPausePerformed;

    private void Awake()
    {
        Instance = this;

        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Tank.Shoot.performed += ShootCannon;
        controls.Pause.Pause.performed += PauseGame;
        EnableControls();
    }

    private void OnDisable()
    {
        controls.Tank.Shoot.performed -= ShootCannon;
        controls.Pause.Pause.performed -= PauseGame;
        DisableTankControls();
    }

    public void EnableControls()
    {
        controls.Tank.Enable();
        controls.Pause.Enable();
    }

    public void DisableTankControls()
    {
        controls.Tank.Disable();
    }

    private void ShootCannon(InputAction.CallbackContext obj)
    {
        OnCannonShot?.Invoke(this, EventArgs.Empty);
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        DisableTankControls();
        OnPausePerformed?.Invoke(this, EventArgs.Empty);
    }

    public float GetMovementDirection()
    {
        Vector2 input = controls.Tank.Move.ReadValue<Vector2>();
        return input.x + input.y;
    }

    public float GetTankRotationDirection()
    {
        Vector2 input = controls.Tank.Rotate.ReadValue<Vector2>();
        return input.x - input.y;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue()));
        if (Physics.Raycast(ray, out RaycastHit raycastHit, MAX_MOUSE_DISTANCE, groundLayerMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
