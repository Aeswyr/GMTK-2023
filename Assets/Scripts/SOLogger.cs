using UnityEngine;
using UnityEngine.Audio;

public class SOLogger : ScriptableObject
{
    public void Log(string message)
    {
        Debug.Log(message);
    }
}
