using DiceGame.GameFlow;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace DiceGame.Canvas.ButtonSettings
{
    public class UiButtonStartSubscriber : MonoBehaviour
    {
        Button startButton;
        [Inject] GameFlowController gameFlowController;

        private void Awake()
        {
            startButton = GetComponent<Button>();
        }

        private void Start() => startButton.onClick.AddListener(SubscribeButton);

        void SubscribeButton() => gameFlowController.StartGameFlow();
    }
}