using System;
using System.Collections;
using System.Linq;
using Components.GameObjectComponents;
using Components.InteractionComponents;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour, IInitilizable
    {
        public Action<bool> IsAutoplayOnChanged = delegate {  };

        [SerializeField]
        private ObjectsInstantiatingComponent _ballsContainerPrefab;
        [SerializeField]
        private Transform _ballsContainerParent;
        [SerializeField]
        private int _startStepsCount;
        [SerializeField]
        private int _finishStepsCount;
        [SerializeField]
        private float _autoplayDelayBeforeStartSecondsCount;
        [SerializeField]
        private float _autoplayDelayBetweenStepsSecondsCount;

        public ObjectsInstantiatingComponent BallsContainerInstance;
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

        private MiniGameModel _miniGameModel;

        private void Awake()
        {
            Initialize();

            StartMiniGame();
        }

        public void Initialize()
        {
            IsAutoplayOn = false;

            BallsContainerInstance = Instantiate(_ballsContainerPrefab, _ballsContainerParent);

            var interactableBlueprints = BallsContainerInstance.Instances.Select(x =>
                x.GetComponent<IInteractable>()).ToList();
            _miniGameModel = new MiniGameModel(_startStepsCount,_finishStepsCount, interactableBlueprints);
        }

        private void StartMiniGame()
        {
            var interactableSequenceModel = _miniGameModel.InteractableSequenceModels.FirstOrDefault(x =>
                x.IsFullyInteracted == false);
            // no more interactableSequenceModels to interact with - game over
            if (interactableSequenceModel == null)
            {
                return;
            }

            StartCoroutine(Autoplay(interactableSequenceModel));
        }

        private IEnumerator Autoplay(InteractableSequenceModel interactableSequenceModel)
        {
            IsAutoplayOn = true;

            yield return new WaitForSeconds(_autoplayDelayBeforeStartSecondsCount);

            foreach (var keyValuePair in interactableSequenceModel.Interactables)
            {
                keyValuePair.Interact();

                yield return new WaitForSeconds(_autoplayDelayBetweenStepsSecondsCount);
            }

            IsAutoplayOn = false;

            yield return null;
        }
    }
}
