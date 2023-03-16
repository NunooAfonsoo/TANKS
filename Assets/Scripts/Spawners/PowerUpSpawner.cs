using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform SpawnPosition;
    [SerializeField] private List<GameObject> powerUpPrefabs;
    private const float SPAWN_INTERVAL = 5;

    private void Start()
    {
        GameManager.Instance.OnGameCountdownEnded += GameManager_OnGameCountdownEnded;
    }

    private void GameManager_OnGameCountdownEnded(object sender, System.EventArgs e)
    {
        InvokeRepeating(nameof(Spawn), Random.Range(0f, 5f), SPAWN_INTERVAL);
    }

    public void Spawn()
    {
        CancelInvoke(nameof(Spawn));

        int prefabIndex = Random.Range(0, powerUpPrefabs.Count);
        ISpawnable powerUp = Instantiate(powerUpPrefabs[prefabIndex], SpawnPosition.position, Quaternion.identity, SpawnPosition).GetComponent<ISpawnable>();
        powerUp.SetSpawner(this);
    }

    public void CanSpawn()
    {
        InvokeRepeating(nameof(Spawn), Random.Range(2f, 7f), SPAWN_INTERVAL);
    }
}
