using System.Threading.Tasks;
using UnityEngine;

namespace DiceThrower.Mechanics.Thrower
{
    public class DiceThrowerController : MonoBehaviour
    {
        [SerializeField] private GameObject diceToThrow;
        [SerializeField] private GameObject dicePrefab;
        [SerializeField] private Vector3 diceStartPosition;
        [Space]
        [SerializeField] float forceAmount;
        [SerializeField] float holdOffset;
        [SerializeField] float clampValueX;
        [SerializeField] float timeCdForSpawn;

        bool canThrow = false;
        Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
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
            canThrow = true;
        }

        void ThrowDice()
        {
            canThrow = false;
            // decline to throw dice

            diceToThrow.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount, ForceMode.Impulse);

            RespawnSystem();
        }

        private async void RespawnSystem()
        {
            diceToThrow = null; // clear dice holder
            await Task.Delay((int)(timeCdForSpawn * 1000));
            CreateDice();
        }
    }
}