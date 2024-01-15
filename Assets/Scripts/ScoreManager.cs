using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelNumberText;
    public float scoreIncreaseSpeed = 10f;

    private int currentScore = 0;
    private int currentLevel = 1;
    private Coroutine scoreCoroutine;

    void Start()
    {
        scoreCoroutine = StartCoroutine(IncreaseScore());
    }

    IEnumerator IncreaseScore()
    {
        while (true)
        {
            // Wait until the game is not paused

            yield return new WaitForSeconds(1 / scoreIncreaseSpeed);
            currentScore++;
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            string formattedScore = string.Format("{0:D6}", currentScore);
            scoreText.text = formattedScore;
        }
    }

    // Stop the score increase when the script is disabled
    void OnDisable()
    {
        StopCoroutine(scoreCoroutine);
    }

    // Call this method to add points to the score when the enemy is destroyed
    public void AddPointsOnEnemyDestroyed(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        UpdateScoreUI();
        IncreaseLevel();
    }

    void IncreaseLevel()
    {
        currentLevel++;
        UpdateLevelUI();
    }

    void UpdateLevelUI()
    {
        if (levelNumberText != null)
        {
            levelNumberText.text = currentLevel.ToString("D2");
        }
    }
}
