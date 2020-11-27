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

        private void Awake()
        {
            this.gameObject.GetComponent<Slider>().value = GameManager.Instance.SettingsManager.Get(_parameterName);

            this.gameObject.GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
        }
        private void OnDestroy()
        {
            this.gameObject.GetComponent<Slider>().onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            GameManager.Instance.SettingsManager.Set(_parameterName, this.gameObject.GetComponent<Slider>().value);
        }
    }
}
