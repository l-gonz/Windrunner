using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float jumpForce = 5f; // Jumping force
    private Vector3 movementInput; // Stores input direction
    private Rigidbody rb; // Player Rigidbody

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Apply movement
        Vector3 move = movementInput * speed;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }

    public void OnMove(InputValue value)
    {
        Vector2 movement = value.Get<Vector2>();
        Debug.Log($"Move input: {movement}");

        // Process movement input here.
        movementInput = new Vector3(movement.x, 0, movement.y);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Jump action triggered!");
            // Process jump here.
            if (IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private bool IsGrounded()
    {
        // Check if player is on the ground
        return true; //Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
