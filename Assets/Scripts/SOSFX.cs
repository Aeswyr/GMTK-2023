using UnityEngine;

public class SOSFX : ScriptableObject
{
    public void Play(AudioClip clip)
    {
        SFX.Play(clip);
    }
}
