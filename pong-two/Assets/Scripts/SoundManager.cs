using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip hitSound;
    public AudioClip powerUpSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlatHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void PlayPowerUpSound()
    {
        audioSource.PlayOneShot(powerUpSound);
    }
}
