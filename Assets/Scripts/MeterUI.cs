using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterUI : MonoBehaviour
{
    private RectTransform rectTransform;
    private GameplayScreen gameplayScreen;
    private float initSize = 0.0f;

    public enum MeterType
    {
        HumanHappiness,
        HumanHunger,
        CatStamina,
    }

    public MeterType Type;

    private void Awake()
    {
        gameplayScreen = GameplayScreen.Instance;
        rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Start()
    {
        initSize = rectTransform.sizeDelta.y;
    }

    private void Update()
    {
        var value = gameplayScreen.MeterCatStamina;
        switch(Type)
        {
            case MeterType.HumanHappiness:
                value = gameplayScreen.MeterHumanHappiness;
                break;
            case MeterType.HumanHunger:
                value = gameplayScreen.MeterHumanHunger;
                break;
            case MeterType.CatStamina:
                value = gameplayScreen.MeterCatStamina;
                break;
        }

        var size = rectTransform.sizeDelta;
        size.y = value * initSize;
        rectTransform.sizeDelta = size;
    }
}
