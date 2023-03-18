using Unity.Netcode;
using UnityEngine;

public class Shell : NetworkBehaviour, IDamageReceiver, IDamageGiver
{
    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private float moveSpeed;

    private bool isShooterTheHost;
    private ParticleSystem explosion;

    private const float AUTO_DESTROY_TIME = 5;

    private void Awake()
    {
        Invoke(nameof(TakeDamage), AUTO_DESTROY_TIME);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GiveDamage(collision.gameObject.GetComponent<IDamageReceiver>(), collision.contacts[0].point);
    }

    public void TakeDamage(bool isFromHost)
    {
        Destroy(gameObject);
    }

    public new bool IsServer()
    {
        return false;
    }

    public void GiveDamage(IDamageReceiver other, Vector3 impactPosition)
    {
        other?.TakeDamage(isShooterTheHost);

        PlayParticleClientRPC(impactPosition);

        DestroySpawnedObjectServerRPC();
    }

    public void SetIsShooterHost(bool isHost)
    {
        isShooterTheHost = isHost;
    }

    #region RpcCalls

    [ClientRpc]
    private void PlayParticleClientRPC(Vector3 impactPosition)
    {
        explosion = Instantiate(explosionPrefab, impactPosition, Quaternion.identity).GetComponent<ParticleSystem>();
        explosion.Play();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroySpawnedObjectServerRPC()
    {
        Destroy(gameObject);
    }

    #endregion
}