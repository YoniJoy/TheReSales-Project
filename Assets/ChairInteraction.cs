using UnityEngine;

public class ChairInteractionController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveSpeed = 0.01f;
    public float zoomSpeed = 2f;
    public float minZoomDistance = 1f;
    public float maxZoomDistance = 5f;

    public Camera mainCam;
    public Transform cameraTarget; // Empty object near chair, zoom & view focus

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

            // Only rotate around Y (horizontal), to avoid chaotic vertical rotation
            transform.Rotate(Vector3.up, -rotationY, Space.World);

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

            // Convert screen movement to world movement
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

    // === Camera View Switches ===

    public void SetTopView()
    {
        SetCameraView(new Vector3(0, 3, 0), Quaternion.Euler(90, 0, 0));
    }

    public void SetLeftView()
    {
        SetCameraView(new Vector3(-3, 1, 0), Quaternion.Euler(15, 90, 0));
    }

    public void SetRightView()
    {
        SetCameraView(new Vector3(3, 1, 0), Quaternion.Euler(15, -90, 0));
    }

    private void SetCameraView(Vector3 offset, Quaternion rotation)
    {
        if (cameraTarget == null || mainCam == null) return;

        mainCam.transform.position = cameraTarget.position + offset;
        mainCam.transform.rotation = rotation;
        currentZoomDistance = Vector3.Distance(mainCam.transform.position, cameraTarget.position);
    }
}
