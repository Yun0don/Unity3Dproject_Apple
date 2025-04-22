using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"[ScoreManager] 점수 +{amount}, 현재 총점: {score}");
    }

    public void ResetScore()
    {
        score = 0;
    }
}