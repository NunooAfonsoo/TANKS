using Cinemachine;
using System;
using Unity.Netcode;
using UnityEngine;

public class Tank : NetworkBehaviour, IDamageReceiver, IPowerUpReceiver
{

    [SerializeField] private GameObject tankNormal;
    [SerializeField] private GameObject tankDestroyed;

    private GameManager gameManager;
    private InputManager inputManager;
    private bool destroyed;
    private NetworkVariable<int> shieldAmount = new NetworkVariable<int>(0);

    private const int MAX_SHIELD_AMOUNT = 1;
    
    public event EventHandler OnTankDestroyed;
    public event EventHandler<OnSpeedChangedArgs> OnSpeedChanged;
    public class OnSpeedChangedArgs : EventArgs
    {
        public float speedIncreasePercentage;
        public float activeTime;
        public OnSpeedChangedArgs(float speedIncreasePercentage, float activeTime)
        {
            this.speedIncreasePercentage = speedIncreasePercentage;
            this.activeTime = activeTime;
        }
    }
    public event EventHandler<OnAmmoAddedArgs> OnAmmoAdded;
    public class OnAmmoAddedArgs : EventArgs
    {
        public int ammoAmount;
        public OnAmmoAddedArgs(int ammoAmount)
        {
            this.ammoAmount = ammoAmount;
        }
    }

    private void Awake()
    {
        ResetStats();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        inputManager = InputManager.Instance;
        inputManager.EnableControls();

        if (gameManager.IsMultiplayer)
        {
            transform.position = gameManager.GetRandomSpawnPoint();
        }

        if (IsLocalPlayer)
        {
            SetupCinemachine();
        }

        gameManager.OnIntervalEnded += GameManager_OnIntervalEnded;
    }

    private void SetupCinemachine()
    {
        CinemachineVirtualCamera virtualCamera = GameObject.Find("CMCam").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.LookAt = transform;
        virtualCamera.Follow = transform;
    }

    private void ResetStats()
    {
        destroyed = false;
        shieldAmount.Value = 0;
    }

    private void ResetVisuals()
    {
        tankNormal.SetActive(true);
        tankDestroyed.SetActive(false);
    }

    private void ResetPosition()
    {
        transform.position = gameManager.GetRandomSpawnPoint();
    }

    public void TakeDamage(bool isFromHost)
    {
        if(!destroyed)
        {
            if (shieldAmount.Value == 0)
            {
                destroyed = true;
                PointScoredServerRpc(isFromHost);
            }
            else
            {
                shieldAmount.Value--;
            }
        }
    }

    public new bool IsServer()
    {
        return base.IsServer;
    }

    public void AddAmmo(int ammoAmount)
    {
        if (IsLocalPlayer)
        {
            OnAmmoAdded?.Invoke(this, new OnAmmoAddedArgs(ammoAmount));
        }
    }

    public void IncreaseSpeed(float speedIncreasePercentage, float activeTime)
    {
        OnSpeedChanged?.Invoke(this, new OnSpeedChangedArgs(speedIncreasePercentage, activeTime));
    }

    public void AddShield()
    {
        shieldAmount.Value++;
        shieldAmount.Value = Mathf.Clamp(shieldAmount.Value, 0, MAX_SHIELD_AMOUNT);
    }

    private void GameManager_OnIntervalEnded(object sender, EventArgs e)
    {
        ResetStats();
        ResetVisuals();
        ResetPosition();

        inputManager.EnableControls();
    }

    #region RpcCalls

    [ServerRpc(RequireOwnership = false)]
    private void PointScoredServerRpc(bool isFromHost)
    {
        PointScoredClientRpc(isFromHost);
        ShowDestroyedTankClientRpc();
    }

    [ClientRpc]
    private void PointScoredClientRpc(bool isFromHost)
    {
        ScoreManagerMultiplayer.Instance.PointScored(isFromHost);
    }

    [ClientRpc]
    private void ShowDestroyedTankClientRpc()
    {
        inputManager.DisableTankControls();
        tankNormal.SetActive(false);
        tankDestroyed.SetActive(true);
        OnTankDestroyed.Invoke(this, EventArgs.Empty);
    }

    #endregion
}