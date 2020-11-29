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

        public List<IInteractable> TrackingInteractables => _trackingInteractables;

        private List<IInteractable> _trackingInteractables = new List<IInteractable>();

        private IInteractable _selectedInteractable;

        public void Initialize()
        {
            Subscribe();
        }
        public void Uninitialize()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            GameManager.Instance.SelectionManager.SelectedObjectChanged += SelectionManagerOnSelectedObjectChanged;

            GameManager.Instance.InputManager.KeyPressed += InputManagerOnKeyPressed;
        }
        private void UnSubscribe()
        {
            if (GameManager.Instance.SelectionManager != null &&
                GameManager.Instance.SelectionManager.SelectedObjectChanged != null)
            {
                GameManager.Instance.SelectionManager.SelectedObjectChanged -= SelectionManagerOnSelectedObjectChanged;
            }

            if (GameManager.Instance.InputManager != null &&
                GameManager.Instance.InputManager.KeyPressed != null)
            {
                GameManager.Instance.InputManager.KeyPressed -= InputManagerOnKeyPressed;
            }
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

        private void InputManagerOnKeyPressed(KeyCode keyCode)
        {
            if (!_interactableKeyCodes.Contains(keyCode))
            {
                return;
            }

            SelectedInteractable?.Interact();
        }

        private void SelectionManagerOnSelectedObjectChanged(SelectableComponent selectableComponent)
        {
            SelectedInteractable = GameManager.Instance.SelectionManager.SelectedObject?.gameObject
                .GetComponent<IInteractable>();
        }
    }
}
