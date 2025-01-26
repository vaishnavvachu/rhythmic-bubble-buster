using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }
}