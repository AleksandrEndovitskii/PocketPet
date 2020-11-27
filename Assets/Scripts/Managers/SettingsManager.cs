using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SettingsManager : MonoBehaviour
    {
        public event Action<string, float> ValueChanged = delegate { };

        [SerializeField]
        private List<KeyValuePair> _defaultValues = new List<KeyValuePair>();

        [Serializable]
        public class KeyValuePair
        {
            public string Key;
            public float Value;
        }

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
            Debug.Log($"{key} settings changed from {PlayerPrefs.GetFloat(key)} to {value}");

            PlayerPrefs.SetFloat(key, value);

            ValueChanged.Invoke(key, PlayerPrefs.GetFloat(key));
        }

        private void ResetSettingToDefault()
        {
            foreach (var keyValuePair in _defaultValues)
            {
                if (!PlayerPrefs.HasKey(keyValuePair.Key))
                {
                    PlayerPrefs.SetFloat(keyValuePair.Key, keyValuePair.Value);
                }
            }
        }
    }
}