using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages game state, score tracking, and UI updates.
/// Implements Singleton pattern for easy access from other scripts.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    [Header("Score Settings")]
    [Tooltip("Current player score")]
    public int score = 0;
    
    [Tooltip("UI Text component to display the score")]
    public Text scoreText;

    [Header("Game Settings")]
    [Tooltip("Whether the game is currently active")]
    public bool isGameActive = true;

    void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persist across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Initialize score display
        UpdateScoreUI();
    }

    /// <summary>
    /// Increases the player's score by the specified amount.
    /// </summary>
    /// <param name="amount">Amount to add to score</param>
    public void IncreaseScore(int amount)
    {
        if (!isGameActive)
            return;

        score += amount;
        UpdateScoreUI();
        
        Debug.Log($"Score increased by {amount}. Total score: {score}");
    }

    /// <summary>
    /// Decreases the player's score (e.g., for hitting bombs or missing fruits).
    /// </summary>
    /// <param name="amount">Amount to subtract from score</param>
    public void DecreaseScore(int amount)
    {
        if (!isGameActive)
            return;

        score = Mathf.Max(0, score - amount); // Don't go below zero
        UpdateScoreUI();
        
        Debug.Log($"Score decreased by {amount}. Total score: {score}");
    }

    /// <summary>
    /// Updates the score UI text display.
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
        else
        {
            Debug.LogWarning("Score Text UI not assigned in GameManager! Please assign a UI Text component in the Inspector.");
        }
    }

    /// <summary>
    /// Resets the score to zero.
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
        Debug.Log("Score reset to 0");
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        isGameActive = true;
        ResetScore();
        Debug.Log("Game Started!");
    }

    /// <summary>
    /// Ends the game.
    /// </summary>
    public void EndGame()
    {
        isGameActive = false;
        Debug.Log($"Game Over! Final Score: {score}");
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    /// <summary>
    /// Resumes the game.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }
}
