using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] CharacterController controller;
    public bool isPlayerInDialogue;
    [Header("x and z movement")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    [Header("jump")]
    [SerializeField] private float jumpHeight;
    public float gravity = -9.81f;
    private bool isGrounded;
    public Vector3 velocity;
    [Header("Animator")]
    [SerializeField] private Animator animator;
    [Header("Camera")]
    public GameObject TPPCamera;

    private void Update()
    {
        // Lock cursor at the center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Check if player is on ground
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = -0.5f;
        }

        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        // x and z axis movement
        if (!isPlayerInDialogue)
        {
            TPPCamera.SetActive(true);

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

                // Start walking
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

        }
        else
        {
            TPPCamera.SetActive(false);
        }
    }
}
