using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int NumberOfActiveCacti { get; private set; }

    private int pointsScored;

    private const int MAX_NUMBER_OF_CACTI = 5;

    public event EventHandler<OnPointScoredArgs> OnPointScored;
    public class OnPointScoredArgs : EventArgs
    {
        public int score;
        public OnPointScoredArgs(int score)
        {
            this.score = score;
        }
    }

    private void Awake()
    {
        Instance = this;

        pointsScored = 0;
        NumberOfActiveCacti = 0;
    }

    public bool CanSpawnCactus()
    {
        return NumberOfActiveCacti < MAX_NUMBER_OF_CACTI;
    }

    public void CactusSpawned()
    {
        NumberOfActiveCacti++;
    }


    public void PointScored()
    {
        pointsScored++;
        OnPointScored.Invoke(this, new OnPointScoredArgs(pointsScored));
        NumberOfActiveCacti--;

        if (pointsScored == 20)
        {
            GameManager.Instance.GameWon();

        }
    }
}