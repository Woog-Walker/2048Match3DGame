using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DiceGame.InGameCanvasManager
{
    public class CanvasManager : MonoBehaviour
    {
        [Header("Score text")]
        [SerializeField] TMP_Text textCurrentScore;
        [SerializeField] TMP_Text textHighScore;
        [Space]
        [Header("Tutorial")]
        [SerializeField] TMP_Text textTutorial;
        [SerializeField] Image imageTutorial;
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
        public void UiHighScoreIsActive(bool isActive) => textCurrentScore.gameObject.SetActive(isActive);
        #endregion

        #region CURRENT SCORE PANEL
        public void UpdateCurrentScoreText(int incValue) => textCurrentScore.text = incValue.ToString();
        public void UiCurrentScoreIsActive(bool isActive) => textHighScore.gameObject.SetActive(isActive);
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