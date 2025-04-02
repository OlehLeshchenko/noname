using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float speed;

    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    private bool crouching = false;
    private bool sprinting = false;
    private bool lerpCrouch = false;
    private float crouchTimer = 1;
    private bool hasJumped = false; 

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float groundedGravityOffset = -2f;
    [SerializeField] private float jumpStaminaCost = 15f;


    // [SerializeField] private float baseStepSpeed = 0.5f;
    // [SerializeField]
    

    private PlayerHealth playerHealth; 

    public bool IsSprinting => sprinting;
    public bool IsCrouching => crouching;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
        if (controller == null)
        {
            Debug.LogError("CharacterController is missing from the Player!");
        }

        speed = walkSpeed;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            hasJumped = false;
        }

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            controller.height = crouching ? Mathf.Lerp(controller.height, 1, p) : Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

        if (!sprinting && isGrounded)
        {
            playerHealth.RestoreStamina(playerHealth.staminaRegenSpeed * Time.deltaTime);
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = transform.TransformDirection(new Vector3(input.x, 0, input.y).normalized);
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = groundedGravityOffset;

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded && !crouching && !hasJumped && playerHealth.HasStamina(jumpStaminaCost))
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            playerHealth.UseStamina(jumpStaminaCost); 
            hasJumped = true;
        }
    }

    public void Crouch()
    {
        if (sprinting) return;

        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        if (!crouching && playerHealth.HasStamina(playerHealth.staminaDrainSpeed * Time.deltaTime))
        {
            sprinting = !sprinting;

            if (sprinting)
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
        }
    }

    void FixedUpdate()
    {
        if (sprinting)
        {
            playerHealth.UseStamina(playerHealth.staminaDrainSpeed * Time.deltaTime);
            if (playerHealth.HasStamina(1) == false)
            {
                sprinting = false;
                speed = walkSpeed;
            }
        }
    }
}
