using DiceGame.Mechanics.GameFlow;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace DiceGame.Canvas.ButtonSettings
{
    public class UiButtonStartSubscriber : MonoBehaviour
    {
        private Button startButton;
        [Inject] private GameFlowController gameFlowController;

        private void Awake() => startButton = GetComponent<Button>();
        private void Start() => startButton.onClick.AddListener(SubscribeButton);
        private void SubscribeButton() => gameFlowController.StartGameFlow();
    }
}