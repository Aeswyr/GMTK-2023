using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private JumpHandler jump;
    [SerializeField] private MovementHandler move;
    [SerializeField] private GroundedCheck ground;
    [SerializeField] private SpriteRenderer sprite;

    private bool grounded; // is the player touching the ground this physics frame

    
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        grounded = ground.CheckGrounded();

        animator.SetBool("grounded", grounded);

        if (InputHandler.Instance.dir.x != 0 && InputHandler.Instance.move.pressed) {
            animator.SetBool("walking", true);
            sprite.flipX = InputHandler.Instance.dir.x < 0;
            move.StartAcceleration(InputHandler.Instance.dir.x);
        } else if (InputHandler.Instance.move.down) {
            move.UpdateMovement(InputHandler.Instance.dir.x);
            sprite.flipX = InputHandler.Instance.dir.x < 0;
            animator.SetBool("walking", true);
        } else if (InputHandler.Instance.move.released) {
            move.StartDeceleration();
            animator.SetBool("walking", false);
        } else {
            animator.SetBool("walking", false);
        }

        if (InputHandler.Instance.jump.pressed) {
            jump.StartJump();
        }
        
    }
}
