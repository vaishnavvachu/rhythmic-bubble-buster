using UnityEngine;
using UnityEngine.UI; // Use this for UI Text
using TMPro; // Use this for TextMeshPro

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Reference to the TextMeshPro UI Text (or use Text if you're not using TMP)
    private int score = 0;             // The player's current score

    private void Start()
    {
        UpdateScoreUI(); // Initialize the score display at the start
    }

    // Function to update the score text on the UI
    public void UpdateScore(int newScore)
    {
        score = newScore;
        UpdateScoreUI();
    }

    // Function to increase the score (e.g., when a bubble is popped or player performs a correct action)
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreUI();
    }

    // Function to update the score UI element
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text =  score.ToString();
        }
    }
}