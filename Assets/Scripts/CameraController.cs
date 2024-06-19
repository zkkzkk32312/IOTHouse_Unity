using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float rotationSpeed = .2f;
    public float panningSpeed = .5f;

    private Vector3 lastMousePosition;

    void Update()
    {
        // Camera Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime);

        // Update last mouse position when any mouse button is initially pressed
        if (Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(1) ||
            Input.GetMouseButtonDown(2)) // Left or right mouse button pressed
        {
            lastMousePosition = Input.mousePosition;
        }

        // Camera Rotation
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) // Left or right mouse button held or mouse moved
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            float rotationX = deltaMousePosition.y * rotationSpeed;
            float rotationY = -deltaMousePosition.x * rotationSpeed;

            // Calculate rotation only around Y and X axes
            Quaternion cameraTargetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + rotationX,
                                                                transform.rotation.eulerAngles.y + rotationY,
                                                                0f);
            transform.rotation = cameraTargetRotation;

            lastMousePosition = Input.mousePosition;
        }

        // Camera Panning
        if (Input.GetMouseButton(2)) // Middle mouse button held
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            Vector3 panDirection = new Vector3(deltaMousePosition.x, deltaMousePosition.y, 0f) * panningSpeed * Time.deltaTime;
            transform.Translate(-panDirection, Space.Self); // Move camera parallel to its own plane

            lastMousePosition = Input.mousePosition;
        }
    }
}
