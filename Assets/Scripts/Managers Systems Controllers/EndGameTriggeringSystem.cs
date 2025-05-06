using DiceGame.Mechanics.InGameScoreManager;
using DiceGame.Mechanics.Thrower;
using DiceGame.Mechanics.InGameCanvasManager;
using UnityEngine;
using Zenject;

namespace DiceGame.Mechanics.EndGameCase
{
    public class EndGameTriggeringSystem : MonoBehaviour
    {
        [Inject] private CanvasManager _canvasManager;
        [Inject] private ScoreManager _scoreManager;
        [Inject] private DiceThrowerController _diceThrower;

        public void PerformEndGameCase()
        {
            _diceThrower.SetStateGameOver();
            _scoreManager.HighScoreSave();

            SetUpAndUpdateUi();
        }

        private void SetUpAndUpdateUi()
        {
            _canvasManager.UiCurrentScoreIsActive(false);
            _canvasManager.UiOpenGameOverWindow();
            _canvasManager.UpdateEndScoreText(_scoreManager.GetCurrentScore());
        }
    }
}
