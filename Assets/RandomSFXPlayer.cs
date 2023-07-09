using System.Collections.Generic;
using UnityEngine;

public class RandomSFXPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();

    public void Play()
    {
        if (clips.Count <= 0) return;
        SFX.Play(clips[Random.Range(0, clips.Count - 1)]);
    }
}
