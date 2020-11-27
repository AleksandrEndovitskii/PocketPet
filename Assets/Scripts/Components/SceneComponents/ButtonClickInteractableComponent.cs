using System;
using Components.InteractionComponents;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components.SceneComponents
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
