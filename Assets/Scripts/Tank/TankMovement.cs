using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Windows;

public class TankMovement : NetworkBehaviour
{
    [SerializeField] private float defaultMoveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform turret;

    private Tank tank;
    private InputManager inputManager;
    private Rigidbody rigidBody;
    private float moveSpeed;



    private void Awake()
    {
        tank = GetComponent<Tank>();
        rigidBody = GetComponent<Rigidbody>();

        ResetSpeed();

        tank.OnSpeedChanged += Tank_OnSpeedChanged;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        inputManager.EnableControls();

        GameManager.Instance.OnIntervalEnded += GameManager_OnIntervalEnded;

    }


    private void FixedUpdate()
    {
        if (IsOwner)
        {
            GetTankMovement();
            GetTankRotation();
            TurnTurret();
        }
    }

    private void ResetSpeed()
    {
        CancelInvoke(nameof(ResetSpeed));
        moveSpeed = defaultMoveSpeed;
    }

    private void GetTankMovement()
    {
        Vector3 moveAmount = transform.forward * inputManager.GetMovementDirection();
        rigidBody.velocity = moveAmount * Time.deltaTime * moveSpeed;
    }

    private void GetTankRotation()
    {
        float rotation = inputManager.GetTankRotationDirection();
        rigidBody.MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles + transform.up * rotation * Time.deltaTime * rotateSpeed));
    }

    private void TurnTurret()
    {
        turret.LookAt(inputManager.GetMouseWorldPosition());
        turret.eulerAngles = new Vector3(0, turret.eulerAngles.y, 0);
    }

    private void GameManager_OnIntervalEnded(object sender, EventArgs e)
    {
        ResetSpeed();
    }

    private void Tank_OnSpeedChanged(object sender, Tank.OnSpeedChangedArgs e)
    {
        ResetSpeed();
        moveSpeed *= (1 + e.speedIncreasePercentage);
        Invoke(nameof(ResetSpeed), e.activeTime);
    }
}
