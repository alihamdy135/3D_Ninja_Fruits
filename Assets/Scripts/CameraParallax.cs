using UnityEngine;

public class CameraParallax : MonoBehaviour
{
    [Header("Settings")]
    public float sensitivity = 2f; // Strength of the camera movement effect
    public float smoothing = 5f;   // Smoothing factor for the movement (higher is faster)

    private Quaternion originalRotation;

    void Start()
    {
        // Store the initial rotation of the camera to use as a reference
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // Calculate mouse position relative to the center of the screen
        // resulting values range from -0.5 to 0.5
        float x = (Input.mousePosition.x / Screen.width) - 0.5f;
        float y = (Input.mousePosition.y / Screen.height) - 0.5f;

        // Calculate the target rotation based on mouse input offset
        // Note: We use -y to invert the vertical axis (Mouse Up -> Look Up)
        Quaternion targetRotation = originalRotation * Quaternion.Euler(-y * sensitivity, x * sensitivity, 0);

        // Smoothly interpolate (Lerp) from current rotation to the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothing);
    }
}