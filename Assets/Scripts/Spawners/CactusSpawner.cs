using UnityEngine;

public class CactusSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject cactusPrefab;

    private ScoreManager scoreManager;

    private const float MAX_SPAWN_INTERVAL = 7f;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;

        GameManager.Instance.OnGameCountdownEnded += GameManager_OnGameCountdownEnded;
    }

    private void GameManager_OnGameCountdownEnded(object sender, System.EventArgs e)
    {
        Invoke(nameof(CanSpawn), Random.Range(0f, MAX_SPAWN_INTERVAL));
    }

    public void Spawn()
    {
        if(scoreManager.CanSpawnCactus())
        {
            CancelInvoke(nameof(Spawn));

            ISpawnable cactus = Instantiate(cactusPrefab, spawnPosition.position, Quaternion.identity, spawnPosition).GetComponent<ISpawnable>();
            cactus.SetSpawner(this);
            scoreManager.CactusSpawned();
        }
    }

    public void CanSpawn()
    {
        Invoke(nameof(Spawn), Random.Range(2f, MAX_SPAWN_INTERVAL));
    }
}
