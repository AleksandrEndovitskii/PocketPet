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
        }

        public void Interact()
        {
            _button.onClick.Invoke();
        }
    }
}
