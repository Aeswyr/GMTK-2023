using UnityEngine;
using UnityEngine.Events;

public class Knockable : MonoBehaviour
{
    [Tooltip("This will be normalized when used")]
    [SerializeField] private Vector2 _knockDirection = Vector2.right;
    public Vector2 KnockDirectionNormalized
    {
        get => _knockDirection.normalized;
        set => _knockDirection = value.normalized;
    }

    [field: SerializeField] public float KnockForce = 100f;

    private struct KnockParams
    {
        public Vector2 knockDirection;
        public float knockForce;
    }

    [SerializeField] private UnityEvent<KnockParams> onKnock;

    public void Knock()
    {
        onKnock.Invoke(new KnockParams()
        {
            knockDirection = KnockDirectionNormalized,
            knockForce = KnockForce,
        });
    }

    public void AddForce(Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(KnockForce * KnockDirectionNormalized);
    }
}
