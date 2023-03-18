using System;
using UnityEngine;

public class TankAudio : MonoBehaviour
{
    [SerializeField] private AudioClip cannonShotAudio;
    [SerializeField] private AudioClip tankDestroyedAudio;
    
    private Tank tank;
    private TankFiring tankFiring;

    private AudioSource tankAudioSource;

    private void Awake()
    {
        tankAudioSource = GetComponent<AudioSource>();

        tank = GetComponent<Tank>();
        tankFiring = GetComponent<TankFiring>();
    }
    private void Start()
    {
        tank.OnTankDestroyed += Tank_OnTankDestroyed;
        tankFiring.OnCannonShot += TankFiring_OnCannonShot;
    }
    private void OnDisable()
    {
        tank.OnTankDestroyed -= Tank_OnTankDestroyed;
        tankFiring.OnCannonShot -= TankFiring_OnCannonShot;
    }

    private void Tank_OnTankDestroyed(object sender, EventArgs e)
    {
        tankAudioSource.clip = tankDestroyedAudio;
        tankAudioSource.Play();
    }

    private void TankFiring_OnCannonShot(object sender, EventArgs e)
    {
        if(GameManager.Instance.CurrentGameState == GameStates.OnGoing)
        {
            tankAudioSource.clip = cannonShotAudio;
            tankAudioSource.Play();
        }
    }
}