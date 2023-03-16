using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Windows;

public class Tank : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    private Rigidbody rigidBody;
    private InputManager inputManager;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void FixedUpdate()
    {
        GetTankMovement();
        GetTankRotation();
    }

    private void GetTankMovement()
    {
        Vector3 moveAmount = transform.forward * inputManager.GetTankMovementAmount().magnitude;
        rigidBody.MovePosition(transform.localPosition + moveAmount * Time.deltaTime * moveSpeed);
    }

    private void GetTankRotation()
    {
        Vector3 rotationAmount = inputManager.GetTankRotationAmount();

        transform.Rotate(rotationAmount);
    }
}
