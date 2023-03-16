using System;
using Unity.VisualScripting;
using UnityEngine;

public class Shell : MonoBehaviour, IDamageReceiver, IDamageGiver
{
    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private float moveSpeed;

    private ParticleSystem explosion;

    private void Awake()
    {
        explosion = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosion.gameObject.SetActive(false);

        Invoke(nameof(TakeDamage), 10);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    public void GiveDamage(IDamageReceiver other)
    {
        // Spawn Particle
        other?.TakeDamage();

        PlayParticle();
        
        Destroy(gameObject);
    }

    private void PlayParticle()
    {
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);
        explosion.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        GiveDamage(other.GetComponent<IDamageReceiver>());
    }
}
