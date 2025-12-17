using UnityEngine;

/// <summary>
/// Handles fruit physics, launch mechanics, and destruction when sliced.
/// Uses 3D Rigidbody for gravity and force application.
/// </summary>
public class Fruit : MonoBehaviour
{
    [Header("Launch Settings")]
    [Tooltip("Minimum upward force applied when spawned")]
    public float minLaunchForce = 8f;
    
    [Tooltip("Maximum upward force applied when spawned")]
    public float maxLaunchForce = 12f;

    [Header("Rotation Settings")]
    [Tooltip("Minimum random torque applied to fruit")]
    public float minTorque = -50f;
    
    [Tooltip("Maximum random torque applied to fruit")]
    public float maxTorque = 50f;

    [Header("Destruction Settings")]
    [Tooltip("Optional particle effect to spawn when fruit is sliced")]
    public GameObject sliceEffect;
    
    [Tooltip("Y position below which the fruit is automatically destroyed")]
    public float destroyYPosition = -10f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            Debug.LogError($"Rigidbody component missing on {gameObject.name}! Please add a Rigidbody (UseGravity=true)");
        }
    }

    void Start()
    {
        // Launch the fruit upward when it spawns
        LaunchFruit();
    }

    void Update()
    {
        // Destroy fruit if it falls too far down (off-screen)
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Launches the fruit upward with random force and applies random rotation.
    /// Uses ForceMode.Impulse for instant force application (like throwing).
    /// </summary>
    public void LaunchFruit()
    {
        if (rb == null) return;

        // Apply random upward force (Y-axis)
        float randomForce = Random.Range(minLaunchForce, maxLaunchForce);
        Vector3 launchDirection = Vector3.up; // Straight up in 3D
        
        rb.AddForce(launchDirection * randomForce, ForceMode.Impulse);

        // Apply random torque for rotation on all axes
        Vector3 randomTorque = new Vector3(
            Random.Range(minTorque, maxTorque),
            Random.Range(minTorque, maxTorque),
            Random.Range(minTorque, maxTorque)
        );
        
        rb.AddTorque(randomTorque);

        Debug.Log($"{gameObject.name} launched with force: {randomForce}");
    }

    /// <summary>
    /// Called when the blade hits this fruit.
    /// Spawns particle effects and destroys the fruit.
    /// </summary>
    public void Slice()
    {
        // Spawn particle effect at fruit position
        if (sliceEffect != null)
        {
            Instantiate(sliceEffect, transform.position, Quaternion.identity);
        }

        // Increase score
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IncreaseScore(1);
        }

        // Destroy the fruit
        Destroy(gameObject);
    }

    /// <summary>
    /// Alternative method: Apply force after instantiation (called by spawner).
    /// Useful if you want the spawner to control the launch force.
    /// </summary>
    /// <param name="force">Custom force to apply</param>
    public void LaunchWithCustomForce(Vector3 force)
    {
        if (rb != null)
        {
            rb.AddForce(force, ForceMode.Impulse);
            
            // Still add random torque
            Vector3 randomTorque = new Vector3(
                Random.Range(minTorque, maxTorque),
                Random.Range(minTorque, maxTorque),
                Random.Range(minTorque, maxTorque)
            );
            
            rb.AddTorque(randomTorque);
        }
    }
}
