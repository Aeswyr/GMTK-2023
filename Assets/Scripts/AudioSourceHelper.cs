using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceHelper : MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void PlayIfNotPlaying()
    {
        if (AudioSource.isPlaying) return;
        AudioSource.Play();
    }
}
