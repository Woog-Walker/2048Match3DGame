using UnityEngine;
using DiceGame.Mechanics.InGameCanvasManager;
using DiceThrower.Mechanics.Thrower;
using System.Collections;
using DiceGame.Mechanics.InGameSoundsController;
using UnityEngine.SceneManagement;
using Zenject;

namespace DiceGame.Mechanics.GameFlow
{
    public class GameFlowController : MonoBehaviour
    {
        [Inject] private CanvasManager _canvasManager;
        [Inject] private DiceThrowerController _diceThrower;
        [Inject] private InGameSounds _inGameSounds;

        [SerializeField] private int targetFps = 100;

        private void Start() => Application.targetFrameRate = targetFps;

        public void StartGameFlow() => StartCoroutine(StartGameSequence());

        private IEnumerator StartGameSequence()
        {
            _diceThrower.CreateDice();

            SetupUIOnGameStart();

            yield return new WaitForEndOfFrame();

            _inGameSounds.PlaySoundButtonClick();
        }

        private void SetupUIOnGameStart()
        {
            _canvasManager.TutorialDisable();
            _canvasManager.UiOnBoardStartLinesIsActive();
            _canvasManager.UiButtonPlayIsActive(false);
            _canvasManager.UiCurrentScoreIsActive(true);
            _canvasManager.UiHighScoreIsActive(false);
        }

        public void RestartScene() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
