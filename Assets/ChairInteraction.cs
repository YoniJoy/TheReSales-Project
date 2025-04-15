using UnityEngine;

public class ChairInteractionController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveSpeed = 0.01f;
    public float zoomSpeed = 2f;
    public float minZoomDistance = 1f;
    public float maxZoomDistance = 5f;

    public Camera mainCam;
    public Transform cameraTarget; // Empty object near chair, zoom focus

    private Vector3 lastMousePos;
    private bool isLeftDragging = false;
    private bool isRightDragging = false;
    private float currentZoomDistance;

    void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;

        if (cameraTarget != null)
            currentZoomDistance = Vector3.Distance(mainCam.transform.position, cameraTarget.position);
    }

    void Update()
    {
        HandleZoom();
        HandleMovement();
        HandleRotation();
    }

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

            transform.Rotate(mainCam.transform.up, -rotationY, Space.World); // Horizontal rotation
            transform.Rotate(mainCam.transform.right, rotationX, Space.World); // Vertical rotation

            lastMousePos = Input.mousePosition;
        }
    }

    void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLeftDragging = true;
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isLeftDragging = false;
        }

        if (isLeftDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            Vector3 move = new Vector3(-delta.x, -delta.y, 0) * moveSpeed;

            // Convert screen move to world move
            transform.position += mainCam.transform.right * move.x + mainCam.transform.up * move.y;

            lastMousePos = Input.mousePosition;
        }
    }

    void HandleZoom()
    {
        if (cameraTarget == null || mainCam == null)
            return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentZoomDistance -= scroll * zoomSpeed;
            currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);

            Vector3 dir = (mainCam.transform.position - cameraTarget.position).normalized;
            mainCam.transform.position = cameraTarget.position + dir * currentZoomDistance;
        }
    }
}
