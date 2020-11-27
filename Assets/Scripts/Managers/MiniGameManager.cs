using System;
using System.Collections;
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

        private void Awake()
        {
            Initialize();

            StartMiniGame();
        }

        public void Initialize()
        {
            IsAutoplayOn = false;

            BallsContainerInstance = Instantiate(_ballsContainerPrefab, _ballsContainerParent);
        }

        private void StartMiniGame()
        {
            StartCoroutine(Autoplay());
        }

        private void InteractWithRandomBall()
        {
            var randomIndex = UnityEngine.Random.Range(0, BallsContainerInstance.Instances.Count);
            var randomBall = BallsContainerInstance.Instances[randomIndex];
            var randomInteractables = randomBall.GetComponents<IInteractable>();
            foreach (var randomInteractable in randomInteractables)
            {
                randomInteractable.Interact();
            }
        }

        private IEnumerator Autoplay()
        {
            IsAutoplayOn = true;

            yield return new WaitForSeconds(_autoplayDelayBeforeStartSecondsCount);

            for (var i = 0; i < _startStepsCount; i++)
            {
                InteractWithRandomBall();

                yield return new WaitForSeconds(_autoplayDelayBetweenStepsSecondsCount);
            }

            IsAutoplayOn = false;

            yield return null;
        }
    }
}
