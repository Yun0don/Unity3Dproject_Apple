using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Life UI")]
    public GameObject[] filledHearts;
    private int lifeCount;

    [Header("Score UI")]
    public TMP_Text scoreText;
    private int score;

    [Header("Timer UI")]
    public TMP_Text timerText;
    private float elapsedTime = 0f;
    private bool isTimerRunning = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ResetLife();
        ResetScore();
        ResetTimer();
        UpdateScoreUI();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerUI();
        isTimerRunning = true;
    }

    public void DecreaseLife()
    {
        if (lifeCount <= 0)
        {
            Debug.LogWarning("[UIManager] 남은 생명이 없습니다.");
            return;
        }

        lifeCount--;
        filledHearts[lifeCount].SetActive(false);
        Debug.Log($"[UIManager]  남은 생명: {lifeCount}");
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        Debug.Log($"[UIManager] 점수 +{amount} → 총점: {score}");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score : {score}";
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void ResetLife()
    {
        lifeCount = filledHearts.Length;
        foreach (GameObject heart in filledHearts)
        {
            heart.SetActive(true);
        }
    }
}
