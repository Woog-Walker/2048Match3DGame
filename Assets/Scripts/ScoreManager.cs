using UnityEngine;
using DiceGame.InGameCanvasManager;
using Zenject;

namespace DiceGame.Mechanics.InGameScoreManager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] int currentScore = 0;
        [SerializeField] int highScore = 0;

        [Inject] CanvasManager canvasManager;

        const string highScorePrefs = "prefHighScore001";

        private void Start()
        {
            HighScoreLoad();
            canvasManager.UpdateCurrentScoreText(currentScore);
        }

        // CURRENT SCORE
        public void CurrentScoreAdd(int incValue)
        {
            currentScore += incValue;
            canvasManager.UpdateCurrentScoreText(currentScore);
        }

        public int GetCurrentScore() => currentScore;

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

        public void HighScoreSave()
        {
            if (!PlayerPrefs.HasKey(highScorePrefs)) PlayerPrefs.SetInt(highScorePrefs, currentScore);
            else
            {
                if (currentScore > PlayerPrefs.GetInt(highScorePrefs))
                    PlayerPrefs.SetInt(highScorePrefs, currentScore);
            }
        }
    }
}