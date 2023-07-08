using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayScreen : Singleton<GameplayScreen>
{
    private float time;
    public const float RealSecondsToInGameHours = 60;

    [NonSerialized]
    public float MeterHumanHappiness = 1.0f;
    [NonSerialized]
    public float MeterHumanHunger = 1.0f;
    [NonSerialized]
    public float MeterCatStamina = 1.0f;

    public const float MeterDepleteRateHumanHappiness = 0.001f;
    public const float MeterDepleteRateHumanHunger = 0.001f;
    public const float MeterDepleteRateCatStamina = 0.001f;

    public enum State
    {
        Gameplay,
        GameOver,
    }

    [NonSerialized]
    public State CurrentState = State.Gameplay;

    public void Update()
    {
        switch(CurrentState)
        {
            case State.Gameplay:
                {
                    time += Time.deltaTime;
                    if (time / RealSecondsToInGameHours >= 12.0f)
                    {
                        CurrentState = State.GameOver;
                        break;
                    }

                    MeterHumanHappiness -= Time.deltaTime * MeterDepleteRateHumanHappiness;
                    MeterHumanHappiness = Mathf.Clamp01(MeterHumanHappiness);

                    MeterHumanHunger -= Time.deltaTime * MeterDepleteRateHumanHunger;
                    MeterHumanHunger = Mathf.Clamp01(MeterHumanHunger);

                    MeterCatStamina -= Time.deltaTime * MeterDepleteRateCatStamina;
                    MeterCatStamina = Mathf.Clamp01(MeterCatStamina);

                    if (MeterHumanHappiness <= 0.0f || MeterHumanHunger <= 0.0f || MeterCatStamina <= 0.0f)
                    {
                        CurrentState = State.GameOver;
                        break;
                    }
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
