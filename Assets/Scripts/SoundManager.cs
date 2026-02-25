using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> shootClips = new List<AudioClip>();
    [SerializeField] private AudioClip engineClip;

    public static SoundManager instance;
    bool enginePlaying;


    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlayAsteroidSound() => Play(0);
    public void PlayDeathSound() => Play(1);
    public void PlayHitSound() => Play(2);
    public void PlayBlackHoleDeathSound() => Play(1);

    

    void Play(int index)
    {
        if (audioSource == null) return;
        if (index < 0 || index >= audioClips.Count) return;
        if (audioClips[index] == null) return;

        audioSource.PlayOneShot(audioClips[index]);
    }
    public void PlayShootSound()
    {
        if (shootClips.Count == 0) return;
        int randomIndex = Random.Range(0, shootClips.Count);
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(shootClips[randomIndex]);
        audioSource.pitch = 1f;
    }

    public void SetEngineLoop(bool shouldPlay)
    {
        if (audioSource == null || engineClip == null) return;

        if (shouldPlay)
        {
            if (enginePlaying) return;

            audioSource.clip = engineClip;
            audioSource.loop = true;
            audioSource.Play();
            enginePlaying = true;
        }
        else
        {
            if (!enginePlaying) return;

            audioSource.Stop();
            audioSource.loop = false;
            audioSource.clip = null;
            enginePlaying = false;
        }
    }
}