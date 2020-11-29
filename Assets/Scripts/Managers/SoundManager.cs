using UnityEngine;
using Utils;

namespace Managers
{
    public class SoundManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        [SerializeField]
        private AudioSource _backgroundMusicAudioSourcePrefab;
        [SerializeField]
        private AudioSource _soundsAudioSourcePrefab;

        private AudioSource _backgroundMusicAudioSourceInstance;
        private AudioSource _soundsAudioSourceInstance;

        public void Initialize()
        {
            _backgroundMusicAudioSourceInstance = Instantiate(_backgroundMusicAudioSourcePrefab);
            _soundsAudioSourceInstance = Instantiate(_soundsAudioSourcePrefab);
        }
        public void UnInitialize()
        {
            Destroy(_soundsAudioSourceInstance);
            Destroy(_backgroundMusicAudioSourceInstance);
        }

        public void PlaySound(AudioClip audioClip)
        {
            _soundsAudioSourceInstance.PlayOneShot(audioClip);
        }
    }
}