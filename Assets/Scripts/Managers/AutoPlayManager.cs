using System;
using System.Collections;
using Models;
using UnityEngine;

namespace Managers
{
    public class AutoPlayManager : MonoBehaviour
    {
        public Action<bool> IsAutoplayOnChanged = delegate {  };

        [SerializeField]
        private float _autoplayDelayBeforeStartSecondsCount;
        [SerializeField]
        private float _autoplayDelayBetweenStepsSecondsCount;

        public bool IsAutoplayOn
        {
            get
            {
                return _isAutoplayOn;
            }
            set
            {
                if (_isAutoplayOn == value)
                {
                    return;
                }

                Debug.Log($"Mini game autoplay state changed from {_isAutoplayOn} to {value}");

                _isAutoplayOn = value;

                IsAutoplayOnChanged.Invoke(_isAutoplayOn);
            }
        }

        private bool _isAutoplayOn;

        public void StartAutoPlay(InteractableSequenceModel interactableSequenceModel)
        {
            StartCoroutine(Autoplay(interactableSequenceModel));
        }

        private IEnumerator Autoplay(InteractableSequenceModel interactableSequenceModel)
        {
            IsAutoplayOn = true;

            yield return new WaitForSeconds(_autoplayDelayBeforeStartSecondsCount);

            foreach (var interactableIsInteracted in interactableSequenceModel.Interactables)
            {
                interactableIsInteracted.Interactable.Interact();

                yield return new WaitForSeconds(_autoplayDelayBetweenStepsSecondsCount);
            }

            IsAutoplayOn = false;

            yield return null;
        }
    }
}
