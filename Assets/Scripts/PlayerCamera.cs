using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public float distance = 5f; // Distance from the player
    public float height = 2f; // Height above the player
    public float rotationSpeed = .75f; // Speed of camera rotation

    private Transform playerTransform;
    private Vector2 lookInput;
    private float currentRotationY;

    public void Start()
    {
        playerTransform = GetComponentInParent<PlayerController>().transform;
    }

    public void LateUpdate()
    {
        // Rotate camera with mouse input
        currentRotationY += lookInput.x * rotationSpeed;

        // Calculate new position and rotation
        Vector3 offset = new Vector3(0, height, -distance);
        Quaternion rotation = Quaternion.Euler(0, currentRotationY, 0);
        Vector3 targetPosition = playerTransform.position + rotation * offset;

        // TODO Collision detection
        // if (Physics.Linecast(playerTransform.position, targetPosition, out RaycastHit hit))
        // {
        //     targetPosition = hit.point + hit.normal * 0.2f; // Adjust to avoid clipping
        // }

        transform.position = targetPosition;
        transform.LookAt(playerTransform.position + Vector3.up * height / 2);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}
