using System;
using System.Collections;
using System.Collections.Generic;
using Components.GameObjectComponents;
using Components.InteractionComponents;
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

        private List<KeyValuePair<List<KeyValuePair<IInteractable, bool>>, bool>> _sequencesOfInteractables =
            new List<KeyValuePair<List<KeyValuePair<IInteractable, bool>>, bool>>();
        private int _currentStepNumber;

        private void Awake()
        {
            Initialize();

            StartMiniGame();
        }

        public void Initialize()
        {
            IsAutoplayOn = false;

            BallsContainerInstance = Instantiate(_ballsContainerPrefab, _ballsContainerParent);

            for (var currentStepNumber = _startStepsCount; currentStepNumber <= _finishStepsCount; currentStepNumber++)
            {
                var sequenceOfInteractables = new List<KeyValuePair<IInteractable, bool>>();
                for (var i = 0; i < currentStepNumber; i++)
                {
                    var randomInteractable = GetRandomInteractable();
                    sequenceOfInteractables.Add(new KeyValuePair<IInteractable, bool>(randomInteractable, false));
                }
                _sequencesOfInteractables.Add(new KeyValuePair<List<KeyValuePair<IInteractable, bool>>, bool>(sequenceOfInteractables, false));
            }
        }

        private void StartMiniGame()
        {
            StartCoroutine(Autoplay());
        }

        private IInteractable GetRandomInteractable()
        {
            var randomIndex = UnityEngine.Random.Range(0, BallsContainerInstance.Instances.Count);
            var randomBall = BallsContainerInstance.Instances[randomIndex];
            var randomInteractable = randomBall.GetComponent<IInteractable>();
            return randomInteractable;
        }

        private IEnumerator Autoplay()
        {
            IsAutoplayOn = true;

            yield return new WaitForSeconds(_autoplayDelayBeforeStartSecondsCount);

            foreach (var sequenceOfInteractables in _sequencesOfInteractables)
            {
                foreach (var keyValuePair in sequenceOfInteractables.Key)
                {
                    keyValuePair.Key.Interact();

                    yield return new WaitForSeconds(_autoplayDelayBetweenStepsSecondsCount);
                }
            }

            IsAutoplayOn = false;

            yield return null;
        }
    }
}
