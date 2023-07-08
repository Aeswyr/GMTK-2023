using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceHelper : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayIfNotPlaying()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
}
