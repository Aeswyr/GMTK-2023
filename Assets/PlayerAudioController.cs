using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> stepClips = new List<AudioClip>();

    public void PlayStepSFX()
    {
        if (stepClips.Count <= 0) return;
        SFX.Play(stepClips[Random.Range(0, stepClips.Count - 1)]);
    }
}
