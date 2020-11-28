using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components.InteractionComponents
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickInteractableComponent : MonoBehaviour, IInteractable
    {
        private Button _button;

        private void Awake()
        {
            _button = this.gameObject.GetComponent<Button>();

            GameManager.Instance.InteractionManager.TrackInteractable(this);
        }
        private void OnDestroy()
        {
            GameManager.Instance.InteractionManager.UnTrackInteractable(this);
        }

        public void Interact()
        {
            _button.onClick.Invoke();

            GameManager.Instance.InteractionManager.InvokeInteraction(this);
        }
    }
}
