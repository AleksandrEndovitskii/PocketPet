using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Components.SliderComponents
{
    [RequireComponent(typeof(Slider))]
    public class MusicVolumeSliderComponent : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer _audioMixer;

        private void Awake()
        {
            _audioMixer.GetFloat("MusicVolume", out var value);
            this.gameObject.GetComponent<Slider>().value = value;

            this.gameObject.GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
        }
        private void OnDestroy()
        {
            this.gameObject.GetComponent<Slider>().onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _audioMixer.SetFloat("MusicVolume", value);
        }
    }
}
