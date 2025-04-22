using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!controller.enabled) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(moveX, 0f, moveZ);
        float moveAmount = move.magnitude;

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && moveAmount > 0.1f;
        bool isWalking = !isRunning && moveAmount > 0.1f;
        bool isJumping = Input.GetKeyDown(KeyCode.Space); //&& controller.isGrounded;

        animator.SetBool("Walk", isWalking);
        animator.SetBool("Run", isRunning);
        animator.SetBool("Jump", isJumping);
    }
}   