using System;
using Unity.Netcode;

public class ScoreManagerMultiplayer : NetworkBehaviour
{
    public static ScoreManagerMultiplayer Instance { get; private set; }

    private GameManager gameManager;
    private int hostPointsScored;
    private int clientPointsScored;

    private const int POINTS_TO_WIN = 3;
    public event EventHandler<OnPointScoredMultiplayerArgs> OnPointScored;
    public class OnPointScoredMultiplayerArgs : EventArgs
    {
        public int scoreHost;
        public int scoreClient;
        public OnPointScoredMultiplayerArgs(int scoreHost, int scoreClient)
        {
            this.scoreHost = scoreHost;
            this.scoreClient = scoreClient;
        }
    }

    private void Awake()
    {
        Instance = this;

        hostPointsScored = 0;
        clientPointsScored = 0;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void PointScored(bool isHost)
    {
        if (isHost)
        {
            hostPointsScored++;
            if (hostPointsScored == POINTS_TO_WIN)
            {
                if (isHost)
                {
                    gameManager.GameWon();
                }
                else
                {
                    gameManager.GameLost();
                }
            }
            else
            {
                Invoke(nameof(Interval), 1);
            }
        }
        else
        {
            clientPointsScored++;
            if (clientPointsScored == POINTS_TO_WIN)
            {
                if (!isHost)
                {
                    gameManager.GameWon();
                }
                else
                {
                    gameManager.GameLost();
                }
            }
            else
            {
                Invoke(nameof(Interval), 1);
            }
        }
        
        OnPointScored.Invoke(this, new OnPointScoredMultiplayerArgs(hostPointsScored, clientPointsScored));
    }

    private void Interval()
    {
        gameManager.Interval();
    }
}