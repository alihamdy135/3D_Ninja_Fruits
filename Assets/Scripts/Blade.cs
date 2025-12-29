using UnityEngine;

public class Blade : MonoBehaviour
{
    [Header("Blade Settings")]
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    [Header("Audio Settings")]
    public AudioClip sliceSound; // The 'Swoosh' sound clip
    private AudioSource audioSource;

    [Header("References")]
    private Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    // Public properties to access state
    public Vector3 direction { get; private set; }
    public bool slicing { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        // Handle Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            StartSlice();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlice();
        }
        else if (slicing)
        {
            ContinueSlice();
        }
    }

    private void StartSlice()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();

        // Play the Swoosh sound when slicing starts
        PlaySwipeSound();
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;

        // Only enable collider if moving fast enough
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }

    // Handles collisions with Fruits and Bombs
    private void OnTriggerEnter(Collider other)
    {
        // 1. Check for Fruit
        Fruit fruit = other.GetComponent<Fruit>();
        if (fruit != null)
        {
            // Send slice data to the fruit (Direction, Position, Force)
            fruit.Slice(direction, transform.position, sliceForce);
        }

        // 2. Check for Bomb
        if (other.CompareTag("Bomb"))
        {
            // Trigger Game Over logic in GameManager
            FindObjectOfType<GameManager>().OnBombHit();
        }
    }

    private void PlaySwipeSound()
    {
        // Check if sound clip and source are assigned
        if (audioSource != null && sliceSound != null)
        {
            // Randomize pitch slightly for variety
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(sliceSound);
        }
    }
}