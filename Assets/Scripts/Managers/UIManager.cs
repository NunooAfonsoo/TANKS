using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UIManager : NetworkBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private TextMeshProUGUI getReadyTimeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private TextMeshProUGUI gameEndMessage;
    [SerializeField] private GameObject intervalPanel;
    [SerializeField] private TextMeshProUGUI intervalText;
    [SerializeField] private GameObject gamePausedPanel;

    private GameManager gameManager;
    private NetworkVariable<float> elapsedTime = new NetworkVariable<float>(0f);

    private const int GET_READY_DURATION = 5;
    private const int INTERVAL_DURATION = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (GameManager.Instance.IsMultiplayer)
        {
            ScoreManagerMultiplayer.Instance.OnPointScored += ScoreManagerMultiplayer_PointScored;
        }
        else
        {
            ScoreManager.Instance.OnPointScored += ScoreManager_PointScored;
        }

        gameManager.OnGameStartCountdownStarted += GameManager_OnGameStartCountdownStarted;
        gameManager.OnGameWon += GameManager_OnGameWon;
        gameManager.OnGameLost += GameManager_OnGameLost;
        gameManager.OnInterval += GameManager_OnInterval;
        InputManager.Instance.OnPausePerformed += InputManager_OnPausePerformed;
    }


    private void OnDisable()
    {
        gameManager.OnGameStartCountdownStarted -= GameManager_OnGameStartCountdownStarted;
        gameManager.OnGameWon -= GameManager_OnGameWon;
        gameManager.OnGameLost -= GameManager_OnGameLost;
        gameManager.OnInterval -= GameManager_OnInterval;
        InputManager.Instance.OnPausePerformed -= InputManager_OnPausePerformed;
    }

    private void Update()
    {
        if (IsServer)
        {
            elapsedTime.Value += Time.deltaTime;
            switch (gameManager.CurrentGameState)
            {
                case GameStates.Starting:
                    UpdateGetReadyTimeTextClientRpc(elapsedTime.Value);
                    if (elapsedTime.Value >= GET_READY_DURATION)
                    {
                        StartGameClientRpc();
                    }
                    break;
                case GameStates.OnGoing:
                    UpdateOnGoingTimeTextClientRpc(elapsedTime.Value);
                    break;
                case GameStates.Interval:
                    UpdateIntervalTextClientRpc(elapsedTime.Value);
                    if (elapsedTime.Value >= INTERVAL_DURATION)
                    {
                        StartNewRoundClientRpc();
                    }
                    break;
            }
        }
    }

    private void StartGame()
    {
        getReadyTimeText.gameObject.SetActive(false);
        startGamePanel.SetActive(false);
        elapsedTime = new NetworkVariable<float>(0f);
        gameManager.StartCountdownEnded();
    }

    public void UpdateAmmoUI(int currentAmmunition)
    {
        ammoText.text = "Ammo: " + currentAmmunition.ToString();
    }

    private void ScoreManagerMultiplayer_PointScored(object sender, ScoreManagerMultiplayer.OnPointScoredMultiplayerArgs e)
    {
        scoreText.text = "P1: " + e.scoreHost + "\nP2: " + e.scoreClient;
    }

    private void ScoreManager_PointScored(object sender, ScoreManager.OnPointScoredArgs e)
    {
        scoreText.text = "Score: " + e.score;
    }

    private void GameManager_OnGameStartCountdownStarted(object sender, EventArgs e)
    {
        elapsedTime = new NetworkVariable<float>(0f);
    }

    private void GameManager_OnGameWon(object sender, EventArgs e)
    {
        gameEndPanel.SetActive(true);
        if (gameManager.IsMultiplayer)
        {
            gameEndMessage.text = "Congratulations, you Won!";
        }
        else
        {
            gameEndMessage.text = "Congratulations! You Destroyed 20 cacti in " + elapsedTime.Value.ToString("F2") + " seconds!";
        }
    }

    private void GameManager_OnGameLost(object sender, EventArgs e)
    {
        gameEndPanel.SetActive(true);
        gameEndMessage.text = "You Lost.\nBetter luck next time.";
    }

    private void GameManager_OnInterval(object sender, EventArgs e)
    {
        intervalPanel.SetActive(true);
        elapsedTime = new NetworkVariable<float>(0f);
    }

    private void InputManager_OnPausePerformed(object sender, EventArgs e)
    {
        gamePausedPanel.SetActive(true);
    }

    #region RpcCalls

    [ClientRpc]
    private void UpdateGetReadyTimeTextClientRpc(float elapsedTime)
    {
        getReadyTimeText.text = (GET_READY_DURATION - elapsedTime).ToString("F0");
    }

    [ClientRpc]
    private void StartGameClientRpc()
    {
        StartGame();
    }

    [ClientRpc]
    private void UpdateOnGoingTimeTextClientRpc(float elapsedTime)
    {
        timeText.text = "Time: " + elapsedTime.ToString("F2");
    }

    [ClientRpc]
    private void UpdateIntervalTextClientRpc(float elapsedTime)
    {
        intervalText.text = "Get ready for another round!\n" + (INTERVAL_DURATION - elapsedTime).ToString("F0") + " s";
    }

    [ClientRpc]
    private void StartNewRoundClientRpc()
    {
        intervalPanel.SetActive(false);
        elapsedTime = new NetworkVariable<float>(0f);
        gameManager.IntervalEnded();
    }

    #endregion
}