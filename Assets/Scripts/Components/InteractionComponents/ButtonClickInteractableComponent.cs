using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Components.InteractionComponents
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickInteractableComponent : MonoBehaviour, IInteractable
    {
        // Event delegates triggered on click.
        [SerializeField]
        private Button.ButtonClickedEvent OnInteract = new Button.ButtonClickedEvent();

        private Button _button;

        private void Awake()
        {
            _button = this.gameObject.GetComponent<Button>();

            _button.onClick.AddListener(ButtonOnClick);

            GameManager.Instance.InteractionManager.TrackInteractable(this);
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonOnClick);

            if (GameManager.Instance.InteractionManager != null)
            {
                GameManager.Instance.InteractionManager.UnTrackInteractable(this);
            }
        }

        public void Interact()
        {
            // invoke all additional actions specified in inspector
            OnInteract.Invoke();

            GameManager.Instance.InteractionManager.InvokeInteraction(this);
        }

        private void ButtonOnClick()
        {
            Debug.Log("ButtonClickInteractableComponent button is pressed");

            Interact();
        }
    }
}
