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

    private void StartAction() {
        actionable = false;
        move.StartDeceleration();
    }

    private void EndAction() {
        actionable = true;
        interactBox.SetActive(false);
        interactBox.transform.localPosition = 100 * Vector3.up;
    }

    void FixedUpdate()
    {
        grounded = ground.CheckGrounded();

        animator.SetBool("grounded", grounded && rbody.velocity.y < 1);
    }
}
