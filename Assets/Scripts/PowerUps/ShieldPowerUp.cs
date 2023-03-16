using UnityEngine;

public class ShieldPowerUp : MonoBehaviour, IPowerUp, ISpawnable
{
    private ISpawner spawner;

    public void GivePower(IPowerUpReceiver other)
    {
        spawner.CanSpawn();

        other.AddShield();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GivePower(other.GetComponent<IPowerUpReceiver>());
    }

    public void SetSpawner(ISpawner spawner)
    {
        this.spawner = spawner;
    }
}
