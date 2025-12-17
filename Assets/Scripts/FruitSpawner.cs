using UnityEngine;

/// <summary>
/// Spawns fruits at random positions with upward force.
/// Handles spawn timing and positioning in 3D space.
/// </summary>
public class FruitSpawner : MonoBehaviour
{
    [Header("Fruit Prefabs")]
    [Tooltip("Array of fruit prefabs to spawn randomly")]
    public GameObject[] fruitPrefabs;

    [Header("Spawn Settings")]
    [Tooltip("Time interval between spawns (in seconds)")]
    public float spawnInterval = 1.5f;
    
    [Tooltip("Minimum X position for spawning fruits")]
    public float minXPosition = -8f;
    
    [Tooltip("Maximum X position for spawning fruits")]
    public float maxXPosition = 8f;

    [Header("Launch Settings")]
    [Tooltip("Minimum upward force for launched fruits")]
    public float minLaunchForce = 10f;
    
    [Tooltip("Maximum upward force for launched fruits")]
    public float maxLaunchForce = 15f;

    private float spawnTimer;

    void Start()
    {
        // Validate fruit prefabs
        if (fruitPrefabs == null || fruitPrefabs.Length == 0)
        {
            Debug.LogError("No fruit prefabs assigned to FruitSpawner! Please assign at least one fruit prefab in the Inspector.");
        }

        // Initialize timer
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        // Count down the timer
        spawnTimer -= Time.deltaTime;

        // When timer reaches zero, spawn a fruit and reset
        if (spawnTimer <= 0f)
        {
            SpawnFruit();
            spawnTimer = spawnInterval;
        }
    }

    /// <summary>
    /// Spawns a random fruit at a random X position.
    /// The fruit is spawned at the spawner's Y and Z position.
    /// </summary>
    void SpawnFruit()
    {
        if (fruitPrefabs == null || fruitPrefabs.Length == 0)
            return;

        // Choose a random fruit prefab
        int randomIndex = Random.Range(0, fruitPrefabs.Length);
        GameObject fruitPrefab = fruitPrefabs[randomIndex];

        // Calculate random spawn position
        float randomX = Random.Range(minXPosition, maxXPosition);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        // Instantiate the fruit
        GameObject spawnedFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);

        // Optional: Apply force from spawner instead of from Fruit.cs
        // This gives you more control over spawn behavior
        Fruit fruitScript = spawnedFruit.GetComponent<Fruit>();
        if (fruitScript != null)
        {
            // You can either let the Fruit.cs handle the launch in its Start()
            // Or you can apply force here for more control:
            
            // Uncomment below if you want spawner to control force instead of Fruit.cs
            /*
            float randomForce = Random.Range(minLaunchForce, maxLaunchForce);
            Vector3 force = Vector3.up * randomForce;
            fruitScript.LaunchWithCustomForce(force);
            */
        }

        Debug.Log($"Spawned {fruitPrefab.name} at position {spawnPosition}");
    }

    /// <summary>
    /// Manually spawn a fruit (useful for testing or special events)
    /// </summary>
    public void SpawnFruitManually()
    {
        SpawnFruit();
    }

    /// <summary>
    /// Adjust spawn rate during gameplay
    /// </summary>
    /// <param name="newInterval">New spawn interval in seconds</param>
    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = Mathf.Max(0.1f, newInterval); // Minimum 0.1 seconds
        Debug.Log($"Spawn interval changed to {spawnInterval} seconds");
    }

    // Visualize spawn range in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        
        Vector3 leftPoint = new Vector3(minXPosition, transform.position.y, transform.position.z);
        Vector3 rightPoint = new Vector3(maxXPosition, transform.position.y, transform.position.z);
        
        Gizmos.DrawLine(leftPoint, rightPoint);
        Gizmos.DrawWireSphere(leftPoint, 0.3f);
        Gizmos.DrawWireSphere(rightPoint, 0.3f);
    }
}
