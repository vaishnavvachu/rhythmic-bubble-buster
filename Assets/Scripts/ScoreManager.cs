using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score;
    private UIManager _uiManager;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();  
    }

    public void AddScore(int points)
    {
        score += points;
        _uiManager.AddScore(points);
    }

    public int GetScore()
    {
        return score;
    }
}