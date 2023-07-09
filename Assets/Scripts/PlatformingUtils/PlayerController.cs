using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private JumpHandler jump;
    [SerializeField] private MovementHandler move;
    [SerializeField] private GroundedCheck ground;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private StaminaHandler stamina;

    [SerializeField] private GameObject interactBox;
    [SerializeField] private GameObject pushBox;
    [SerializeField] private GameObject meowBox;

    private bool grounded; // is the player touching the ground this physics frame
    private bool actionable = true;

    void Start()
    {

    }


    void FixedUpdate()
    {
        grounded = ground.CheckGrounded();

        animator.SetBool("grounded", grounded && rbody.velocity.y < 1);

        if (actionable)
            ParseInputs();

        if (move.IsMoving)
        {
            stamina.DecreaseByMovementCost();
        }
    }

    private void ParseInputs()
    {
        if (actionable && InputHandler.Instance.dir.x != 0 && InputHandler.Instance.move.pressed)
        {
            animator.SetBool("walking", true);
            sprite.flipX = InputHandler.Instance.dir.x < 0;
            move.StartAcceleration(InputHandler.Instance.dir.x);
        }
        else if (actionable && InputHandler.Instance.move.down)
        {
            move.UpdateMovement(InputHandler.Instance.dir.x);
            sprite.flipX = InputHandler.Instance.dir.x < 0;
            animator.SetBool("walking", true);
        }
        else if (actionable && InputHandler.Instance.move.released)
        {
            move.StartDeceleration();
            animator.SetBool("walking", false);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if (actionable && grounded && InputHandler.Instance.jump.pressed)
        {
            jump.StartJump();
            animator.SetBool("walking", false);
            animator.SetBool("grounded", false);
            animator.SetTrigger("jump");
            stamina.DecreaseByJumpCost();
        }

        if (grounded && InputHandler.Instance.primary.pressed)
        {
            StartAction();
            animator.SetBool("walking", false);
            animator.SetTrigger("push");
            
        }

        if (grounded && InputHandler.Instance.secondary.pressed)
        {
            StartAction();
            animator.SetBool("walking", false);
            animator.SetTrigger("meow");
            interactBox.SetActive(true);
            meowBox.SetActive(true);
            interactBox.transform.localPosition = Vector3.zero;
            stamina.DecreaseByMeowCost();
        }
    }

    private void TriggerPushbox() {
        Vector3 facing = new Vector3(sprite.flipX ? -0.4f : 0.4f, 0, 0);
        pushBox.transform.localPosition = facing;
        pushBox.SetActive(true);
    }

    private void StartAction()
    {
        actionable = false;
        move.StartDeceleration();
    }

    private void EndAction()
    {
        actionable = true;
        interactBox.SetActive(false);
        interactBox.transform.localPosition = 100 * Vector3.up;
        meowBox.SetActive(false);
        pushBox.SetActive(false);
    }
}
