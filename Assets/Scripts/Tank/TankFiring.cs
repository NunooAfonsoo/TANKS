using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using static Tank;

public class TankFiring : NetworkBehaviour, IShootable
{
    [SerializeField] private Transform turret;
    [SerializeField] private Transform cannonEnd;
    [SerializeField] private int defaultAmmunition;
    [SerializeField] private GameObject shellPrefab;

    private Tank tank;
    private InputManager inputManager;

    private int currentAmmunition;

    private const int MAX_AMMUNITION_AMOUNT = 15;

    public event EventHandler OnCannonShot;


    private void Awake()
    {
        tank = GetComponent<Tank>();

        ResetAmmunition();

        tank.OnAmmoAdded += Tank_OnAmmoAdded;
    }


    private void Start()
    {
        inputManager = InputManager.Instance;


        UIManager.Instance.UpdateAmmoUI(currentAmmunition);

        GameManager.Instance.OnIntervalEnded += GameManager_OnIntervalEnded;
        inputManager.OnCannonShot += InputManager_OnCannonShot;
    }

    private void OnDisable()
    {
        tank.OnAmmoAdded -= Tank_OnAmmoAdded;
        GameManager.Instance.OnIntervalEnded -= GameManager_OnIntervalEnded;
        inputManager.OnCannonShot -= InputManager_OnCannonShot;
    }

    private void ResetAmmunition()
    {
        currentAmmunition = defaultAmmunition;
    }

    public void Shoot()
    {
        ShootServerRpc(NetworkManager.IsHost);
    }

    private void Tank_OnAmmoAdded(object sender, OnAmmoAddedArgs e)
    {
        currentAmmunition += e.ammoAmount;
        currentAmmunition = Mathf.Clamp(currentAmmunition, 0, MAX_AMMUNITION_AMOUNT);
        UIManager.Instance.UpdateAmmoUI(currentAmmunition);
    }

    private void GameManager_OnIntervalEnded(object sender, EventArgs e)
    {
        ResetAmmunition();
    }

    private void InputManager_OnCannonShot(object sender, EventArgs e)
    {
        if (IsLocalPlayer && GameManager.Instance.CurrentGameState == GameStates.OnGoing)
        {
            if (currentAmmunition > 0)
            {
                Shoot();
                currentAmmunition--;
                OnCannonShot?.Invoke(this, EventArgs.Empty);
                UIManager.Instance.UpdateAmmoUI(currentAmmunition);
            }
        }
    }

    #region RpcCalls

    [ServerRpc(RequireOwnership = false)]
    public void ShootServerRpc(bool isHost)
    {
        GameObject shell = Instantiate(shellPrefab, cannonEnd.position, turret.rotation);
        shell.GetComponent<IDamageGiver>().SetIsShooterHost(isHost);

        shell.GetComponent<NetworkObject>().Spawn(true);
    }

    #endregion
}
