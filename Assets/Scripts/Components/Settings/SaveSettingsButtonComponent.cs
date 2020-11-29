using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Settings
{
    [RequireComponent(typeof(Button))]
    public class SaveSettingsButtonComponent : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = this.gameObject.GetComponent<Button>();

            _button.onClick.AddListener(ButtonOnClick);
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonOnClick);
        }

        private void ButtonOnClick()
        {
            Debug.Log("SaveSettingsButtonComponent button is pressed");

            GameManager.Instance.SettingsManager.Save();
        }
    }
}