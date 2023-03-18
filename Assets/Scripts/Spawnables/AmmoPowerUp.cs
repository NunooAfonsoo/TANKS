using Unity.Netcode;
using UnityEngine;

public class AmmoPowerUp : NetworkBehaviour, IPowerUp, ISpawnable
{
    private ISpawner spawner;

    private const int ADD_AMOUNT = 5;
   private void OnTriggerEnter(Collider other)
    {
        GivePower(other.GetComponent<IPowerUpReceiver>());
    }

    public void GivePower(IPowerUpReceiver other)
    {
        CanSpawnServerRPC();

        other.AddAmmo(ADD_AMOUNT);

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
