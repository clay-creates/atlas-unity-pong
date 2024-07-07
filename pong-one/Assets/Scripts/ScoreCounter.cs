using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    public Ball ball;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            score++;
            UpdateScoreText();
            ball.ResetBall();

            if (score == 11)
            {
                EndGame();
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void EndGame()
    {
        // Display end screen
        Debug.Log("Game Over");
        // Add logic to display end screen and stop the game
    }
}
