using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _move;
    [SerializeField] private AudioClip _merge;
    [SerializeField] private AudioClip _finish;
    [SerializeField] private AudioClip _emotion;
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

    public void PlayMergeSound()
    {
        _soundSource.PlayOneShot(_merge);
        if (isVibrationEnabled)
            Vibration.VibratePeek();
    }

    public void PlayMoveSound()
    {
        _soundSource.PlayOneShot(_move);
        if (isVibrationEnabled)
            Vibration.VibratePeek();
    }

    public void PlayFinishSound()
    {
        _soundSource.PlayOneShot(_finish);
        if (isVibrationEnabled)
            Vibration.Vibrate();
    }

    public void PlayEmotionSound()
    {
        _soundSource.PlayOneShot(_emotion);
        if (isVibrationEnabled)
            Vibration.VibrateNope();
    }
}