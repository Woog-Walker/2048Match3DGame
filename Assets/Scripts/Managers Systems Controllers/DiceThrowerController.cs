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
        [SerializeField] private GameObject diceToThrow, dicePrefab;
        [SerializeField] private Vector3 diceStartPosition;
        [Space]
        [SerializeField] float forceAmount, holdOffset, clampValueX, timeCdForSpawn;
        [Space]
        [SerializeField] bool canThrow, isGameOver = false;

        float chanceFor4x = 25; // 25 % for 4x dice

        Camera mainCamera;
        [Inject] InGameSounds inGameSounds;
        [Inject] DicesMagnetController dicesMagnetController;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (isGameOver) return;
            if (!canThrow) return;

            foreach (Touch touch in Input.touches)
            {
                if (touch.position.y < Screen.height / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector3 startDicePos = diceToThrow.transform.position;
                        startDicePos.z = startDicePos.z - holdOffset;

                        diceToThrow.transform.position = startDicePos;
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        // Преобразуем позицию касания в мировые координаты
                        Vector3 touchPosition = touch.position;
                        touchPosition.z = Mathf.Abs(mainCamera.transform.position.z - diceToThrow.transform.position.z); // Расстояние до объекта
                        Vector3 worldPos = mainCamera.ScreenToWorldPoint(touchPosition);

                        // Ограничиваем X в диапазоне [-5, 5]
                        float clampedX = Mathf.Clamp(worldPos.x, -clampValueX, clampValueX);

                        // Применяем ограниченное значение X
                        diceToThrow.transform.position = new Vector3(clampedX, diceToThrow.transform.position.y, diceToThrow.transform.position.z);
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        ThrowDice();
                    }
                }
            }
        }

        public void CreateDice()
        {
            diceToThrow = Instantiate(dicePrefab, diceStartPosition, Quaternion.identity);

            float rndValue = GenerateRandomValueForDice();

            if (chanceFor4x > rndValue)
                diceToThrow.GetComponent<DiceController>().SetDiceValue(4);
            else
                diceToThrow.GetComponent<DiceController>().SetDiceValue(2);

            diceToThrow.GetComponent<DiceController>().SetDiceMaterial();
            diceToThrow.GetComponent<DiceController>().SetDiceCanvasValue();
            diceToThrow.GetComponent<DiceController>().DiceAppearTweenSale(timeCdForSpawn);

            StartCoroutine(DelayOnDiceCreation(timeCdForSpawn));
        }

        float GenerateRandomValueForDice() => Random.Range(0, 100);

        public void CreateDiceOnMerdge(Vector3 worldPos, int diceValue)
        {
            var _tmpDice = Instantiate(dicePrefab, worldPos, Quaternion.identity);
            _tmpDice.GetComponent<DiceController>().SetDiceValue(diceValue);
            _tmpDice.GetComponent<DiceController>().SetDiceMaterial();
            _tmpDice.GetComponent<DiceController>().SetDiceCanvasValue();
            _tmpDice.GetComponent<DiceController>().DiceAppearTweenSale(timeCdForSpawn);
            _tmpDice.GetComponent<DiceController>().EnableCaseToTriggerWithEndGameZone();

            dicesMagnetController.FindDicesInRange(_tmpDice.transform);
            GameObject magneteDice =  dicesMagnetController.PushDiceToSimiliar(diceValue);
            _tmpDice.GetComponent<DiceController>().PerformPushmentUp(magneteDice.transform);

            StartCoroutine(DelayOnDiceCreation(timeCdForSpawn));
        }

        IEnumerator DelayOnDiceCreation(float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            canThrow = true;
        }

        void ThrowDice()
        {
            diceToThrow.GetComponent<DiceController>().PerformPushmentForward();
            diceToThrow.GetComponent<DiceController>().TrailRendererChangeState(true);

            canThrow = false;
            diceToThrow = null; // clear dice holder

            inGameSounds.PlaySoundReleaseDice();

            RespawnSystem();
        }

        private async void RespawnSystem()
        {
            await Task.Delay((int)(timeCdForSpawn * 1000));
            CreateDice();
        }

        public void SetStateGameOver() => isGameOver = true;
    }
}