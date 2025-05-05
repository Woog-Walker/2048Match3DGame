using UnityEngine;

namespace DiceGame.InGameSoundsController
{
    public class InGameSounds : MonoBehaviour
    {
        [SerializeField] AudioClip soundPlayButtonClick, soundReleaseDice, soundDicesMerdge;

        AudioSource soundSource;

        private void Awake() => soundSource = GetComponent<AudioSource>();

        public void PlaySoundButtonClick() => soundSource.PlayOneShot(soundPlayButtonClick);

        public void PlaySoundReleaseDice()
        {
            RandomizeSoundOptions();
            soundSource.PlayOneShot(soundReleaseDice);
        }

        public void PlaySoundDicesMerdge()
        {
            RandomizeSoundOptions();
            soundSource.PlayOneShot(soundDicesMerdge);
        }

        void RandomizeSoundOptions()
        {
            soundSource.pitch = Random.Range(0.95f, 1.1f);
            soundSource.volume = Random.Range(0.9f, 1.1f);
        }
    }
}