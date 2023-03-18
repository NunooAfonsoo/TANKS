using UnityEngine;

public class MapProp : MonoBehaviour, IDamageReceiver
{
    public void TakeDamage(bool isFromHost)
    {
    }

    bool IDamageReceiver.IsServer()
    {
        return false;
    }
}
