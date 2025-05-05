using System.Collections.Generic;
using DiceGame.SingleDice.Canvas;
using DiceGame.SingleDice.Materials;
using DiceGame.Mechanics.DiceMerdger;
using DiceGame.Mechanics.EndGameCase;
using DG.Tweening;
using UnityEngine;

namespace DiceGame.SingleDice.Controller
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] private int diceValue;
        [SerializeField] private float pushmentForwardForce, pushmentUpForce, magneteMultiplyer;

        private bool isTriggeringWithEndGame = false;

        private const string diceTag = "Dice";
        private const string endGameTriggeringZone = "EndGameZone";

        private EndGameTriggeringSystem endGameTriggeringSystem;
        private MerdgeDicesController merdgeDicesController;

        private DiceMaterialChanger diceMaterialChanger;
        private DiceCanvasController diceCanvasController;
        private TrailRenderer trailRenderer;
        private new Rigidbody rigidbody;

        static readonly Dictionary<int, int> valueToMaterialIndex = new()
        {
            { 2, 0 }, { 4, 1 }, { 8, 2 }, { 16, 3 },
            { 32, 4 }, { 64, 5 }, { 128, 6 },
            { 256, 6 }, { 512, 6 }, { 1024, 6 }
        };

        private void Awake()
        {
            diceCanvasController = GetComponent<DiceCanvasController>();
            diceMaterialChanger = GetComponent<DiceMaterialChanger>();
            trailRenderer = GetComponentInChildren<TrailRenderer>();
            rigidbody = GetComponent<Rigidbody>();

            merdgeDicesController = FindObjectOfType<MerdgeDicesController>();
            endGameTriggeringSystem = FindObjectOfType<EndGameTriggeringSystem>();
        }

        public void TrailRendererChangeState(bool incState) => trailRenderer.enabled = incState;

        public void DiceAppearTweenSale(float scaleTime)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, scaleTime);
        }

        public void SetDiceMaterial()
        {
            if (valueToMaterialIndex.TryGetValue(diceValue, out int index))
                diceMaterialChanger.ChangeDiceMaterial(index);
        }

        public void SetDiceCanvasValue() => diceCanvasController.UpdateDiceValue(diceValue);

        public void EnableCaseToTriggerWithEndGameZone() => isTriggeringWithEndGame = true;

        public void SetDiceValue(int incValue) => diceValue = incValue;

        public int GetDiceValue() => diceValue;

        public void PerformPushmentForward() =>
            rigidbody.AddForce(Vector3.forward * pushmentForwardForce, ForceMode.Impulse);

        public void PerformPushmentUp(Transform magneteDiceTransform)
        {
            Vector3 finalForce = magneteDiceTransform != null
                ? (Vector3.up + (magneteDiceTransform.position - transform.position).normalized * magneteMultiplyer).normalized * pushmentUpForce
                : Vector3.up * pushmentUpForce;

            rigidbody.AddForce(finalForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.CompareTag(diceTag)) return;

            int triggeredDiceValue = collision.transform.GetComponent<DiceController>().GetDiceValue();

            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID() && triggeredDiceValue == diceValue)
                merdgeDicesController.PerformDicesMerdge(gameObject, collision.gameObject, triggeredDiceValue);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(endGameTriggeringZone))
                EnableCaseToTriggerWithEndGameZone();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(endGameTriggeringZone) && isTriggeringWithEndGame)
                endGameTriggeringSystem.PerformEndGameCase();
        }
    }
}
