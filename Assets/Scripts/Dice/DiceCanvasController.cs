using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DiceGame.SingleDice.Canvas
{
    public class DiceCanvasController : MonoBehaviour
    {
        [SerializeField] List<TMP_Text> listOfSideTexts = new List<TMP_Text>(); // 6 sides per dice - 6 text per dice

        public void UpdateDiceValue(int invValue) => listOfSideTexts.ForEach(tmpText => tmpText.text = invValue.ToString());
    }
}