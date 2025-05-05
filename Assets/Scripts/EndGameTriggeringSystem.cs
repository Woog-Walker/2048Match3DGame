using DiceGame.InGameCanvasManager;
using DiceGame.InGameScoreManager;
using DiceThrower.Mechanics.Thrower;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DiceGame.EndGameCase
{
    public class EndGameTriggeringSystem : MonoBehaviour
    {
        CanvasManager canvasManager;
        ScoreManager scoreManager;
        DiceThrowerController diceThrowerController;

        private void Awake()
        {
            canvasManager = FindObjectOfType<CanvasManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
            diceThrowerController = FindObjectOfType<DiceThrowerController>();
        }

        public void PerformEndGameCase()
        {
            diceThrowerController.SetStateGameOver();

            scoreManager.HighScoreSave();

            canvasManager.UiCurrentScoreIsActive(false);
            canvasManager.UiOpenGameOverWindow();
            canvasManager.UpdateEndScoreText(scoreManager.GetCurrentScore());
        }
    }
}