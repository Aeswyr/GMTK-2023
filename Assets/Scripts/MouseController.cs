using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private MovementHandler move;
    [SerializeField] private GroundedCheck ground;
    [SerializeField] private SpriteRenderer sprite;

    private bool grounded; // is the player touching the ground this physics frame
    private float direction;

    public void Init(float direction)
    {
        this.direction = direction;
        move.StartAcceleration(direction);
        sprite.flipX = direction > 0;
    }

    void FixedUpdate()
    {
        grounded = ground.CheckGrounded();
        //animator.SetBool("grounded", grounded && rbody.velocity.y < 1);
        move.UpdateMovement(direction);
    }
    public void OnKnock()
    {
        Destroy(this.gameObject);
    }
}
