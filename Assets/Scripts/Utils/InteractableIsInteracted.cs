using System;
using Components.InteractionComponents;
using UnityEngine;

namespace Utils
{
    public class InteractableIsInteracted
    {
        public Action<bool> IsInteractedChanged = delegate {  };

        public IInteractable Interactable;

        public bool IsInteracted
        {
            get
            {
                return _isInteracted;
            }
            set
            {
                if (_isInteracted == value)
                {
                    return;
                }

                _isInteracted = value;

                Debug.Log($"Interactable is interacted state changed from {_isInteracted} to {value}");

                IsInteractedChanged.Invoke(value);
            }
        }

        private bool _isInteracted;

        public InteractableIsInteracted(IInteractable interactable, bool isInteracted)
        {
            Interactable = interactable;
            IsInteracted = isInteracted;
        }
    }
}