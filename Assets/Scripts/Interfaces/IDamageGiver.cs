using UnityEngine;

public interface IDamageGiver
{
    public void GiveDamage(IDamageReceiver other, Vector3 impactPosition);
    public void SetIsShooterHost(bool isHost);
}
