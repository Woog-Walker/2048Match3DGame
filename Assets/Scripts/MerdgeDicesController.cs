using DiceGame.Mechanics.InGameScoreManager;
using DiceGame.Mechanics.InGameSoundsController;
using DiceThrower.Mechanics.Thrower;
using System.Collections;
using UnityEngine;
using Zenject;

namespace DiceGame.Mechanics.DiceMerdger
{
    public class MerdgeDicesController : MonoBehaviour
    {
        [SerializeField] GameObject dice_1;
        [SerializeField] GameObject dice_2;
        [Space]
        [SerializeField] GameObject dicePrefab;

        int scorePerDice;
        Vector3 posBetweenDices;
        [Inject] ScoreManager scoreManager;
        [Inject] InGameSounds inGameSounds;
        [Inject] PoolOfParticlesToMerdge poolOfParticlesToMerdge;
        [Inject] DiceThrowerController diceThrowerController;

        public void PerformDicesMerdge(GameObject incDice_1, GameObject incDice_2, int _scorePerDice)
        {
            dice_1 = incDice_1;
            dice_2 = incDice_2;

            scorePerDice = _scorePerDice * 2;

            posBetweenDices = Vector3.Lerp(dice_1.transform.position, dice_2.transform.position, 0.5f);

            scoreManager.CurrentScoreAdd(_scorePerDice);

            inGameSounds.PlaySoundDicesMerdge();

            StartCoroutine(PerformVfxOnMerdge());

            StartCoroutine(PerformMerdging());
        }

        IEnumerator PerformMerdging()
        {
            Destroy(dice_1);
            Destroy(dice_2);

            yield return new WaitForEndOfFrame();

            diceThrowerController.CreateDiceOnMerdge(posBetweenDices, scorePerDice);
        }

        IEnumerator PerformVfxOnMerdge()
        {
            var vfx = poolOfParticlesToMerdge.GetVfxFromPool();     // get vfx from pool
            float vfxDuration = vfx.main.duration;                  // get vfx duration

            vfx.transform.SetParent(null);                          // set vfx parent to null
            vfx.transform.position = posBetweenDices;               // set vfx position between 2 dices
            vfx.Play();                                             // play vfx

            yield return new WaitForSeconds(vfxDuration);           // wait for vfx duration

            poolOfParticlesToMerdge.PutBackVFxToPool(vfx);          // put vfx back to pool
        }
    }
}