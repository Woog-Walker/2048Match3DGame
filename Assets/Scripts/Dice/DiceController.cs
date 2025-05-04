using UnityEngine;
using System.Collections.Generic;
using DiceGame.SingleDice.Canvas;
using DiceGame.SingleDice.Materials;
using DiceGame.InGameScoreManager;

namespace DiceGame.SingleDice.Controller
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] int diceValue;

        const string diceTag = "Dice";

        ScoreManager scoreManager;
        DiceMaterialChanger diceMaterialChanger;
        DiceCanvasController diceCanvasController;

        private void Awake()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            diceMaterialChanger = GetComponent<DiceMaterialChanger>();
            diceCanvasController = GetComponent<DiceCanvasController>();
        }

        private void Start()
        {
            diceValue = 2;
            SetDiceCanvasValue();
            SetDiceMaterial();
        }

        void SetDiceMaterial()
        {
            if (valueToMaterialIndex.TryGetValue(diceValue, out int index))
                diceMaterialChanger.ChangeDiceMaterial(index);
        }

        void SetDiceCanvasValue() => diceCanvasController.UpdateDiceValue(diceValue);
        public int GetDiceValue() => diceValue;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(diceTag))
            {
                int triggeredDiceValue = collision.transform.GetComponent<DiceController>().GetDiceValue();
                int currentDice = diceValue;

                if (triggeredDiceValue == currentDice)
                {
                    diceValue += diceValue;
                    diceCanvasController.UpdateDiceValue(diceValue);

                    Debug.Log("SCORE IT ");
                    scoreManager.CurrentScoreAdd(diceValue);

                    Destroy(collision.gameObject);
                }
            }
        }

        // value - material index
        // for example value 2 - 
        Dictionary<int, int> valueToMaterialIndex = new Dictionary<int, int>
        {
            { 2, 0 },
            { 4, 1 },
            { 8, 2 },
            { 16, 3 },
            { 32, 4 },
            { 64, 5 }
        };
    }
}