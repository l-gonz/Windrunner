using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float distance = 5f; // Distance from the player
    public float height = 2f; // Height above the player
    public float rotationSpeed = .75f; // Speed of camera rotation

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

        // Collision detection
        // if (Physics.Linecast(player.position, targetPosition, out RaycastHit hit))
        // {
        //     targetPosition = hit.point + hit.normal * 0.2f; // Adjust to avoid clipping
        // }

        // Option 1: Update camera position and rotation
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * height / 2);

        // Option 2: More smooth??
        // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}
