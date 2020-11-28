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

            Subscribe();

            PlayNextUnPlayedInteractableSequenceModel();
        }

        private void Subscribe()
        {
            GameManager.Instance.AutoPlayManager.IsAutoplayOnChanged += OnIsAutoplayOnChanged;
        }

        public void Initialize()
        {
            IsAutoplayOn = false;

            BallsContainerInstance = Instantiate(_ballsContainerPrefab, _ballsContainerParent);

            var interactableBlueprints = BallsContainerInstance.Instances.Select(x =>
                x.GetComponent<IInteractable>()).ToList();
            _miniGameModel = new MiniGameModel(_startStepsCount,_finishStepsCount, interactableBlueprints);
        }

        private void OnIsAutoplayOnChanged(bool isAutoplayOn)
        {
            if (isAutoplayOn == true)
            {
                return;
            }

            PlayNextUnPlayedInteractableSequenceModel();
        }

        private void PlayNextUnPlayedInteractableSequenceModel()
        {
            var unPlayedInteractableSequenceModel = _miniGameModel.InteractableSequenceModels.FirstOrDefault(x =>
                x.IsFullyInteracted == false);
            // all interactable sequences was played - game over
            if (unPlayedInteractableSequenceModel == null)
            {
                return;
            }

            GameManager.Instance.AutoPlayManager.StartAutoPlay(unPlayedInteractableSequenceModel);
        }
    }
}
