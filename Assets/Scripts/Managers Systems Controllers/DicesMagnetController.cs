using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DiceGame.SingleDice.Controller;

namespace DiceGame.Mechanics.MagneteForDices
{
    public class DicesMagnetController : MonoBehaviour
    {
        [SerializeField] public List<GameObject> dicesInRange = new();

        [SerializeField] private float searchRadius;
        private const string diceTag = "Dice";

        public void FindDicesInRange(Transform incDice)
        {
            dicesInRange.Clear();

            var colliders = Physics.OverlapSphere(incDice.position, searchRadius);

            foreach (var col in colliders)
                if (col.CompareTag(diceTag))
                    dicesInRange.Add(col.gameObject);
        }

        public GameObject PushDiceToSimiliar(int incDiceValue) =>
            dicesInRange.FirstOrDefault(dice =>
                dice.GetComponent<DiceController>().GetDiceValue() == incDiceValue);
    }
}
