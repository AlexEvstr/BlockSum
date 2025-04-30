using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _minusLife;
    [SerializeField] private AudioClip _letterSound;
    [SerializeField] private AudioClip _win;
    [SerializeField] private AudioClip _lose;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;


    public static bool isVibrationEnabled;

    private void Start()
    {
        Time.timeScale = 1;
        Vibration.Init();
        int vibrationPreference = PlayerPrefs.GetInt("vibrationPreference", 1);
        isVibrationEnabled = vibrationPreference == 1;
        _musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        _soundSource.volume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
    }

    public void PlayClick()
    {
        _soundSource.PlayOneShot(_click);
        if (isVibrationEnabled)
            Vibration.VibratePop();
    }

    public void PlayStarSound()
    {
        _soundSource.PlayOneShot(_letterSound);
        if (isVibrationEnabled)
            Vibration.VibratePeek();
    }

    public void PlayMinusLife()
    {
        _soundSource.PlayOneShot(_minusLife);
        if (isVibrationEnabled)
            Vibration.VibratePeek();
    }

    public void PlayWin()
    {
        _soundSource.PlayOneShot(_win);
        if (isVibrationEnabled)
            Vibration.Vibrate();
    }

    public void PlayLose()
    {
        _soundSource.PlayOneShot(_lose);
        if (isVibrationEnabled)
            Vibration.VibrateNope();
    }
}