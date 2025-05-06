using DiceGame.Mechanics.InGameScoreManager;
using DiceGame.Mechanics.InGameSoundsController;
using DiceGame.Mechanics.PoolOfVFX;
using DiceGame.Mechanics.Thrower;
using System.Collections;
using UnityEngine;
using Zenject;

namespace DiceGame.Mechanics.DiceMerdger
{
    public class MerdgeDicesController : MonoBehaviour
    {
        [Header("Dice Prefab")]
        [SerializeField] private GameObject dicePrefab;

        private GameObject dice_1;
        private GameObject dice_2;
        private Vector3 posBetweenDices;
        private int scorePerDice;
        private float halfValue = 0.5f;

        [Inject] private ScoreManager scoreManager;
        [Inject] private InGameSounds inGameSounds;
        [Inject] private PoolOfParticlesToMerdge poolOfParticlesToMerdge;
        [Inject] private DiceThrowerController diceThrowerController;

        public void PerformDicesMerdge(GameObject incDice_1, GameObject incDice_2, int _scorePerDice)
        {
            dice_1 = incDice_1;
            dice_2 = incDice_2;
            scorePerDice = _scorePerDice * 2;

            posBetweenDices = Vector3.Lerp(dice_1.transform.position, dice_2.transform.position, halfValue);

            scoreManager.CurrentScoreAdd(_scorePerDice);
            inGameSounds.PlaySoundDicesMerdge();

            StartCoroutine(PerformVfxOnMerdge());
            StartCoroutine(PerformMerdging());
        }

        private IEnumerator PerformMerdging()
        {
            Destroy(dice_1);
            Destroy(dice_2);

            yield return new WaitForEndOfFrame();

            diceThrowerController.CreateDiceOnMerdge(posBetweenDices, scorePerDice);
        }

        private IEnumerator PerformVfxOnMerdge()
        {
            var vfx = poolOfParticlesToMerdge.GetVfxFromPool();
            var vfxDuration = vfx.main.duration;

            vfx.transform.SetParent(null);
            vfx.transform.position = posBetweenDices;
            vfx.Play();

            yield return new WaitForSeconds(vfxDuration);

            poolOfParticlesToMerdge.PutBackVFxToPool(vfx);
        }
    }
}
