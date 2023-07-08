using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private MovementHandler move;
    [SerializeField] private GroundedCheck ground;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject interactBox;

    private bool grounded; // is the player touching the ground this physics frame
    private bool actionable = true;

    private enum State
    {
        Idle,
        WalkingRandomly,
        WalkingToTarget,
    }

    private State currentState = State.Idle;
    private float stateTimer = 0.0f;
    private float diretcion = -1;
    private Vector3 targetPosition = Vector3.zero;

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
    }

    void FixedUpdate()
    {
        grounded = ground.CheckGrounded();
        animator.SetBool("grounded", grounded && rbody.velocity.y < 1);
        stateTimer += Time.fixedDeltaTime;

        switch (currentState)
        {
            case State.Idle:
                if (stateTimer > 2.0f)
                {
                    stateTimer = 0.0f;
                    currentState = State.WalkingRandomly;
                    diretcion *= -1;
                    move.StartAcceleration(diretcion);
                    sprite.flipX = diretcion > 0;
                    animator.SetBool("walking", true);
                }
                break;
            case State.WalkingRandomly:
                move.UpdateMovement(diretcion);
                if (stateTimer > 2.0f)
                {
                    stateTimer = 0.0f;
                    currentState = State.Idle;
                    move.StartDeceleration();
                    animator.SetBool("walking", false);
                }
                break;
            case State.WalkingToTarget:
                move.UpdateMovement(diretcion);
                if (Mathf.Abs((transform.position - targetPosition).x) < 0.5f)
                {
                    stateTimer = 0.0f;
                    currentState = State.Idle;
                    move.StartDeceleration();
                    animator.SetBool("walking", false);
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MeowBox"))
        {
            currentState = State.WalkingToTarget;
            targetPosition = collision.transform.parent.position;
            diretcion = (targetPosition - transform.position).x < 0 ? -1 : 1;
            move.StartAcceleration(diretcion);
            sprite.flipX = diretcion > 0;
            animator.SetBool("walking", true);
        }
    }
}
