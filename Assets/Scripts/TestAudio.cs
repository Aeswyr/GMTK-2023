using UnityEngine;
using UnityEngine.InputSystem;

public class TestAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    void Update()
    {
        if (Keyboard.current.tKey.wasReleasedThisFrame)
        {
            SFX.Play(_clip);
        }
    }
}
