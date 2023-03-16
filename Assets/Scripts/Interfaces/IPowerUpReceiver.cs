public interface IPowerUpReceiver
{
    public void AddAmmo(int ammoAmount);
    public void IncreaseSpeed(float speedIncreasePercentage, float activeTime);
    public void AddShield();
}
