using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAudio : MonoBehaviour
{
    [SerializeField] private AudioClip cannonShotAudio;
    [SerializeField] private AudioClip tankDestroyedAudio;
    
    private AudioSource tankAudioSource;
    private Tank tank;

    private void Awake()
    {
        tankAudioSource = GetComponent<AudioSource>();
        tank = GetComponent<Tank>();
    }
    private void Start()
    {
        InputManager.Instance.OnCannonShot += InputManager_OnCannonShot;
        tank.OnTankDestroyed += Tank_OnTankDestroyed;
    }
    private void OnDisable()
    {
        InputManager.Instance.OnCannonShot -= InputManager_OnCannonShot;
        tank.OnTankDestroyed -= Tank_OnTankDestroyed;

    }

    private void InputManager_OnCannonShot(object sender, EventArgs e)
    {
        tankAudioSource.clip = cannonShotAudio;
        tankAudioSource.Play();
    }

    private void Tank_OnTankDestroyed(object sender, EventArgs e)
    {
        tankAudioSource.clip = tankDestroyedAudio;
        tankAudioSource.Play();
    }
}
