using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceFade : MonoBehaviour
{
    [SerializeField] private float _fadeSpeed = 1f;
    [SerializeField] private float _fadeFrom = 0f;
    [SerializeField] private float _fadeTo = 1f;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _fadeFrom;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        while (!Mathf.Approximately(_audioSource.volume, _fadeTo))
        {
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, _fadeTo, Time.deltaTime * _fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
