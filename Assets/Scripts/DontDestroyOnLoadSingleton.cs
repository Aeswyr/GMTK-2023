using UnityEngine;

public class DontDestroyOnLoadSingleton : MonoBehaviour
{
    public static DontDestroyOnLoadSingleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
