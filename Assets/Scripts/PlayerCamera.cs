using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float distance = 5f; // Distance from the player
    public float height = 2f; // Height above the player
    public float rotationSpeed = 5f; // Speed of camera rotation

    private Vector2 lookInput; // Mouse input
    private float currentRotationY; // Y-axis rotation

    void LateUpdate()
    {
        // Rotate camera with mouse input
        currentRotationY += lookInput.x * rotationSpeed;

        // Calculate new position and rotation
        Vector3 offset = new Vector3(0, height, -distance);
        Quaternion rotation = Quaternion.Euler(0, currentRotationY, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // Update camera position and look at the player
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * height / 2);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}
