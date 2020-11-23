using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Components.SliderComponents
{
    [RequireComponent(typeof(Slider))]
    public class AudioMixerParameterValueSliderComponent : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer _audioMixer;
        [SerializeField]
        private string _parameterName;

        private void Awake()
        {
            _audioMixer.GetFloat(_parameterName, out var value);
            this.gameObject.GetComponent<Slider>().value = value;

            this.gameObject.GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
        }
        private void OnDestroy()
        {
            this.gameObject.GetComponent<Slider>().onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _audioMixer.SetFloat(_parameterName, value);
        }
    }
}
