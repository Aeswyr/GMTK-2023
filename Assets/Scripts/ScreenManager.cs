using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            LoadScreen("MainMenu");
        if (Input.GetKeyDown(KeyCode.Alpha2))
            LoadScreen("Gameplay");
    }

    public void LoadScreen(string screenName)
    {
        switch(screenName)
        {
            case "MainMenu":
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            case "Gameplay":
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
        }
    }
}
