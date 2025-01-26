using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sliders")]
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    
    [Header("Audio Sources")]
    public AudioSource backgroundMusic; // Audio source for background music
    public AudioSource sfxSource;
    
    private const string masterVolumeKey = "masterVolume";
    private const string sfxVolumeKey = "sfxVolume";

    private void Start()
    {
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
}
