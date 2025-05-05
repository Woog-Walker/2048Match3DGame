using UnityEngine;
using DiceGame.InGameCanvasManager;
using DiceThrower.Mechanics.Thrower;
using System.Collections;
using DiceGame.Mechanics.InGameSoundsController;
using UnityEngine.SceneManagement;
using Zenject;

namespace DiceGame.GameFlow
{
    public class GameFlowController : MonoBehaviour
    {
        [Inject] CanvasManager canvasManager;
        [Inject] DiceThrowerController diceThrowerController;
        [Inject] InGameSounds inGameSounds;

        int targetFps = 100;

        private void Start() => SetTargetAppFps();

        public void StartGameFlow() => StartCoroutine(DelayForStartEngine());

        IEnumerator DelayForStartEngine()
        {
            diceThrowerController.CreateDice();

            canvasManager.TutorialDisable();
            canvasManager.UiOnBoardStartLinesIsActive();
            canvasManager.UiButtonPlayIsActive(false);
            canvasManager.UiCurrentScoreIsActive(true);
            canvasManager.UiHighScoreIsActive(false);

            yield return new WaitForEndOfFrame();

            inGameSounds.PlaySoundButtonClick();                                   // play sound on button play click

        }

        // SET FPS FOR MOBILE DEVICES
        void SetTargetAppFps() => Application.targetFrameRate = targetFps;

        // RESTART SCENE
        public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}