using UnityEngine;
using UnityEngine.UI;

public class UiButtonStartSubscriber : MonoBehaviour
{
    Button startButton;
    GameFlowController gameFlowController;

    private void Awake()
    {
        startButton = GetComponent<Button>();
        gameFlowController = FindObjectOfType<GameFlowController>();
    }

    private void Start() => startButton.onClick.AddListener(SubscribeButton);

    void SubscribeButton()
    {
        gameFlowController.StartGameFlow();
    }
}