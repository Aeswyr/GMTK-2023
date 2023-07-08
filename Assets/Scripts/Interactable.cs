using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (action != null)
            action.Invoke();
        else
            Debug.LogWarning("No action bound for interactable!");
    }
}
