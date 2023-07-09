using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLoop : MonoBehaviour
{
    [SerializeField] private GameObject human;
    private HumanController humanController;
    private bool eventsStarted = false;

    public enum EventType
    {
        LoseHappiness,
        GetHangry
    }

    void Start()
    {
        humanController = human.GetComponent<HumanController>();
    }

    void Update()
    {
        if (humanController.currentState != HumanController.State.Sleeping)
        {
            if (!eventsStarted)
            {
                eventsStarted = true;
                StartCoroutine(EventRoutine());
            }
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(EventRoutine());
    }

    private IEnumerator EventRoutine()
    {
        var eventType = (EventType)Random.Range(0, System.Enum.GetNames(typeof(EventType)).Length);
        var nextEventTime = Random.Range(30, 46); // Every 60s in game is 1s

        switch (eventType)
        {
            case EventType.LoseHappiness:
                GameplayScreen.Instance.MeterHumanHappiness -= Random.Range(0.05f, 0.1f);
                break;
            case EventType.GetHangry:
                GameplayScreen.Instance.MeterHumanHunger -= Random.Range(0.1f, 0.2f);
                break;
        }
        yield return new WaitForSeconds(nextEventTime);
        StartCoroutine(EventRoutine());
    }
}
