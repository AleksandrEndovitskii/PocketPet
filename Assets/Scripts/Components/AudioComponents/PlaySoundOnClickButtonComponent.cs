using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components.AudioComponents
{
    [RequireComponent(typeof(Button))]
    public class PlaySoundOnClickButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _audioClip;

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
            Debug.Log("PlaySoundOnClickButtonComponent button is pressed");

            GameManager.Instance.SoundManager.PlaySound(_audioClip);
        }
    }
}