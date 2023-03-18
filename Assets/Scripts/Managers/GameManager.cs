using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    [SerializeField] private bool isMultiplayer;
    public bool IsMultiplayer
    {
        get { return isMultiplayer; }
    }
    [SerializeField] private List<Transform> spawnPoints;
    public List<Transform> SpawnPoints
    {
        get { return spawnPoints; }
    }

    public GameStates CurrentGameState { get; private set; }

    public event EventHandler OnGameStartCountdownStarted;
    public event EventHandler OnGameCountdownEnded;
    public event EventHandler OnInterval;
    public event EventHandler OnIntervalEnded;
    public event EventHandler OnGameWon;
    public event EventHandler OnGameLost;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (isMultiplayer)
        {
            CurrentGameState = GameStates.ChoosingNetworkStatus;
        }
        else
        {
            NetworkManager.Singleton.StartHost();
            CurrentGameState = GameStates.Starting;
        }

        InputManager.Instance.OnPausePerformed += InputManager_OnPausePerformed;
    }

    public void NetworkStatusChosen()
    {
        if (!IsServer)
        {
            Invoke(nameof(NetworkStatusChosenServerRpc), 1);
        }
    }

    public Vector3 GetRandomSpawnPoint()
    {
        int index = UnityEngine.Random.Range(0, spawnPoints.Count);
        Vector3 spawnPoint = spawnPoints[index].position;
        spawnPoints.RemoveAt(index);

        return spawnPoint;
    }

    public void StartCountdownEnded()
    {
        if (IsServer)
        {
            StartCountdownEndedServerRpc();
        }
    }

    public void Interval()
    {
        CurrentGameState = GameStates.Interval;
        OnInterval?.Invoke(this, EventArgs.Empty);
    }
    
    public void IntervalEnded()
    {
        if (IsServer)
        {
            IntervalEndedServerRpc();
        }
    }

    public void GameWon()
    {
        CurrentGameState = GameStates.GameEnd;
        OnGameWon.Invoke(this, EventArgs.Empty);
    }

    public void GameLost()
    {
        CurrentGameState = GameStates.GameEnd;
        OnGameLost.Invoke(this, EventArgs.Empty);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        InputManager.Instance.EnableControls();
    }

    private void InputManager_OnPausePerformed(object sender, EventArgs e)
    {
        if (!isMultiplayer)
        {
            Time.timeScale = 0;
        }
    }

    #region RpcCalls

    [ServerRpc]
    private void StartCountdownEndedServerRpc()
    {
        StartCountdownEndedClientRpc();
    }

    [ClientRpc]
    private void StartCountdownEndedClientRpc()
    {
        CurrentGameState = GameStates.OnGoing;
        OnGameCountdownEnded.Invoke(this, EventArgs.Empty);
    }

    [ServerRpc(RequireOwnership = false)]
    private void NetworkStatusChosenServerRpc()
    {
        CurrentGameState = GameStates.Starting;
        OnGameStartCountdownStarted?.Invoke(this, EventArgs.Empty);
    }

    [ServerRpc]
    private void IntervalEndedServerRpc()
    {
        IntervalEndedClientRpc();
    }

    [ClientRpc]
    private void IntervalEndedClientRpc()
    {
        CurrentGameState = GameStates.OnGoing;
        OnIntervalEnded?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}