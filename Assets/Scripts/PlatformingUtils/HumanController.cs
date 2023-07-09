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
        Sleeping,
    }

    private State currentState = State.Idle;
    private State CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            stateTimer = 0.0f;
            switch (currentState)
            {
                case State.Idle:
                    {
                        currentState = State.Idle;
                        move.StartDeceleration();
                        animator.SetBool("walking", false);
                        animator.SetBool("sleeping", false);
                    }
                    break;
                case State.WalkingRandomly:
                    {
                        move.StartAcceleration(diretcion);
                        sprite.flipX = diretcion > 0;
                        animator.SetBool("walking", true);
                        animator.SetBool("sleeping", false);
                    }
                    break;
                case State.WalkingToTarget:
                    {
                        diretcion = (targetPosition - transform.position).x < 0 ? -1 : 1;
                        move.StartAcceleration(diretcion);
                        sprite.flipX = diretcion > 0;
                        animator.SetBool("walking", true);
                        animator.SetBool("sleeping", false);
                    }
                    break;
                case State.Sleeping:
                    {
                        animator.SetBool("sleeping", true);
                    }
                    break;
            }
        }
    }
    private float stateTimer = 0.0f;
    private float diretcion = -1;
    private Vector3 targetPosition = Vector3.zero;

    private void Start()
    {
        CurrentState = State.Sleeping;
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
    }

    void FixedUpdate()
    {
        grounded = ground.CheckGrounded();
        animator.SetBool("grounded", grounded && rbody.velocity.y < 1);
        stateTimer += Time.fixedDeltaTime;

        switch (CurrentState)
        {
            case State.Idle:
                if (stateTimer > 2.0f)
                {
                    diretcion *= -1;
                    CurrentState = State.WalkingRandomly;
                }
                break;
            case State.WalkingRandomly:
                move.UpdateMovement(diretcion);
                if (stateTimer > 2.0f)
                    CurrentState = State.Idle;
                break;
            case State.WalkingToTarget:
                move.UpdateMovement(diretcion);
                if (Mathf.Abs((transform.position - targetPosition).x) < 0.5f)
                    CurrentState = State.Idle;
                break;
            case State.Sleeping:
                break;
        }
    }

    public void WalkToTarget(Vector3 position)
    {
        targetPosition = position;
        CurrentState = State.WalkingToTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MeowBox"))
            WalkToTarget(collision.transform.parent.position);
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            Destroy(collision.gameObject);
            GameplayScreen.Instance.MeterHumanHunger += 0.3f;
            Mathf.Clamp01(GameplayScreen.Instance.MeterHumanHunger);
        }
    }
}
