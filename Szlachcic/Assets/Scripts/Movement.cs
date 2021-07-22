using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] CharacterController controller;
    [Header("x and z movement")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    [Header("jump")]
    [SerializeField] private float jumpHeight;
    private float gravity = -9.81f;
    private bool isGrounded;
    private Vector3 velocity;
    [Header("Animator")]
    [SerializeField] private Animator animator;

    private void Update()
    {
        // Check if player is on ground
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            velocity.y = -0.5f;
        }

        // x and z axis movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, 0, z).normalized;

        if (direction.magnitude > 0)
        {
            // Rotate player
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Move player to rotated direction
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            // Move player
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Start sprinting
                controller.Move(moveDirection.normalized * speed * 2 * Time.deltaTime);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            else
            {
                // Start walking
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            animator.SetBool("isJumping", true);
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
    }
}
