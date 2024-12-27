using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
  

    public static AudioManager instance;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioClip musicAudio;
    [SerializeField] AudioClip buttonSfxAudio;
    [SerializeField] AudioClip levelCompletedSfxAudio;

    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider MusicSlider;

  




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
        PlayBackgroundMusic();
    }




  
    void Update()
    {
        SFXSlider.onValueChanged.AddListener(value =>
        {
            sfxAudioSource.volume = value;     
           
        });

        MusicSlider.onValueChanged.AddListener(value =>
        {
            musicAudioSource.volume = value;
           
        });

      






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

    public void changeSFXAudioSorceState()
    {   
       
        if (sfxAudioSource.mute == false)
        {
            sfxAudioSource.mute = true;
        }
        else
        {
            sfxAudioSource.mute = false;
        }
    }

    public void changeMusicAudioSorceState()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Pause();
        }
        else
        {
            musicAudioSource.Play();
        }
    }
}
