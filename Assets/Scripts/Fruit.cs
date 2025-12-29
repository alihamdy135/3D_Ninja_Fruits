using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceEffect;

    public int points = 1;
    private bool isSliced = false; // Flag to track if fruit is sliced

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    // NEW: Check if fruit falls below the screen
    private void Update()
    {
        // If fruit falls below Y = -5 and hasn't been sliced yet
        if (transform.position.y < -20f)
        {
            if (!isSliced)
            {
                // Deduct a life because player missed the fruit
                GameManager.Instance.LoseLife();
            }

            // Destroy the object to clean up memory
            Destroy(gameObject);
        }
    }

    public void Slice(Vector3 direction, Vector3 position, float force)
    {
        if (isSliced) return; // Prevent double slicing

        isSliced = true;
        GameManager.Instance.IncreaseScore(points);

        fruitCollider.enabled = false;
        whole.SetActive(false);

        sliced.SetActive(true);
        juiceEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}