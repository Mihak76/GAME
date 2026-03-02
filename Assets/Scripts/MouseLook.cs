using UnityEngine;

public class SmoothMouseLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    public float smoothTime = 0.05f; // manj = hitreje, več = mehko

    [HideInInspector] public bool allowLook = true;

    private float xRotation = 0f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!allowLook) return;

        // Input miške
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Vector2 targetMouseDelta = new Vector2(mouseX, mouseY);

        // Smooth rotacija
        currentMouseDelta = Vector2.SmoothDamp(
            currentMouseDelta,
            targetMouseDelta,
            ref currentMouseDeltaVelocity,
            smoothTime
        );

        // Pitch (gor/dol) za kamero
        xRotation -= currentMouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Yaw (levo/desno) za player body
        if (playerBody != null)
            playerBody.Rotate(Vector3.up * currentMouseDelta.x);
    }
}