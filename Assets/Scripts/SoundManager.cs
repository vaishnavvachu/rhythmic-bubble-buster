using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sliders")]
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    
    [Header("Audio Sources")]
    public AudioSource backgroundMusic;
    public AudioSource sfxSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField] private AudioClip buttonClickClip;
    
    private const string masterVolumeKey = "masterVolume";
    private const string sfxVolumeKey = "sfxVolume";

    private void Start()
    {
        
        if (backgroundMusic != null && backgroundMusicClip != null)
        {
            backgroundMusic.clip = backgroundMusicClip;
            backgroundMusic.loop = true; 
            backgroundMusic.Play();
        }
        
        float savedMasterVolume = PlayerPrefs.GetFloat(masterVolumeKey, 1f);
        float savedSfxVolume = PlayerPrefs.GetFloat(sfxVolumeKey, 1f);
        
        masterVolumeSlider.value = savedMasterVolume;
        sfxVolumeSlider.value = savedSfxVolume;

        UpdateMusicVolume(savedMasterVolume);
        UpdateSfxVolume(savedSfxVolume);
        
        
        masterVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(UpdateSfxVolume);
    }

    private void UpdateMusicVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume; 
        }
        PlayerPrefs.SetFloat(masterVolumeKey, volume);
        PlayerPrefs.Save();
    }

    private void UpdateSfxVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
        }
    
        PlayerPrefs.SetFloat(sfxVolumeKey, volume);
        PlayerPrefs.Save();
    }
    
    public void PlayButtonClickSound()
    {
        if (sfxSource != null)
        {
            sfxSource.Play();
        }
    }
}
