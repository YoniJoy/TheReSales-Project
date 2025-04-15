using UnityEngine;

public class ChairControls : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 0.1f;

    [Header("Rotation Settings")]
    public float rotateSpeed = 5f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 2f;
    public float minZoom = 1f;
    public float maxZoom = 10f;

    private Vector3 lastMousePos;
    private float currentZoom = 5f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        currentZoom = -mainCamera.transform.localPosition.z;

        // Safety check
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found! Tag your camera as MainCamera.");
            enabled = false;
        }
    }

    void Update()
    {
        // LEFT-CLICK + DRAG = MOVE
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            transform.Translate(
                -delta.x * moveSpeed * Time.deltaTime,
                -delta.y * moveSpeed * Time.deltaTime,
                0,
                Space.Self
            );
        }

        // RIGHT-CLICK + DRAG = ROTATE
        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            transform.Rotate(Vector3.up, delta.x * rotateSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, -delta.y * rotateSpeed * Time.deltaTime, Space.World);
        }

        // SCROLL WHEEL = ZOOM IN / OUT
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f && mainCamera != null)
        {
            currentZoom -= scroll * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            mainCamera.transform.localPosition = new Vector3(0, 0, -currentZoom);
        }

        lastMousePos = Input.mousePosition;
    }
}