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
        Walking,
        Idle,
    }

    private State currentState = State.Idle;
    private float stateTimer = 0.0f;
    private float diretcion = -1;

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

        if (stateTimer > 2.0f)
        {
            switch (currentState)
            {
                case State.Idle:
                    currentState = State.Walking;
                    diretcion *= -1;
                    move.StartAcceleration(diretcion);
                    sprite.flipX = diretcion > 0;
                    animator.SetBool("walking", true);
                    break;
                case State.Walking:
                    currentState = State.Idle;
                    move.StartDeceleration();
                    animator.SetBool("walking", false);
                    break;
            }
            stateTimer = 0.0f;
        }

        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Walking:
                move.UpdateMovement(diretcion);
                break;
        }
    }
}
