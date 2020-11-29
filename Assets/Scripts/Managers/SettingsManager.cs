using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace Managers
{
    public class SettingsManager : MonoBehaviour, IInitilizable
    {
        public event Action<string, float> ValueChanged = delegate { };

        [SerializeField]
        private AudioMixer _audioMixer;
        [SerializeField]
        private List<KeyValue> _defaultValues = new List<KeyValue>();

        private List<KeyValue> _currentValues = new List<KeyValue>();

        private AudioSource _backgroundMusicAudioSourceInstance;
        private AudioSource _soundsAudioSourceInstance;

        public void Initialize()
        {
            ResetSettingToDefault();
        }

        public float Get(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
        public void Set(string key, float value)
        {
            var keyValue = _currentValues.FirstOrDefault(x=>x.Key == key);
            if (keyValue != null)
            {
                keyValue.Value = value;
            }
            else
            {
                keyValue = new KeyValue(key, value);

                _currentValues.Add(keyValue);
            }

            //TODO: move logic for audio settings to audio settings manager
            _audioMixer.SetFloat(key, value);

            ValueChanged.Invoke(key, PlayerPrefs.GetFloat(key));
        }

        private void ResetSettingToDefault()
        {
            foreach (var keyValue in _defaultValues)
            {
                if (!PlayerPrefs.HasKey(keyValue.Key))
                {
                    Debug.Log($"Value for {keyValue.Key} was not specified - " +
                              $"resetting it to default value({keyValue.Value})");

                    Set(keyValue.Key, keyValue.Value);
                }
            }
        }

        public void Save()
        {
            foreach (var keyValue in _currentValues)
            {
                Debug.Log($"{keyValue.Key} settings changed from {PlayerPrefs.GetFloat(keyValue.Key)} to " +
                          $"{keyValue.Value}");

                PlayerPrefs.SetFloat(keyValue.Key, keyValue.Value);
            }
        }
    }
}