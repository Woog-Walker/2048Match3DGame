using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiceGame.InGameCanvasManager
{
    public class CanvasManager : MonoBehaviour
    {
        [Header("Score texts and objects")]
        [SerializeField] TMP_Text textCurrentScore;
        [SerializeField] TMP_Text textHighScore;
        [SerializeField] GameObject highScoreObject;
        [Space]

        [Header("Tutorial")]
        [SerializeField] TMP_Text textTutorial;
        [SerializeField] Image imageTutorial;
        [Space]

        [Header("Game Over Panel")]
        [SerializeField] GameObject panelGameOver;
        [SerializeField] TMP_Text textOverScore;
        [Space]

        [SerializeField] Image [] startLineImages;
        [SerializeField] Button buttonPlay;
         
        #region TUTORIAL
        public void TutorialDisable()
        {
            imageTutorial.gameObject.SetActive(false);
            textTutorial.gameObject.SetActive(false);
        }
        #endregion

        #region HIGH SCORE PANEL
        public void UpdateHighScoreText(int incValue) => textHighScore.text = incValue.ToString();
        public void UiHighScoreIsActive(bool isActive) => highScoreObject.SetActive(isActive);
        #endregion

        #region CURRENT SCORE PANEL
        public void UpdateCurrentScoreText(int incValue) => textCurrentScore.text = incValue.ToString();
        public void UiCurrentScoreIsActive(bool isActive) => textCurrentScore.gameObject.SetActive(isActive);
        #endregion

        #region PANEL GAME OVER
        public void UiOpenGameOverWindow() => panelGameOver.gameObject.SetActive(true);
        public void UpdateEndScoreText(int incValue) => textOverScore.text = incValue.ToString();
        #endregion

        // BUTTON PLAY 
        public void UiButtonPlayIsActive(bool isActive) => buttonPlay.gameObject.SetActive(isActive);

        // IMAGES ON BOARD - START LINES
        public void UiOnBoardStartLinesIsActive()
        {
            foreach (var img in startLineImages)
                img.enabled = true;
        }
    }
}