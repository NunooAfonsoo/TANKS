using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, IPowerUp, ISpawnable
{
    private ISpawner spawner;
    private const float SPEED_INCREASE_PERCENTAGE = 0.3f;
    private const int ACTIVE_TIME = 5;

    public void GivePower(IPowerUpReceiver other)
    {
        spawner.CanSpawn();

        other.IncreaseSpeed(SPEED_INCREASE_PERCENTAGE, ACTIVE_TIME);
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
