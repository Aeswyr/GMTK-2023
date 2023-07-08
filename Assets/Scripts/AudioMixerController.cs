using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : ScriptableObject
{
    [SerializeField] private AudioMixer _audioMixer;

    public void SetMasterVolume(float value)
    {
        _audioMixer.SetFloat("master/volume", PercentToDb(value));
    }

    public void SetSFXVolume(float value)
    {
        _audioMixer.SetFloat("sfx/volume", PercentToDb(value));
    }

    public void SetMusicVolume(float value)
    {
        _audioMixer.SetFloat("music/volume", PercentToDb(value));
    }

    private float PercentToDb(float percent) => Mathf.Log10(percent) * 20;
}
