using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayScreen : Singleton<GameplayScreen>
{
    public float time;
    public const float RealSecondsToInGameHours = 1;

    public enum State
    {
        Gameplay,
        GameOver,
    }

    public State CurrentState = State.Gameplay;

    public void Update()
    {
        switch(CurrentState)
        {
            case State.Gameplay:
                {
                    time += Time.deltaTime;
                    if (time / RealSecondsToInGameHours >= 12.0f)
                        CurrentState = State.GameOver;
                }
                break;
        }
    }

    // mapping 'time' to in game clock
    // clock starts at 8am ends at 8pm
    public enum TimeAMPM
    {
        AM,
        PM,
    }
    public (int, int, TimeAMPM) GetTimeMinutesHours()
    {
        float hours = time / RealSecondsToInGameHours;
        float minutes = Mathf.Repeat(time / RealSecondsToInGameHours, 1.0f);

        int hoursInt = Mathf.FloorToInt(hours) + 8;
        TimeAMPM ampm = TimeAMPM.AM;
        if (hoursInt > 11) 
            ampm = TimeAMPM.PM;
        if (hoursInt > 12)
            hoursInt %= 12;
        int minutesInt = Mathf.FloorToInt(minutes * 60);

        return (hoursInt, minutesInt, ampm);
    }
}
