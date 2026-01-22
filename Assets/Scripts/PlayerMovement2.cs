using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement Settings")]
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1.0f;
    public float standingHeight = 2.0f;
    public float crouchSpeed = 2f;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchTransitionSpeed = 6f;

    [Header("Sprint Settings")]
    public float sprintSpeed = 8f;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Animation")]
    public Animator animator; // Animator na modelu

    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching = false;
    private float targetHeight;

    void Start()
    {
        targetHeight = standingHeight;
        controller.height = standingHeight;
    }

    void Update()
    {
        // Check if grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // prepreči "tonjenje" playerja
        }

        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // Crouch logic
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;
            targetHeight = isCrouching ? crouchHeight : standingHeight;
        }
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);

        // Determine current speed
        float currentSpeed = speed;
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else if (Input.GetKey(sprintKey))
        {
            currentSpeed = sprintSpeed;
        }

        // Move player
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Animation
        float moveAmount = new Vector3(x, 0, z).magnitude; // koliko se premikaš
        animator.SetFloat("Speed", moveAmount);
    }
}
