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

    /*[Header("Interaction Settings")]
    [SerializeField] public BoxCollider interactionZone; // Reference to the interaction zone collider*/

    [SerializeField]private Rigidbody Edmon_RigidBody;
    [SerializeField]private Animation_Edmon animation_Edmon;
    [SerializeField]private float verticalRotation = 0f;
   [SerializeField] private bool Interacting;



    private void Start()
    {
        // Assign the Rigidbody & animator component at runtime
        Edmon_RigidBody = GetComponent<Rigidbody>();
        animation_Edmon = GetComponent<Animation_Edmon>();

        // Lock the cursor for mouse control
        Cursor.lockState = CursorLockMode.Locked;

        // If no camera is assigned, find and assign it
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Assume the main camera is the player camera
        }

        //If no animator controll asigend asign it
        if (animation_Edmon == null)
        {
            animation_Edmon = GetComponent<Animation_Edmon>();
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
        if(!Interacting)
        {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a movement vector based on camera orientation
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        movement = playerCamera.transform.TransformDirection(movement);
        movement.y = 0; // Prevent moving up/down

            if (movement.magnitude > 1)
                movement.Normalize(); // Normalize if moving diagonally

        // Determine speed based on sprinting
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Move the player
        Edmon_RigidBody.MovePosition(transform.position + movement * speed * Time.deltaTime);

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
