using UnityEngine;
using DiceGame.InGameCanvasManager;

namespace DiceGame.InGameScoreManager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] int currentScore = 0;
        [SerializeField] int highScore = 0;

        CanvasManager canvasManager;

        const string highScorePrefs = "prefHighScore001";

        private void Awake()
        {
            canvasManager = FindObjectOfType<CanvasManager>();
        }

        private void Start()
        {
            canvasManager.UpdateCurrentScoreText(currentScore);
        }

        // CURRENT SCORE
        public void CurrentScoreAdd(int incValue)
        {
            currentScore += incValue;
            canvasManager.UpdateCurrentScoreText(currentScore);
        }

        // HIGH SCORE
        public void HighScoreLoad()
        {
            if (PlayerPrefs.HasKey(highScorePrefs))
            {
                highScore = PlayerPrefs.GetInt(highScorePrefs);
                canvasManager.UpdateHighScoreText(highScore);
            }
            else
            {
                highScore = 0;
                canvasManager.UpdateHighScoreText(highScore);
            }
        }

        public void HighScoreSave(int incValue)
        {
            if (!PlayerPrefs.HasKey(highScorePrefs)) PlayerPrefs.SetInt(highScorePrefs, incValue);
            else
            {
                if (incValue > PlayerPrefs.GetInt(highScorePrefs))
                    PlayerPrefs.SetInt(highScorePrefs, incValue);
            }
        }
    }
}