public interface IDamageReceiver
{
    public void TakeDamage(bool isFromHost);
    public bool IsServer();
}
