using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class IntervalEventInvoker : MonoBehaviour
{
    [SerializeField] private UnityEvent onInvoke;
    [SerializeField] private float minIntervalSeconds = 1f;
    [SerializeField] private float maxIntervalSeconds = 2f;

    private void Awake()
    {
        StartCoroutine(IntervalInvoke());
    }

    private IEnumerator IntervalInvoke()
    {
        yield return new WaitForSeconds(Random.Range(minIntervalSeconds, maxIntervalSeconds));
        onInvoke.Invoke();
        StartCoroutine(IntervalInvoke());
    }



    private void OnValidate()
    {
        minIntervalSeconds = Mathf.Min(minIntervalSeconds, maxIntervalSeconds);
        maxIntervalSeconds = Mathf.Max(maxIntervalSeconds, minIntervalSeconds);
    }
}
