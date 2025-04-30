using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private AudioClip _click;
    [SerializeField] private GameObject _vibrationOnButton;
    [SerializeField] private GameObject _vibrationOffButton;

    public static bool isVibrationEnabled;

    private void Start()
    {
        Time.timeScale = 1;

        Vibration.Init();
        int vibrationPreference = PlayerPrefs.GetInt("vibrationPreference", 1);
        isVibrationEnabled = vibrationPreference == 1;
        if (isVibrationEnabled) EnableVibration(); else DisableVibration();

        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        _musicSource.volume = _musicSlider.value;
        _soundsSource.volume = _soundSlider.value;
        _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        _soundSlider.onValueChanged.AddListener(SetSoundVolume);
    }

    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSoundVolume(float volume)
    {
        _soundsSource.volume = volume;
        PlayerPrefs.SetFloat("SoundVolume", volume);
        PlayerPrefs.Save();
    }

    public void PlayClick()
    {
        _soundsSource.PlayOneShot(_click);
        if (isVibrationEnabled)
            Vibration.VibratePop();
    }

    public void DisableVibration()
    {
        isVibrationEnabled = false;
        PlayerPrefs.SetInt("vibrationPreference", 0);
        _vibrationOnButton.SetActive(false);
        _vibrationOffButton.SetActive(true);
    }

    public void EnableVibration()
    {
        isVibrationEnabled = true;
        PlayerPrefs.SetInt("vibrationPreference", 1);
        _vibrationOffButton.SetActive(false);
        _vibrationOnButton.SetActive(true);
    }
}
