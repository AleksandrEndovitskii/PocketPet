using System;

namespace Utils
{
    [Serializable]
    public class KeyValue
    {
        public string Key;
        public float Value;

        public KeyValue(string key, float value)
        {
            Key = key;
            Value = value;
        }
    }
}