using System;
using System.Collections.Generic;
using System.Linq;
using Components.InteractionComponents;
using UnityEngine;

namespace Models
{
    public class InteractableSequenceModel
    {
        public Action<bool> IsInteractedChanged = delegate {  };

        public bool IsInteracted
        {
            get
            {
                var result = false;

                result = Interactables.All(x=>x.IsInteracted);

                return result;
            }
            set
            {
                Interactables.ForEach(x=>x.IsInteracted = value);
            }
        }

        public List<InteractableIsInteracted> Interactables => _interactables;

        private List<InteractableIsInteracted> _interactables = new List<InteractableIsInteracted>();

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

                    Debug.Log($"Is interacted state changed from {_isInteracted} to {value}");

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

        private bool _isInteractedBufferValue;

        public InteractableSequenceModel(int count, List<IInteractable> interactableBlueprints)
        {
            for (var i = 0; i < count; i++)
            {
                var randomInteractable = interactableBlueprints.GetRandomElement();
                var interactableIsInteracted = new InteractableIsInteracted(randomInteractable, false);
                interactableIsInteracted.IsInteractedChanged += OnIsInteractedChanged;

                _interactables.Add(interactableIsInteracted);
            }

            _isInteractedBufferValue = IsInteracted;
        }

        private void OnIsInteractedChanged(bool isInteracted)
        {
            if (IsInteracted == _isInteractedBufferValue)
            {
                return;
            }

            _isInteractedBufferValue = IsInteracted;

            IsInteractedChanged.Invoke(IsInteracted);
        }
    }
}