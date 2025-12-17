using UnityEngine;

/// <summary>
/// Controls the blade movement based on mouse position in 3D space.
/// Converts 2D screen position to 3D world position with fixed Z-distance from camera.
/// </summary>
public class Blade : MonoBehaviour
{
    [Header("Blade Settings")]
    [Tooltip("Distance from camera where the blade will be positioned (Z-axis)")]
    public float bladeDistanceFromCamera = 10f;
    
    [Tooltip("Speed at which the blade follows the mouse")]
    public float followSpeed = 20f;

    private Camera mainCamera;
    private Rigidbody rb;

    void Start()
    {
        // Get references
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();

        // Validate components
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found! Make sure your camera is tagged as 'MainCamera'");
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody component missing on Blade! Please add a Rigidbody (IsKinematic=true, UseGravity=false)");
        }
    }

    void Update()
    {
        // Convert mouse position from screen space to world space
        Vector3 mousePosition = ConvertMouseToWorldPosition();

        // Smoothly move blade to mouse position
        transform.position = Vector3.Lerp(transform.position, mousePosition, followSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Converts the 2D mouse position from screen space to 3D world space.
    /// CRUCIAL: We set a fixed Z-distance so the blade exists in 3D space and can intersect with fruits.
    /// </summary>
    /// <returns>World position of the mouse in 3D space</returns>
    private Vector3 ConvertMouseToWorldPosition()
    {
        // Get mouse position in screen coordinates (2D)
        Vector3 mouseScreenPosition = Input.mousePosition;

        // IMPORTANT: Set the Z-distance from the camera
        // This is the key difference from 2D - we need to specify depth!
        mouseScreenPosition.z = bladeDistanceFromCamera;

        // Convert to world position using the camera
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        return worldPosition;
    }

    /// <summary>
    /// Detects collision with fruits using 3D trigger collision.
    /// This uses OnTriggerEnter (3D) instead of OnTriggerEnter2D.
    /// </summary>
    /// <param name="other">The collider that entered the trigger</param>
    void OnTriggerEnter(Collider other)
    {
        // Check if the object we hit is a fruit
        Fruit fruit = other.GetComponent<Fruit>();
        
        if (fruit != null)
        {
            // Tell the fruit it has been sliced
            fruit.Slice();
            
            Debug.Log($"Sliced: {other.gameObject.name}");
        }
    }
}
