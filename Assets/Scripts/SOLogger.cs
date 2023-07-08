using UnityEngine;

public class SOLogger : ScriptableObject
{
    public void Log(string message)
    {
        Debug.Log(message);
    }
}
