using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 4.0f;
    [SerializeField] private float mouseSensitivity = 2.0f;

    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float verticalRotationLimit = 85.0f;

    [SerializeField] private Rigidbody Edmon_RigidBody;
    [SerializeField] private Animation_Edmon animation_Edmon;
    [SerializeField] private float verticalRotation = 0f;
    [SerializeField] private bool Interacting;

    private void Start()
    {
        // Assign the Rigidbody & animator component at runtime
        Edmon_RigidBody = this.GetComponent<Rigidbody>();
        animation_Edmon = this.GetComponent<Animation_Edmon>();

        // Lock the cursor for mouse control
        Cursor.lockState = CursorLockMode.Locked;

        // If no camera is assigned, find and assign it
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Assume the main camera is the player camera
        }

        Interacting = false;
    }

    private void Update()
    {
        LookAround();
        HandleMovement();
    }

    private void LookAround()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust vertical rotation and clamp it
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        // Apply the rotations
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX); // Rotate the player object around the Y axis
    }

    private void HandleMovement()
    {
        if (!Interacting)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // Create a movement vector relative to the player's local space (not the camera's orientation)
            Vector3 movement = new Vector3(moveX, 0, moveZ);

            // Convert movement vector to world space but only apply horizontal plane (XZ movement)
            movement = transform.TransformDirection(movement);
            movement.y = 0; // Flatten to ensure movement remains on the XZ plane

            // Normalize the movement to avoid diagonal speed increases
            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }

            // Determine speed based on sprinting
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            // Move the player with normalized speed
            Edmon_RigidBody.MovePosition(transform.position + movement * speed * Time.deltaTime);

            // Check if the movement affects animation state
            CheckMovement(movement);
        }
    }

    private void CheckMovement(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animation_Edmon.ChangeCurrentStateAccessMethod("Running");
            }
            else
            {
                animation_Edmon.ChangeCurrentStateAccessMethod("Walking");
            }
        }
        else
        {
            animation_Edmon.ChangeCurrentStateAccessMethod("Idle");
        }
    }

    // ACCESS METHODS

    public void SetInteract(bool interacting)
    {
        Interacting = interacting;
    }

    public bool GetInteract()
    {
        return Interacting;
    }
}
