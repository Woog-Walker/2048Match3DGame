using DiceGame.Mechanics.InGameSoundsController;
using DiceGame.Mechanics.MagneteForDices;
using DiceGame.SingleDice.Controller;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace DiceThrower.Mechanics.Thrower
{
    public class DiceThrowerController : MonoBehaviour
    {
        [Header("Dice Setup")]
        [SerializeField] private GameObject dicePrefab;
        [SerializeField] private Vector3 diceStartPosition;

        [Header("Throw Settings")]
        [SerializeField] private float forceAmount;
        [SerializeField] private float holdOffset;
        [SerializeField] private float clampValueX;
        [SerializeField] private float timeCdForSpawn;

        [Header("State")]
        private bool canThrow;
        private bool isGameOver;

        private GameObject diceToThrow;
        private float chanceFor4x = 25f;
        private Camera mainCamera;

        [Inject] private InGameSounds inGameSounds;
        [Inject] private DicesMagnetController dicesMagnetController;

        private void Awake() => mainCamera = Camera.main;

        private void Update()
        {
            if (isGameOver || !canThrow || Input.touchCount == 0) return;

            foreach (Touch touch in Input.touches)
            {
                if (touch.position.y >= Screen.height / 2) continue;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        MoveDiceBack();
                        break;
                    case TouchPhase.Moved:
                        MoveDiceWithTouch(touch.position);
                        break;
                    case TouchPhase.Ended:
                        ThrowDice();
                        break;
                }
            }
        }

        private void MoveDiceBack()
        {
            if (diceToThrow == null) return;
            var pos = diceToThrow.transform.position;
            pos.z -= holdOffset;
            diceToThrow.transform.position = pos;
        }

        private void MoveDiceWithTouch(Vector2 touchPosition)
        {
            if (diceToThrow == null) return;

            float zDepth = Mathf.Abs(mainCamera.transform.position.z - diceToThrow.transform.position.z);
            Vector3 screenPosition = new Vector3(touchPosition.x, touchPosition.y, zDepth);
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPosition);

            float clampedX = Mathf.Clamp(worldPos.x, -clampValueX, clampValueX);
            diceToThrow.transform.position = new Vector3(clampedX, diceToThrow.transform.position.y, diceToThrow.transform.position.z);
        }


        public void CreateDice()
        {
            diceToThrow = Instantiate(dicePrefab, diceStartPosition, Quaternion.identity);
            var diceController = diceToThrow.GetComponent<DiceController>();

            diceController.SetDiceValue(GenerateRandomValueForDice() < chanceFor4x ? 4 : 2);
            SetupDiceVisuals(diceController);

            StartCoroutine(DelayOnDiceCreation(timeCdForSpawn));
        }

        public void CreateDiceOnMerdge(Vector3 worldPos, int diceValue)
        {
            var newDice = Instantiate(dicePrefab, worldPos, Quaternion.identity);
            var diceController = newDice.GetComponent<DiceController>();

            diceController.SetDiceValue(diceValue);
            SetupDiceVisuals(diceController);
            diceController.EnableCaseToTriggerWithEndGameZone();

            dicesMagnetController.FindDicesInRange(newDice.transform);
            var targetDice = dicesMagnetController.PushDiceToSimiliar(diceValue);

            diceController.PerformPushmentUp(targetDice.transform);

            StartCoroutine(DelayOnDiceCreation(timeCdForSpawn));
        }

        private void SetupDiceVisuals(DiceController diceController)
        {
            diceController.SetDiceMaterial();
            diceController.SetDiceCanvasValue();
            diceController.DiceAppearTweenSale(timeCdForSpawn);
        }

        private IEnumerator DelayOnDiceCreation(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            canThrow = true;
        }

        private void ThrowDice()
        {
            if (diceToThrow == null) return;

            var controller = diceToThrow.GetComponent<DiceController>();
            controller.PerformPushmentForward();
            controller.TrailRendererChangeState(true);

            inGameSounds.PlaySoundReleaseDice();

            diceToThrow = null;
            canThrow = false;

            RespawnSystem();
        }

        private async void RespawnSystem()
        {
            await Task.Delay((int)(timeCdForSpawn * 1000));
            CreateDice();
        }

        private float GenerateRandomValueForDice() => Random.Range(0f, 100f);

        public void SetStateGameOver() => isGameOver = true;
    }
}
