using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public Transform cameraTarget;

    public float mouseSensitivity = 220f;
    public float distance = 4.5f;
    public float height = 2f;
    public float positionSmoothTime = 0.12f;
    public float rotationSmoothSpeed = 12f;
    public float minPitch = -25f;
    public float maxPitch = 60f;

    private float yaw;
    private float pitch = 15f;
    private Vector3 currentVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yaw = player.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (player == null || cameraTarget == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 desiredPosition =
            cameraTarget.position
            - cameraRotation * Vector3.forward * distance
            + Vector3.up * (height - 1.5f);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            positionSmoothTime
        );

        Quaternion targetLookRotation =
            Quaternion.LookRotation(cameraTarget.position - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetLookRotation,
            rotationSmoothSpeed * Time.deltaTime
        );
    }

    public float GetYaw()
    {
        return yaw;
    }
}