using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToScreenButton : MonoBehaviour
{
    public void GoToScreen(string screen)
    {
        ScreenManager.Instance.LoadScreen(screen);
    }
}
