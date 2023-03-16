using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject cactusPrefab;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnGameCountdownEnded += GameManager_OnGameCountdownEnded;
    }

    private void GameManager_OnGameCountdownEnded(object sender, System.EventArgs e)
    {
        InvokeRepeating(nameof(CanSpawn), Random.Range(0f, 5f), Random.Range(3f, 10f));
    }


    public void Spawn()
    {
        if(gameManager.CanSpawnCactus())
        {
            CancelInvoke(nameof(Spawn));

            ISpawnable cactus = Instantiate(cactusPrefab, spawnPosition.position, Quaternion.identity, spawnPosition).GetComponent<ISpawnable>();
            cactus.SetSpawner(this);
            gameManager.CactusSpawned();
        }
    }

    public void CanSpawn()
    {
        InvokeRepeating(nameof(Spawn), Random.Range(2f, 7f), Random.Range(3f, 10f));
    }
}
