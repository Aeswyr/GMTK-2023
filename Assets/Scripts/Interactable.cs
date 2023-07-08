using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = LayerMaskHelper.Everything;
    [SerializeField] private UnityEvent action;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!LayerMaskHelper.GameObjectIsOnLayerMask(other.gameObject, layerMask)) return;
        if (action != null)
            action.Invoke();
        else
            Debug.LogWarning("No action bound for interactable!");
    }
}
