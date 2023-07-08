using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CollisionUnityEventBehaviour : MonoBehaviour
{
    public LayerMask mask;

    [SerializeField] private bool _ignoreFirstCollisionEnter = false;
    private bool _ignoredFirstCollisionEnter = false;

    public UnityEvent<Collision2D> onCollisionEnter;
    public UnityEvent<Collision2D> onCollisionExit;

    public Collider2D Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onCollisionEnter.GetPersistentEventCount() <= 0) return;
        if (!LayerMaskHelper.GameObjectIsOnLayerMask(collision.gameObject, mask)) return;
        if (_ignoreFirstCollisionEnter && !_ignoredFirstCollisionEnter)
        {
            _ignoredFirstCollisionEnter = true;
            return;
        }
        onCollisionEnter.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (onCollisionExit.GetPersistentEventCount() <= 0) return;
        if (!LayerMaskHelper.GameObjectIsOnLayerMask(collision.gameObject, mask)) return;
        onCollisionExit.Invoke(collision);
    }


}
