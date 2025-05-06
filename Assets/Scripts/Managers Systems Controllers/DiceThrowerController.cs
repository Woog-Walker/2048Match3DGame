using DiceGame.Mechanics.InGameSoundsController;
using DiceGame.Mechanics.MagneteForDices;
using DiceGame.SingleDice.Controller;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;
using DiceGame.Mechanics.TouchInput;

namespace DiceGame.Mechanics.Thrower
{
    public class DiceThrowerController : MonoBehaviour
    {
        [Header("Dice Setup")]
        [SerializeField] private GameObject dicePrefab;
        [SerializeField] private Vector3 diceStartPosition;

        [Header("Throw Settings")]
        [SerializeField] private float holdOffset;
        [SerializeField] private float clampValueX;
        [SerializeField] private float timeCdForSpawn;

        [Header("State")]
        private bool canThrow;

        private GameObject diceToThrow;
        private float chanceFor4x = 25f;
        private Camera mainCamera;

        [Inject] private InGameSounds inGameSounds;
        [Inject] private DicesMagnetController dicesMagnetController;
        [Inject] private TouchInputController touchInputController; // Внедряем контроллер ввода

        private void Awake()
        {
            mainCamera = Camera.main;
            touchInputController.OnTouchBegin += MoveDiceBack;
            touchInputController.OnTouchMove += MoveDiceWithTouch;
            touchInputController.OnTouchEnd += ThrowDice;
        }

        private void MoveDiceBack(Vector2 touchPosition)
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
            if (diceToThrow == null || !canThrow) return;

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

        public void SetStateGameOver()
        {
            touchInputController.OnTouchBegin -= MoveDiceBack;
            touchInputController.OnTouchMove -= MoveDiceWithTouch;
            touchInputController.OnTouchEnd -= ThrowDice;
        }

        private void OnDestroy()
        {
            touchInputController.OnTouchBegin -= MoveDiceBack;
            touchInputController.OnTouchMove -= MoveDiceWithTouch;
            touchInputController.OnTouchEnd -= ThrowDice;
        }
    }
}
