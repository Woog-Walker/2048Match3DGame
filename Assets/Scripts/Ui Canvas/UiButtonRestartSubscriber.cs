using DiceGame.GameFlow;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DiceGame.Canvas.ButtonSettings
{
    public class UiButtonRestartSubscriber : MonoBehaviour
    {
        Button startButton;
        [Inject] GameFlowController gameFlowController;

        private void Awake()
        {
            startButton = GetComponent<Button>();;
        }

        private void Start() => startButton.onClick.AddListener(SubscribeButton);

        void SubscribeButton() => gameFlowController.RestartScene();
    }
}