using System.Collections.Generic;
using DiceGame.SingleDice.Canvas;
using DiceGame.SingleDice.Materials;
using DiceGame.DiceMerdger;
using DiceGame.EndGameCase;
using DG.Tweening;
using UnityEngine;

namespace DiceGame.SingleDice.Controller
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] int diceValue;

        float pushmentForwardForce = 25;
        float pushmentUpForce = 10;
        float magneteMultiplyer = 0.125f;

        bool isTriggeringWithEndGame = false;
        const string diceTag = "Dice";
        const string endGameTriggeringZone = "EndGameZone";

        DiceMaterialChanger diceMaterialChanger;
        DiceCanvasController diceCanvasController;
        EndGameTriggeringSystem endGameTriggeringSystem;
        MerdgeDicesController merdgeDicesController;

        TrailRenderer trailRenderer;
        Rigidbody rigidbody;

        private void Awake()
        {
            diceCanvasController = GetComponent<DiceCanvasController>();
            diceMaterialChanger = GetComponent<DiceMaterialChanger>();
            trailRenderer = GetComponentInChildren<TrailRenderer>();
            rigidbody = GetComponent<Rigidbody>();

            endGameTriggeringSystem = FindObjectOfType<EndGameTriggeringSystem>();
            merdgeDicesController = FindObjectOfType<MerdgeDicesController>();
        }

        // DICE VALUE
        public void SetDiceValue(int incValue) => diceValue = incValue;
        public int GetDiceValue() => diceValue;

        // RIGIDBODY PUSHMENT
        public void PerformPushmentForward() => rigidbody.AddForce(Vector3.forward * pushmentForwardForce, ForceMode.Impulse);

        public void PerformPushmentUp(Transform magneteDiceTransform)
        {
            if (magneteDiceTransform != null)
            {
                Vector3 directionToMagnet = (magneteDiceTransform.position - transform.position).normalized;
                Vector3 slightPull = directionToMagnet * magneteMultiplyer;
                Vector3 finalForce = (Vector3.up + slightPull).normalized * pushmentUpForce;
                rigidbody.AddForce(finalForce, ForceMode.Impulse);
            }
            else            
                rigidbody.AddForce(Vector3.up * pushmentUpForce, ForceMode.Impulse);            
        }

        public void SetDiceCanvasValue() => diceCanvasController.UpdateDiceValue(diceValue);
        public void EnableCaseToTriggerWithEndGameZone() => isTriggeringWithEndGame = true;
        public void TrailRendererChangeState(bool incState) => trailRenderer.enabled = incState;

        // DICE TWEEN SCALE ANIMATION
        public void DiceAppearTweenSale(float scaleTime)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, scaleTime);
        }

        // DICTIONARY FOR MATERIALS COLORS
        Dictionary<int, int> valueToMaterialIndex = new Dictionary<int, int>
        {
            { 2, 0 },
            { 4, 1 },
            { 8, 2 },
            { 16, 3 },
            { 32, 4 },
            { 64, 5 },
            { 128, 6 },
            { 256, 6 },
            { 512, 6 },
            { 1024, 6 },
        };
        public void SetDiceMaterial()
        {
            if (valueToMaterialIndex.TryGetValue(diceValue, out int index))
                diceMaterialChanger.ChangeDiceMaterial(index);
        }

        // TRIGGERING AND COLLISION
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(diceTag))
            {
                int triggeredDiceValue = collision.transform.GetComponent<DiceController>().GetDiceValue();
                int currentDice = diceValue;

                // only allow the dice with the higher id to merdge to merge
                if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
                {
                    if (triggeredDiceValue == currentDice)
                    {
                        merdgeDicesController.PerformDicesMerdge(transform.gameObject, collision.gameObject, triggeredDiceValue);

                        // destroy one of 2 dices
                        Destroy(collision.gameObject);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(endGameTriggeringZone))
                isTriggeringWithEndGame = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(endGameTriggeringZone) && isTriggeringWithEndGame)
                endGameTriggeringSystem.PerformEndGameCase();
        }
    }
}