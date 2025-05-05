using UnityEngine;
using System.Collections.Generic;
using DiceGame.SingleDice.Controller;

namespace DiceGame.Mechanics.MagneteForDices
{
    public class DicesMagnetController : MonoBehaviour
    {
        public List<GameObject> dicesInRange = new List<GameObject>();

        [SerializeField] private float searchRadius;

        const string diceTag = "Dice";

        public void FindDicesInRange(Transform incDice)
        {
            dicesInRange.Clear(); // Clear the list before populating

            Collider[] colliders = Physics.OverlapSphere(incDice.position, searchRadius);

            foreach (Collider col in colliders)
                if (col.CompareTag(diceTag))
                    dicesInRange.Add(col.gameObject);
        }

        public GameObject PushDiceToSimiliar(int incDiceValue)
        {
            foreach (GameObject dice in dicesInRange)
            {
                int tmpDiceValue = dice.GetComponent<DiceController>().GetDiceValue();

                if (incDiceValue == tmpDiceValue)                
                    return dice;              
            }

            return null;
        }
    }
}