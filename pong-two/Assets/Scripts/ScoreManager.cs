using System;
using TMPro;
using UnityEngine;

namespace ZPong
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        public int scorePlayer1 = 0; // Score for Player 1
        public int scorePlayer2 = 0; // Score for Player 2
        public int winningScore = 11; // Score required to win the game

        public TMP_Text leftScoreText;
        public TMP_Text rightScoreText;
        public GameObject victoryUI;
        public TMP_Text victoryText;
        public FlashEffect flashEffect;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            winningScore = PlayerPrefs.GetInt("ScoreToWin", winningScore); // Use a default value if not set
            ResetGame();
        }

        public void ScorePointPlayer1()
        {
            scorePlayer1++;
            leftScoreText.text = scorePlayer1.ToString();

            Debug.Log("Player 1 scored! Current score: " + scorePlayer1);

            ApplyScoreEffects();
            if (!CheckWinCondition())
            {
                GameManager.Instance.ResetBall();
            }
        }

        public void ScorePointPlayer2()
        {
            scorePlayer2++;
            rightScoreText.text = scorePlayer2.ToString();

            Debug.Log("Player 2 scored! Current score: " + scorePlayer2);

            ApplyScoreEffects();
            if (!CheckWinCondition())
            {
                GameManager.Instance.ResetBall();
            }
        }


        private void ApplyScoreEffects()
        {
            flashEffect.TriggerFlash();
            ScreenShake.Instance.Shake(0.2f, 0.3f); // Adjust the shake duration and magnitude as needed
        }

        private bool CheckWinCondition()
        {
            if (scorePlayer1 >= winningScore)
            {
                HandleVictory("PLAYER 1\nWINS");
                return true;
            }
            else if (scorePlayer2 >= winningScore)
            {
                HandleVictory("PLAYER 2\nWINS");
                return true;
            }
            return false;
        }

        private void HandleVictory(string victoryMessage)
        {
            Debug.Log(victoryMessage);
            victoryUI.SetActive(true);
            victoryText.text = victoryMessage;

            // Play victory music
            VictoryMusicManager.Instance.PlayVictoryMusic();

            // Trigger screen shake
            ScreenShake.Instance.Shake(0.5f, 1.0f);

            // Trigger flash effect
            flashEffect.TriggerFlash();

            // Disable the active ball
            GameManager.Instance.activeBall.DisableBall();
        }

        public void ResetGame()
        {
            scorePlayer1 = 0;
            scorePlayer2 = 0;

            leftScoreText.text = "0";
            rightScoreText.text = "0";

            victoryUI.SetActive(false);
        }
    }
}
