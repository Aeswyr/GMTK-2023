using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WallClock : MonoBehaviour
{
    GameplayScreen gameplayScreen;
    Text text;

    void Start()
    {
        gameplayScreen = GameplayScreen.Instance;
        text = GetComponent<Text>();
    }

    void Update()
    {
        var hoursMinutes = gameplayScreen.GetTimeMinutesHours();
        text.text = $"{hoursMinutes.Item1}:{hoursMinutes.Item2:D2}{hoursMinutes.Item3}";
    }
}
