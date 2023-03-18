using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PowerUpSpawner : NetworkBehaviour, ISpawner
{
    [SerializeField] private Transform SpawnPosition;
    [SerializeField] private List<GameObject> powerUpPrefabs;

    private const float MAX_SPAWN_INTERVAL = 5f;

    private void Start()
    {
        GameManager.Instance.OnGameCountdownEnded += GameManager_OnGameCountdownEnded;
    }

    private void GameManager_OnGameCountdownEnded(object sender, System.EventArgs e)
    {
        Invoke(nameof(Spawn), MAX_SPAWN_INTERVAL);
    }

    public void Spawn()
    {
        if (IsServer)
        {
            SpawnPowerUpServerRpc();
        }
    }

    public void CanSpawn()
    {
        Invoke(nameof(Spawn), MAX_SPAWN_INTERVAL);
    }

    #region RpcCalls

    [ServerRpc]
    public void SpawnPowerUpServerRpc()
    {
        CancelInvoke(nameof(Spawn));

        int prefabIndex = Random.Range(0, powerUpPrefabs.Count);
        GameObject powerUp = Instantiate(powerUpPrefabs[prefabIndex], SpawnPosition.position, Quaternion.identity, SpawnPosition);

        powerUp.GetComponent<NetworkObject>().Spawn(true);
        
        SpawnPowerUpClientRpc(powerUp.GetComponent<NetworkObject>());
    }

    [ClientRpc]
    public void SpawnPowerUpClientRpc(NetworkObjectReference networkObjectReference)
    {
        networkObjectReference.TryGet(out NetworkObject networkObject);
        ISpawnable spawnable = networkObject.GetComponent<ISpawnable>();

        spawnable.SetSpawner(this);
    }

    #endregion
}
