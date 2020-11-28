using System.Collections.Generic;
using Components.InteractionComponents;

namespace Models
{
    public class MiniGameModel
    {
        public bool IsFullyInteracted => false;

        public List<InteractableSequenceModel> InteractableSequenceModels => _interactableSequenceModels;

        private List<InteractableSequenceModel> _interactableSequenceModels = new List<InteractableSequenceModel>();

        public MiniGameModel(int startStepsCount, int finishStepsCount, List<IInteractable> interactableBlueprints)
        {
            for (var currentStepNumber = startStepsCount;
                currentStepNumber <= finishStepsCount;
                currentStepNumber++)
            {
                var interactableSequence = new InteractableSequenceModel(currentStepNumber, interactableBlueprints);
                _interactableSequenceModels.Add(interactableSequence);
            }
        }
    }
}