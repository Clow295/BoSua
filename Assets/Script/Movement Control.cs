using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;

    [Header("Movement Settings")]
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float jumpPower = 10f;
    public float gravity = 40f;

    [Header("Look Settings")]
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool canMove = true;

    private float verticalVelocity = 0f; // For vertical movement (jumping/gravity)

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        LockCursor();
    }

    void Update()
    {
        if (canMove)
        {
            HandleMovement();
            HandleMouseLook();
        }
    }

    // Locks the cursor to the center of the screen
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Handles player movement including walking, running, jumping, and gravity
    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Check if the player is running
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Get input and calculate movement direction (horizontal)
        float moveX = Input.GetAxis("Vertical");
        float moveY = Input.GetAxis("Horizontal");
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 horizontalMovement = forward * (moveX * currentSpeed) + right * (moveY * currentSpeed);

        // Handle vertical movement (jumping and gravity)
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f; // Small downward force to keep grounded
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpPower; // Apply jump force
            }
        }
        
        else
        {
            verticalVelocity -= gravity * Time.deltaTime; // Apply gravity
        }

        // Combine horizontal and vertical movement
        moveDirection = horizontalMovement + Vector3.up * verticalVelocity;

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);
    }

    // Handles the player's camera look rotation
    private void HandleMouseLook()
    {
        // Horizontal rotation (Y-axis)
        transform.Rotate(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Vertical rotation (X-axis)
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
