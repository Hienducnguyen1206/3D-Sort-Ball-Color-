using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  

    public static AudioManager instance;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioClip musicAudio;
    [SerializeField] AudioClip buttonSfxAudio;
    [SerializeField] AudioClip levelCompletedSfxAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        
    }




  
    void Update()
    {
        
    }

    public void PlayBackgroundMusic()
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.Play();
        }
    }

    public void PlayButtonSFX()
    {   

        if (sfxAudioSource != null)
        {
            sfxAudioSource.clip = buttonSfxAudio;
            sfxAudioSource.Play();
        }
    }
}
