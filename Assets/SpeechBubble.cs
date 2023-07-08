using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] public TMP_Text text;

    public void Initialize(String speechString)
    {
        text.text = speechString;
    }
}
