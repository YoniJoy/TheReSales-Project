using UnityEngine;

public class ChairInteractionController : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float rotationSpeed = 100f; // Speed of rotation based on mouse movement
    public float zoomSpeed = 2f;       // Speed of zooming in/out with the scroll wheel
    public float minZoomDistance = 1f; // Closest zoom distance allowed
    public float maxZoomDistance = 5f; // Furthest zoom distance allowed

    [Header("Camera and Target References")]
    public Camera mainCam;             // Reference to the main camera
    public Transform cameraTarget;     // Target point used for zoom focus, usually near the chair

    // Internal state tracking
    private Vector3 lastMousePos;          // Last mouse position used to calculate dragging delta
    private bool isRightDragging = false;  // Tracks if the user is currently right-dragging (rotating)
    private float currentZoomDistance;     // Current zoom distance from the target

    // Initial states for reset functionality
    private Vector3 initialChairPosition;     // Original chair position at start
    private Quaternion initialChairRotation;  // Original chair rotation at start
    private float initialZoomDistance;        // Original zoom distance at start
    private Vector3 initialCameraPosition;    // Original camera position at start

    void Start()
    {
        // Assign main camera if not already set
        if (mainCam == null)
            mainCam = Camera.main;

        // Initialize zoom distance based on camera's distance from the target
        if (cameraTarget != null)
        {
            currentZoomDistance = Vector3.Distance(mainCam.transform.position, cameraTarget.position);
            initialZoomDistance = currentZoomDistance;
            initialCameraPosition = mainCam.transform.position;
        }

        // Store the chair's initial position and rotation for resetting
        initialChairPosition = transform.position;
        initialChairRotation = transform.rotation;
    }

    void Update()
    {
        HandleZoom();    // Allows zooming in/out using the mouse scroll wheel
        HandleRotation(); // Allows rotating the chair using right mouse drag
        HandleReset();   // Resets the chair and camera to their original states on key press
    }

    /// <summary>
    /// Handles chair rotation based on right mouse drag.
    /// Rotates around both the camera's up (Y) and right (X) axes.
    /// </summary>
    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightDragging = true;
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightDragging = false;
        }

        if (isRightDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePos;

            float rotationY = delta.x * rotationSpeed * Time.deltaTime;
            float rotationX = -delta.y * rotationSpeed * Time.deltaTime;

            // Rotate around the world's Y-axis (horizontal)
            transform.Rotate(mainCam.transform.up, -rotationY, Space.World);

            // Rotate around the world's X-axis (vertical)
            transform.Rotate(mainCam.transform.right, rotationX, Space.World);

            lastMousePos = Input.mousePosition;
        }
    }

    /// <summary>
    /// Handles zooming in and out using the mouse scroll wheel.
    /// Moves the camera closer or further from the target.
    /// </summary>
    void HandleZoom()
    {
        if (cameraTarget == null || mainCam == null)
            return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            // Adjust zoom based on scroll input
            currentZoomDistance -= scroll * zoomSpeed;
            currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);

            // Move camera along the direction vector relative to the target
            Vector3 dir = (mainCam.transform.position - cameraTarget.position).normalized;
            mainCam.transform.position = cameraTarget.position + dir * currentZoomDistance;
        }
    }

    /// <summary>
    /// Resets the chair's position and rotation,
    /// and restores the camera zoom to its initial state.
    /// Triggered by pressing the 'R' key.
    /// </summary>
    void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset chair transform
            transform.position = initialChairPosition;
            transform.rotation = initialChairRotation;

            // Reset zoom (camera position)
            currentZoomDistance = initialZoomDistance;
            Vector3 dir = (mainCam.transform.position - cameraTarget.position).normalized;
            mainCam.transform.position = cameraTarget.position + dir * currentZoomDistance;
        }
    }
}

