using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class SFX : MonoBehaviour
{
    private AudioMixer _audioMixer;

    private IObjectPool<AudioSource> _pool = null;
    private IObjectPool<AudioSource> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<AudioSource>(() =>
                {
                    var gameObject = new GameObject();
                    gameObject.name = typeof(AudioSource).Name;
                    var audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.playOnAwake = false;
                    audioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SFX").First();
                    return audioSource;
                }, (audioSource) =>
                {
                    audioSource.gameObject.SetActive(true);
                }, (audioSource) =>
                {
                    audioSource.gameObject.SetActive(false);
                });
            }
            return _pool;
        }
    }

    private static SFX _instance = null;
    private static SFX Instance
    {
        get
        {
            if (!_instance)
            {

                var gameObject = new GameObject();
                gameObject.name = typeof(SFX).Name;
                var sfx = gameObject.AddComponent<SFX>();
                sfx._audioMixer = Resources.Load<AudioMixer>("mixer");
                _instance = sfx;
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }


    public static void Play(AudioClip clip)
    {
        var audioSource = Instance.Pool.Get();
        audioSource.clip = clip;
        audioSource.Play();
        Instance.StartCoroutine(ReleaseAfterLength(audioSource, clip));
    }

    private static IEnumerator ReleaseAfterLength(AudioSource audioSource, AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        Instance.Pool.Release(audioSource);
    }
}
