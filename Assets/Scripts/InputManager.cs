using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event EventHandler OnCannonShot;

    private Controls controls;

    private void Awake()
    {
        Instance = this;

        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Tank.Shoot.performed += ShootCannon;

        controls.Tank.Enable();
    }

    private void OnDisable()
    {
        controls.Tank.Shoot.performed -= ShootCannon;

        controls.Tank.Disable();
    }

    private void ShootCannon(InputAction.CallbackContext obj)
    {
        OnCannonShot?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetTankMovementAmount()
    {
        Vector2 input = controls.Tank.Move.ReadValue<Vector2>();
        return new Vector3(input.x, 0, input.y);
    }
    public Vector3 GetTankRotationAmount()
    {
        Vector2 input = controls.Tank.Rotate.ReadValue<Vector2>();
        float rotationAmount = input.x - input.y;
        return new Vector3(0, rotationAmount, 0);
    }
}
