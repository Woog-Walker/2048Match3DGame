using DiceGame.GameFlow;
using UnityEngine;
using UnityEngine.UI;

namespace DiceGame.Canvas.ButtonSettings
{
    public class UiButtonRestartSubscriber : MonoBehaviour
    {
        Button startButton;
        GameFlowController gameFlowController;

        private void Awake()
        {
            startButton = GetComponent<Button>();
            gameFlowController = FindObjectOfType<GameFlowController>();
        }

        private void Start() => startButton.onClick.AddListener(SubscribeButton);

        void SubscribeButton() => gameFlowController.RestartScene();
    }
}