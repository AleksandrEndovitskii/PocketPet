using System;
using System.Collections.Generic;
using Components.InteractionComponents;
using Components.SelectionComponents;
using UnityEngine;
using Utils;

namespace Managers
{
    public class InteractionManager : MonoBehaviour, IInitilizable
    {
        public Action<IInteractable> SelectedInteractableChanged = delegate {  };

        [SerializeField]
        private List<KeyCode> _interactableKeyCodes = new List<KeyCode>();

        public IInteractable SelectedInteractable
        {
            get
            {
                return _selectedInteractable;
            }
            set
            {
                if (_selectedInteractable == value)
                {
                    return;
                }

                _selectedInteractable = value;

                SelectedInteractableChanged.Invoke(_selectedInteractable);
            }
        }

        private IInteractable _selectedInteractable;

        public void Initialize()
        {
            GameManager.Instance.SelectionManager.SelectableComponentChanged += SelectableComponentChanged;

            GameManager.Instance.InputManager.KeyPressed += OnKeyPressed;
        }

        private void SelectableComponentChanged(SelectableComponent selectableComponent)
        {
            SelectedInteractable = GameManager.Instance.SelectionManager.SelectableComponent?.gameObject
                .GetComponent<IInteractable>();
        }

        private void OnKeyPressed(KeyCode keyCode)
        {
            if (!_interactableKeyCodes.Contains(keyCode))
            {
                return;
            }

            SelectedInteractable?.Interact();
        }
    }
}
