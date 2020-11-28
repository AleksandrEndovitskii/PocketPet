using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components.AudioComponents
{
    [RequireComponent(typeof(Slider))]
    public class AudioMixerParameterValueSliderComponent : MonoBehaviour
    {
        [SerializeField]
        private string _parameterName;

        private Slider _slider;

        private void Awake()
        {
            _slider = this.gameObject.GetComponent<Slider>();

            _slider.value = GameManager.Instance.SettingsManager.Get(_parameterName);

            _slider.onValueChanged.AddListener(OnValueChanged);
        }
        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            GameManager.Instance.SettingsManager.Set(_parameterName, _slider.value);
        }
    }
}
