using Unity.Netcode;
using UnityEngine;

public class SpeedPowerUp : NetworkBehaviour, IPowerUp, ISpawnable
{
    private ISpawner spawner;

    private const float SPEED_INCREASE_PERCENTAGE = 0.3f;
    private const int ACTIVE_TIME = 10;

    private void OnTriggerEnter(Collider other)
    {
        GivePower(other.GetComponent<IPowerUpReceiver>());
    }

    public void GivePower(IPowerUpReceiver other)
    {
        CanSpawnServerRPC();

        other.IncreaseSpeed(SPEED_INCREASE_PERCENTAGE, ACTIVE_TIME);

        DestroySpawnedObjectServerRPC();
    }

    public void SetSpawner(ISpawner spawner)
    {
        this.spawner = spawner;
    }

    #region RpcCalls

    [ServerRpc(RequireOwnership = false)]
    private void CanSpawnServerRPC()
    {
        spawner.CanSpawn();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroySpawnedObjectServerRPC()
    {
        Destroy(gameObject);
    }

    #endregion
}
