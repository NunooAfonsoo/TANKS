using UnityEngine;

public class Cactus : MonoBehaviour, IDamageReceiver, ISpawnable
{
    private ISpawner spawner;

    public void SetSpawner(ISpawner spawner)
    {
        this.spawner = spawner;
    }

    public void TakeDamage(bool isFromHost)
    {
        spawner.CanSpawn();
        ScoreManager.Instance.PointScored();

        Destroy(gameObject);
    }

    bool IDamageReceiver.IsServer()
    {
        return false;
    }
}
