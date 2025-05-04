using UnityEngine;
using DiceGame.InGameCanvasManager;
using DiceThrower.Mechanics.Thrower;
using System.Collections;

public class GameFlowController : MonoBehaviour
{
    CanvasManager canvasManager;
    DiceThrowerController diceThrowerController;

    float delayToStartGame = 1;

    private void Awake()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        diceThrowerController = FindObjectOfType<DiceThrowerController>();
    }

    public void StartGameFlow() => StartCoroutine(DelayForStartEngine());

    IEnumerator DelayForStartEngine()
    {
        canvasManager.TutorialDisable();                             // disable tutorial on board
        canvasManager.UiOnBoardStartLinesIsActive();                // enable start lines on board
        canvasManager.UiButtonPlayIsActive(false);                 // disable play button menu

        yield return new WaitForSeconds(delayToStartGame);

        diceThrowerController.CreateDice();                       // create dice on board | start system
    }
}