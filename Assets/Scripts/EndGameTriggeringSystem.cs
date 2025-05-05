using DiceGame.Mechanics.InGameScoreManager;
using DiceThrower.Mechanics.Thrower;
using DiceGame.InGameCanvasManager;
using UnityEngine;
using Zenject;

namespace DiceGame.Mechanics.EndGameCase
{
    public class EndGameTriggeringSystem : MonoBehaviour
    {
        [Inject] CanvasManager canvasManager;
        [Inject] ScoreManager scoreManager;
        [Inject] DiceThrowerController diceThrowerController;

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