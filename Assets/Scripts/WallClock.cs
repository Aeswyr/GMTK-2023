using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallClock : MonoBehaviour
{
    GameplayScreen gameplayScreen;
    TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        gameplayScreen = GameplayScreen.Instance;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var hoursMinutes = gameplayScreen.GetTimeMinutesHours();
        textMeshProUGUI.text = $"{hoursMinutes.Item1}:{hoursMinutes.Item2:D2}{hoursMinutes.Item3}";
    }
}
