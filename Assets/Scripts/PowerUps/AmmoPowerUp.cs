using UnityEngine;

public class AmmoPowerUp : MonoBehaviour, IPowerUp, ISpawnable
{
    private ISpawner spawner;
    private const int ADD_AMOUNT = 5;

    public void GivePower(IPowerUpReceiver other)
    {
        spawner.CanSpawn();

        other.AddAmmo(ADD_AMOUNT);
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
