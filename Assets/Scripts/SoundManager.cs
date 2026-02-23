using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    
    public static SoundManager instance;

    void Start(){
        if(instance == null){
            instance = this;
        }
    }
    public void PlayAsteroidSound(){
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
}
