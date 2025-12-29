using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;

    [Header("Difficulty Settings")]
    [Range(0f, 1f)]
    public float bombChance = 0.05f; // Initial bomb chance (5%)

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    [Header("Physics Settings")]
    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            // Pick random fruit
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            // Check bomb chance
            if (Random.value < bombChance)
            {
                prefab = bombPrefab;
            }

            // Calculate random position within the collider bounds
            Vector3 position = new Vector3
            {
                x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            };

            // Random rotation
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            // Create object
            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifetime);

            // Apply Force
            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            // Wait before next spawn
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    // 👇👇👇 THIS IS THE NEW PART 👇👇👇
    // Called by GameManager to increase difficulty
    public void IncreaseDifficulty()
    {
        // 1. Increase Bomb Chance (Max 50%)
        if (bombChance < 0.5f)
        {
            bombChance += 0.05f;
        }

        // 2. Speed up spawning (Decrease delays)
        // Clamp minDelay to 0.1s so it doesn't get too crazy
        if (minSpawnDelay > 0.1f)
        {
            minSpawnDelay -= 0.05f;
            maxSpawnDelay -= 0.05f;
        }

        Debug.Log($"Difficulty Increased! New BombChance: {bombChance}, Delays: {minSpawnDelay}-{maxSpawnDelay}");
    }
}