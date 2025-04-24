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

    [Header("Panel UI")] 
    public Button pauseButton;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    
    [Header("Pause UI")] 
    public Button continueButton;
    public Button restartButtonP;
    public Button mainButtonP;
    
    [Header("GameOver UI")] 
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverTimerText;
    public Button restartButtonG;
    public Button mainButtonG;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        ResetPanel();
    }

    private void Start()
    {
        ResetLife();
        ResetScore();
        ResetTimer();
        UpdateScoreUI();
        
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(ShowPausePanel);
        }
        if (restartButtonP != null)
            restartButtonP.onClick.AddListener(() =>
            {
                // 패널 닫고, 씬 리스타트
                ClosePausePanel();
                SceneChanger sc = FindObjectOfType<SceneChanger>();
                sc?.RestartLevel();
            });

        // GameOver 패널의 '다시하기'
        if (restartButtonG != null)
            restartButtonG.onClick.AddListener(() =>
            {
                gameOverPanel.SetActive(false);
                SceneChanger sc = FindObjectOfType<SceneChanger>();
                sc?.RestartLevel();
            });
        
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel == null) return;

            if (pausePanel.activeSelf)
                ClosePausePanel();
            else
                ShowPausePanel();
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

    public void ResetPanel()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
    public void ShowPausePanel()
    {
        if (pausePanel == null || pausePanel.activeSelf) return;

        pausePanel.SetActive(true);
        PauseTimer();
    }

    public void ClosePausePanel()
    {
        if (pausePanel == null || !pausePanel.activeSelf) return;

        pausePanel.SetActive(false);
        ResumeTimer();
    }
    public void ShowGameOverPanel()
    {
        Invoke(nameof(ShowGameOverPanelDelayed), 3.5f);
    }

    private void ShowGameOverPanelDelayed()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        PauseTimer();

        //  점수 표시
        if (gameOverScoreText != null)
            gameOverScoreText.text = $"Score : {score}";

        //  시간 표시
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        if (gameOverTimerText != null)
            gameOverTimerText.text = $"Time : {minutes:D2}:{seconds:D2}";
    }
}
