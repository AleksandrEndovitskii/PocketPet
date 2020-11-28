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
        public Action<IInteractable> Interacted = delegate {  };
        public Action<IInteractable> SelectedInteractableChanged = delegate {  };

        [SerializeField]
        private List<KeyCode> _interactableKeyCodes = new List<KeyCode>();

        public List<KeyCode> InteractableKeyCodes => _interactableKeyCodes;

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

        public List<IInteractable> TrackingInteractables { get; private set; }

        private IInteractable _selectedInteractable;

        public void Initialize()
        {
            GameManager.Instance.SelectionManager.SelectedObjectChanged += SelectableComponentChanged;

            GameManager.Instance.InputManager.KeyPressed += OnKeyPressed;
        }

        public void TrackInteractable(IInteractable interactable)
        {
            TrackingInteractables.Add(interactable);
        }
        public void UnTrackInteractable(IInteractable interactable)
        {
            TrackingInteractables.Remove(interactable);
        }

        public void InvokeInteraction(IInteractable interactable)
        {
            if (!TrackingInteractables.Contains(interactable))
            {
                Debug.LogWarning("Cannot invoke interaction on non trackable interactable");

                return;
            }

            Interacted.Invoke(interactable);
        }

        private void SelectableComponentChanged(SelectableComponent selectableComponent)
        {
            SelectedInteractable = GameManager.Instance.SelectionManager.SelectedObject?.gameObject
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
