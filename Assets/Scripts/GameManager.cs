using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    [SerializeField] private Blade blade;
    [SerializeField] private Spawner spawner; // Make sure this is linked in Inspector
    [SerializeField] private Text scoreText;
    [SerializeField] private Image fadeImage;

    [Header("Lives System")]
    [SerializeField] private Image[] livesImages; // Drag the 3 Heart Images here
    private int lives;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource musicSource; // For Background Music ONLY

    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        Time.timeScale = 1f;

        // Reset Lives
        lives = 3;
        UpdateLivesUI();

        // Start Music
        if (musicSource != null)
        {
            musicSource.Play();
        }

        ClearScene();

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach (Fruit fruit in fruits) Destroy(fruit.gameObject);

        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs) Destroy(bomb.gameObject);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        // 📈 DIFFICULTY RAMP: Every 10 points
        if (score % 10 == 0)
        {
            // Tell Spawner to make it harder
            if (spawner != null)
            {
                spawner.IncreaseDifficulty();
            }
        }

        // Save Highscore
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }
    }

    public void LoseLife()
    {
        lives--;
        UpdateLivesUI();

        // Game Over if lives run out
        if (lives <= 0)
        {
            Explode();
        }
    }

    private void UpdateLivesUI()
    {
        for (int i = 0; i < livesImages.Length; i++)
        {
            // Show or hide hearts based on current lives
            livesImages[i].enabled = (i < lives);
        }
    }

    // Called by Blade when hitting a bomb
    public void OnBombHit()
    {
        // Stop Music immediately
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        Explode();
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        // Ensure music is stopped
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        // Fade out
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        elapsed = 0f;

        // Fade in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}