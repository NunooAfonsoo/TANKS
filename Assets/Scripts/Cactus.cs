using UnityEngine;

public class Cactus : MonoBehaviour, IDamageReceiver, ISpawnable
{
    private ISpawner spawner;

    public void TakeDamage()
    {
        spawner.CanSpawn();
        GameManager.Instance.CactusDestroyed();

        Destroy(gameObject);
    }

    public void SetSpawner(ISpawner spawner)
    {
        this.spawner = spawner;
    }
}
