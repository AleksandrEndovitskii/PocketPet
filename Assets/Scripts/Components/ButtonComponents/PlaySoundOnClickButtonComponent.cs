using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components.ButtonComponents
{
    [RequireComponent(typeof(Button))]
    public class PlaySoundOnClickButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _audioClip;

        private void Awake()
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(ButtonOnClick);
        }
        private void OnDestroy()
        {
            this.gameObject.GetComponent<Button>().onClick.RemoveListener(ButtonOnClick);
        }

        private void ButtonOnClick()
        {
            Debug.Log("PlaySoundOnClickButtonComponent button is pressed");

            GameManager.Instance.SoundManager.PlaySound(_audioClip);
        }
    }
}