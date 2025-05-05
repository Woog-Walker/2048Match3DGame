using UnityEngine;

namespace DiceGame.Mechanics.InGameSoundsController
{
    public class InGameSounds : MonoBehaviour
    {
        [Header("Audio Clips")]
        [SerializeField] private AudioClip soundPlayButtonClick;
        [SerializeField] private AudioClip soundReleaseDice;
        [SerializeField] private AudioClip soundDicesMerdge;

        [Header("Audio Source")]
        [SerializeField] private AudioSource soundSource;

        [Header("Random Sound Settings")]
        [Range(0.8f, 1.2f)][SerializeField] private float pitchMin = 0.95f;
        [Range(0.8f, 1.2f)][SerializeField] private float pitchMax = 1.1f;
        [Range(0.5f, 1.5f)][SerializeField] private float volumeMin = 0.9f;
        [Range(0.5f, 1.5f)][SerializeField] private float volumeMax = 1.1f;

        private void Awake()
        {
            if (soundSource == null)
                soundSource = GetComponent<AudioSource>();
        }

        public void PlaySoundButtonClick() =>
            soundSource.PlayOneShot(soundPlayButtonClick);

        public void PlaySoundReleaseDice()
        {
            ApplyRandomSoundSettings();
            soundSource.PlayOneShot(soundReleaseDice);
        }

        public void PlaySoundDicesMerdge()
        {
            ApplyRandomSoundSettings();
            soundSource.PlayOneShot(soundDicesMerdge);
        }

        private void ApplyRandomSoundSettings()
        {
            soundSource.pitch = Random.Range(pitchMin, pitchMax);
            soundSource.volume = Random.Range(volumeMin, volumeMax);
        }
    }
}
