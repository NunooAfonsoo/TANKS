using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public const int MAX_NUMBER_OF_CACTI = 5;
    public int NumberOfActiveCacti { get; private set; }

    private int cactiDestroyed;
    public enum GameState
    {
        Starting,
        OnGoing,
        GameEnd
    }
    public GameState CurrentGameState { get; private set; }

    public event EventHandler OnGameWon;
    public event EventHandler OnGameCountdownEnded;
    public event EventHandler<OnCactusDestroyedArgs> OnCactusDestroyed;
    public class OnCactusDestroyedArgs : EventArgs
    {
        public int score;
        public OnCactusDestroyedArgs(int score)
        {
            this.score = score;
        }
    }

    private void Awake()
    {
        Instance = this;

        CurrentGameState = GameState.Starting;
        cactiDestroyed = 0;
        NumberOfActiveCacti = 0;
    }

    internal bool CanSpawnCactus()
    {
        return NumberOfActiveCacti < MAX_NUMBER_OF_CACTI;
    }

    public void CactusSpawned()
    {
        NumberOfActiveCacti++;
    }

    public void CactusDestroyed()
    {
        cactiDestroyed ++;
        OnCactusDestroyed.Invoke(this, new OnCactusDestroyedArgs(cactiDestroyed));
        NumberOfActiveCacti--;

        if (cactiDestroyed == 20)
        {
            CurrentGameState = GameState.GameEnd;
            OnGameWon.Invoke(this, EventArgs.Empty);
        }
    }

    public void StartCountdownEnded()
    {
        CurrentGameState = GameState.OnGoing;
        OnGameCountdownEnded.Invoke(this, EventArgs.Empty);
    }
}
