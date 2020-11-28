using System.Collections.Generic;
using Components.InteractionComponents;

namespace Models
{
    public class InteractableSequenceModel
    {
        public bool IsFullyInteracted => false;

        public List<IInteractable> Interactables => _interactables;

        private List<IInteractable> _interactables = new List<IInteractable>();

        public InteractableSequenceModel(int count, List<IInteractable> interactableBlueprints)
        {
            for (var i = 0; i < count; i++)
            {
                var randomInteractable = interactableBlueprints.GetRandomElement();
                _interactables.Add(randomInteractable);
            }
        }
    }
}