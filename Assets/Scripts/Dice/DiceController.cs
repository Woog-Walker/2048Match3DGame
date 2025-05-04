using UnityEngine;
using DiceGame.SingleDice.Canvas;
using DiceGame.SingleDice.Materials;
using UnityEngine.UIElements;

namespace DiceGame.SingleDice.Controller
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] int diceValue;

        const string diceTag = "Dice";
        DiceMaterialChanger diceMaterialChanger;
        DiceCanvasController diceCanvasController;

        private void Awake()
        {
            diceMaterialChanger = GetComponent<DiceMaterialChanger>();
            diceCanvasController = GetComponent<DiceCanvasController>();
        }

        private void Start()
        {
/*            diceValue = 2;
            diceMaterialChanger.ChangeDiceMaterial(0);
            diceCanvasController.UpdateDiceValue(diceValue);*/
        }

        public int GetDiceValue() => diceValue;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(diceTag))
            {
                int triggeredDiceValue = collision.transform.GetComponent<DiceController>().GetDiceValue();

                Debug.Log($"CURRENT DICE - {diceValue}");
                Debug.Log($"TRIGGERED DICE - {triggeredDiceValue}");

                Destroy(collision.gameObject);
                Destroy(transform.gameObject);
            }
        }
    }
}