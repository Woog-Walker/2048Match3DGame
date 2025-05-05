using DiceGame.InGameCanvasManager;
using DiceGame.Mechanics.InGameScoreManager;
using DiceThrower.Mechanics.Thrower;
using UnityEngine;

namespace DiceGame.Mechanics.EndGameCase
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