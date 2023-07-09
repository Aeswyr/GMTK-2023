using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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
    public const float MeterDepleteRateHumanHunger = 0.01f;

    [SerializeField] private GameObject prefabMouse;
    private int spawnedMouseInHour = -1;

    [SerializeField] private UnityEvent onMouseSpawn;

    public enum State
    {
        Gameplay,
        GameOver,
    }

    private State currentState = State.Gameplay;
    public State CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            if (currentState == State.GameOver)
            {
                ScreenManager.Instance.LoadScreen("GameOver");
            }
        }
    }

    public void Update()
    {
        switch (CurrentState)
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

                    if (MeterHumanHappiness <= 0.0f || MeterHumanHunger <= 0.0f || MeterCatStamina <= 0.0f)
                    {
                        CurrentState = State.GameOver;
                        break;
                    }

                    int hour = Mathf.FloorToInt(time / RealSecondsToInGameHours);
                    if (hour != 0 && spawnedMouseInHour != hour)
                    {
                        spawnedMouseInHour = hour;
                        Vector3 pos = new Vector3(-15, 0, 0);
                        float direction = 1;
                        if (hour % 2 == 0)
                        {
                            pos = new Vector3(15, 0, 0);
                            direction = -1;
                        }
                        var mouse = Instantiate(prefabMouse, pos, Quaternion.identity).GetComponent<MouseController>();
                        onMouseSpawn.Invoke();
                        mouse.Init(direction);
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
