using System;
using System.Collections.Generic;
using System.Linq;
using Components.InteractionComponents;
using Utils;

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