using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private TextMeshProUGUI getReadyTimeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private TextMeshProUGUI gameWonMessage;

    private GameManager gameManager;
    private float elapsedTime;
    private const int GET_READY_TIME = 5;

    private void Awake()
    {
        Instance = this;

        elapsedTime = 0;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnCactusDestroyed += GameManager_OnCactusDestroyed;
        gameManager.OnGameWon += GameManager_OnGameWon;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        switch (gameManager.CurrentGameState)
        {
            case GameManager.GameState.Starting:
                getReadyTimeText.text = (GET_READY_TIME - elapsedTime).ToString("F0");
                if(elapsedTime >= GET_READY_TIME)
                {
                    StartGame();
                }
                break;
            case GameManager.GameState.OnGoing:
                timeText.text = "Time: " + elapsedTime.ToString("F2");
                break;
        }        
    }

    private void StartGame()
    {
        getReadyTimeText.gameObject.SetActive(false);
        startGamePanel.SetActive(false);
        elapsedTime = 0;
        gameManager.StartCountdownEnded();
    }

    private void GameManager_OnGameWon(object sender, EventArgs e)
    {
        gameWonPanel.SetActive(true);
        gameWonMessage.text = "Congratulations! You Destroyed 20 cacti in " + elapsedTime.ToString("F2") + " seconds!";
    }

    private void GameManager_OnCactusDestroyed(object sender, GameManager.OnCactusDestroyedArgs e)
    {
        scoreText.text = "Score: " + e.score;
    }

    internal void UpdateAmmoUI(int currentAmmunition)
    {
        ammoText.text = "Ammo: " + currentAmmunition.ToString();
    }
}