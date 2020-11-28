using System;
using System.Collections.Generic;
using System.Linq;
using Components.InteractionComponents;
using Utils;

namespace Models
{
    public class MiniGameModel
    {
        public Action<bool> IsInteractedChanged = delegate {  };

        public bool IsInteracted
        {
            get
            {
                var result = false;

                result = InteractableSequenceModels.All(x=>x.IsInteracted);

                return result;
            }
            set
            {
                InteractableSequenceModels.ForEach(x=>x.IsInteracted = value);
            }
        }

        public List<InteractableSequenceModel> InteractableSequenceModels => _interactableSequenceModels;

        public InteractableIsInteracted FirstUnInteractedInteractable
        {
            get
            {
                var firstUnInteractableSequenceModel = InteractableSequenceModels.FirstOrDefault(x =>
                    x.Interactables.Any(y=> y.IsInteracted == false));
                var firstUnInteractedInteractable = firstUnInteractableSequenceModel?.Interactables.FirstOrDefault(x =>
                    x.IsInteracted == false);

                return firstUnInteractedInteractable;
            }
        }

        public InteractableSequenceModel FirstUnInteractedSequence
        {
            get
            {
                var firstUnInteractableSequenceModel = InteractableSequenceModels.FirstOrDefault(x =>
                    x.Interactables.Any(y=> y.IsInteracted == false));

                return firstUnInteractableSequenceModel;
            }
        }

        private List<InteractableSequenceModel> _interactableSequenceModels = new List<InteractableSequenceModel>();

        private bool _isInteractedBufferValue;

        public MiniGameModel(int startStepsCount, int finishStepsCount, List<IInteractable> interactableBlueprints)
        {
            for (var currentStepNumber = startStepsCount;
                currentStepNumber <= finishStepsCount;
                currentStepNumber++)
            {
                var interactableSequence = new InteractableSequenceModel(currentStepNumber, interactableBlueprints);
                interactableSequence.IsInteractedChanged += OnIsInteractedChanged;

                _interactableSequenceModels.Add(interactableSequence);
            }

            _isInteractedBufferValue = IsInteracted;
        }

        private void OnIsInteractedChanged(bool isInteracted)
        {
            // is interacted dos not changed - nothing to do here
            if (IsInteracted == _isInteractedBufferValue)
            {
                return;
            }

            // update interacted buffer value
            _isInteractedBufferValue = IsInteracted;

            IsInteractedChanged.Invoke(IsInteracted);
        }
    }
}