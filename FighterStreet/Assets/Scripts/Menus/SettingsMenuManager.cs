using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;

    private void Start()
    {
        // Load saved volume levels or default to 0
        float master = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 0f);

        // Apply volumes
        mainAudioMixer.SetFloat("Master", master);
        mainAudioMixer.SetFloat("Music", music);
        mainAudioMixer.SetFloat("SFX", sfx);

        // Update sliders
        masterVol.value = master;
        musicVol.value = music;
        sfxVol.value = sfx;
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("Master", masterVol.value);
        PlayerPrefs.SetFloat("MasterVolume", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("Music", musicVol.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVol.value);
    }

    public void ChangeSfxVolume()
    {
        mainAudioMixer.SetFloat("SFX", sfxVol.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVol.value);
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }

    private void OnDisable()
    {
        // Saves when the scene changes
        PlayerPrefs.Save();
    }
}
