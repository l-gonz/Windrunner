using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float baseTurnSpeed = 5f;
    public float maxTurnSpeed = 15f;
    public float angleScalingFactor = .1f;

    private Vector3 movementInput;
    private Rigidbody playerRigidbody;
    private Animator animator;
    private Transform cameraTransform;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        cameraTransform = GetComponentInChildren<PlayerCamera>().transform;
    }

    private void FixedUpdate()
    {
        if (movementInput == Vector3.zero) return;

        var moveDirection = GetMoveDirection();
        var turnSpeed = CalculateTurnSpeed(moveDirection);

        // Rotate the character to face the movement direction
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 
            turnSpeed * Time.fixedDeltaTime);

        // Apply movement
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void OnMove(InputValue value)
    {
        Vector2 movement = value.Get<Vector2>();
        movementInput = new Vector3(movement.x, 0, movement.y);

        // Animations
        if (movementInput.magnitude > 0.1f)
            animator.SetTrigger("Run");
        else
            animator.SetTrigger("Stop");
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        if (!IsGrounded()) return;
        
        animator.SetTrigger("Jump");
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // TODO Check if player is on the ground
    private bool IsGrounded()
    {
        return true; //Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    // Calculate the movement direction
    private Vector3 GetMoveDirection()
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Flatten the directions to ignore the Y component
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * movementInput.z + cameraRight * movementInput.x;
    }

    // Scale the turn speed based on how big the angle to turn is
    private float CalculateTurnSpeed(Vector3 moveDirection)
    {
        float angle = Vector3.Angle(transform.forward, moveDirection);
        return Mathf.Lerp(baseTurnSpeed, maxTurnSpeed, angle * angleScalingFactor);
    }
}
